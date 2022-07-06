using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TicketingSolution.Core.DataServices;
using TicketingSolution.Core.Enums;
using TicketingSolution.Domain;
using Xunit;
using Xunit.Abstractions;

namespace TicketingSolution.Core
{
    public class TicketBookingReqestHandlerTest
    {
        private readonly TicketBookingRequestHandler _handler;
        private readonly TicketBookingRequest _request;
        private readonly Mock<ITicketBoockingService> _ticketBookingServiceMock;
        private readonly ITestOutputHelper _testOutputHelper;
        List<Ticket> _availableTickets;



        public TicketBookingReqestHandlerTest(ITestOutputHelper testOutputHelper)
        {

            _testOutputHelper = testOutputHelper;
            
            
            //Arrange
            _request = new TicketBookingRequest
            {
                Name = "saied",
                Family = "rahimy",
                Email = "saied.rahimy@gmail.com" ,
                Date = DateTime.Now,    
            };

            _availableTickets=new List<Ticket>() { new Ticket() { ID = 1 },new Ticket() { ID = 10 } };
            _ticketBookingServiceMock = new Mock<ITicketBoockingService>();

            // مقدار دهی خروجی متد فراخوانی شده از سرویس مورد نظر
            _ticketBookingServiceMock.Setup(x => x.GetAvailableTickets(_request.Date)).Returns(_availableTickets);

            _handler = new TicketBookingRequestHandler(_ticketBookingServiceMock.Object);
            
        }

        [Fact]
        [Trait("Core","Booking")]
        public void Should_Return_Ticket_Booking_Response_With_Request_Value()
        {           

            _testOutputHelper.WriteLine("Should_Return_Ticket_Booking_Response_With_Request_Value :D");
            
            //Act
            ServiceBookigResult result= _handler.BookService(_request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Name, _request.Name);
            Assert.Equal(result.Family, _request.Family);
            Assert.Equal(result.Email, _request.Email);            

        }

        [Fact]
        public void Should_throw_Exceptions_For_Null_Request()
        {
            Assert.Throws<ArgumentNullException>(() => _handler.BookService(null));
            #region Exception other Types
            //ArgumentException
            //ArgumentNullException
            //ArgumentOutOfRangeException
            //DivideByZeroException
            //FileNotFoundException
            //FormatException
            //IndexOutOfRangeException
            //InvalidOperationException
            //KeyNotFoundException
            //NotSupportedException
            //NullReferenceException
            //OverflowException
            //OutOfMemoryException
            //StackOverflowException
            //TimeoutException

            #endregion

        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _handler.BookService(null));

            Assert.Equal("bookingRequest", exception.ParamName);
        }

        [Fact]
        public void Should_Save_Booking_Request()
        {
            //Arrange
            TicketBooking ticketBooking = null;            
            _ticketBookingServiceMock.Setup(x => x.Save(It.IsAny<TicketBooking>())).Callback<TicketBooking>
                (booking =>
                {
                    ticketBooking = booking;
                });

            //Act
            _handler.BookService(_request);


            // Assert
            _ticketBookingServiceMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once);
            _ticketBookingServiceMock.Verify(x => x.Save(It.Is<TicketBooking>(c=>c.TicketID==10)), Times.Never);
            Assert.NotNull(ticketBooking);
            Assert.Equal(ticketBooking.Name, _request.Name);
            Assert.Equal(ticketBooking.Family, _request.Family);
            Assert.Equal(ticketBooking.Email, _request.Email);
            Assert.Equal(ticketBooking.TicketID, _availableTickets.First().ID);
        }

        [Fact]
        public void Should_Not_Save_Booking_Request_If_None_Available()
        {
            _availableTickets.Clear();
            _handler.BookService(_request);

            _ticketBookingServiceMock.Verify(x=> x.Save(It.IsAny<TicketBooking>()), Times.Never);

            

        }

        [Theory]
        [InlineData(BookingResultFlag.Failure,false)]
        [InlineData(BookingResultFlag.Success,true)]
        public void Should_Return_SuccessOrFailure_Flag_In_Result(BookingResultFlag bookingSuccessFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableTickets.Clear();
            }

            var result = _handler.BookService(_request);

            Assert.Equal(bookingSuccessFlag, result.Flag);



        }

        [Theory]
        [MemberData(nameof(TestDataShare.SuccessOrFailureData), MemberType =typeof(TestDataShare))]
        public void Should_Return_SuccessOrFailure_Flag_In_Result_MemberData(BookingResultFlag bookingSuccessFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableTickets.Clear();
            }

            var result = _handler.BookService(_request);

            Assert.Equal(bookingSuccessFlag, result.Flag);



        }

        [Theory]
        [SuccessOrFailureData]
        public void Should_Return_SuccessOrFailure_Flag_In_Result_SuccessOrFailureDataAttribute(BookingResultFlag bookingSuccessFlag, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableTickets.Clear();
            }

            var result = _handler.BookService(_request);

            Assert.Equal(bookingSuccessFlag, result.Flag);



        }

        [Theory]
        [InlineData(null, false)]
        [InlineData(1, true)]
        public void Should_Return_TicketBookingID_In_Result(int? ticketBookingID, bool isAvailable)
        {
            if (!isAvailable)
            {
                _availableTickets.Clear();
            }
            else
            {
                // نحوه مقدار دهی به متد save
                _ticketBookingServiceMock.Setup(x => x.Save(It.IsAny<TicketBooking>())).Callback<TicketBooking>
                (booking =>
                {
                    booking.Id = ticketBookingID.Value;
                });
                _handler.BookService(_request);
            }

            var result = _handler.BookService(_request);

            Assert.Equal(ticketBookingID, result.TicketBookingId);



        }



    }
}
