using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.API.Controllers;
using TicketingSolution.Core;
using TicketingSolution.Core.Enums;
using Xunit;

namespace TicketingSolution.API.Test
{
    public class BookingControllerTest
    {
        private Mock<ITicketBookingRequestHandler> _ticketBookingRequestHandler;
        BookingController _controller;
        TicketBookingRequest _request;  
        ServiceBookigResult _result;

        public BookingControllerTest()
        {
            _ticketBookingRequestHandler = new Mock<ITicketBookingRequestHandler>();
            _controller = new BookingController(_ticketBookingRequestHandler.Object);
            _request=new TicketBookingRequest();            
            _result = new ServiceBookigResult();

            //var ss=Mock.
            _ticketBookingRequestHandler.Setup(x => x.BookService(_request)).Returns(_result);



        }

        [Theory]
        [InlineData(1,true,typeof(OkObjectResult), BookingResultFlag.Success)]
        [InlineData(0,false,typeof(BadRequestObjectResult), BookingResultFlag.Failure)]
        public async Task Should_Call_Booking(int expectedMethodCalls, bool isMethodValid, Type expectedActionResultType, BookingResultFlag bookingResultFlag)
        {
            if (!isMethodValid)
            {
                _controller.ModelState.AddModelError("Key", "ErrorMessage");

            }
            _result.Flag = bookingResultFlag;


            //Act
            var result = await _controller.Book(_request);

            result.ShouldBeOfType(expectedActionResultType);

            //Assert
            _ticketBookingRequestHandler.Verify(x=>x.BookService(_request), Times.Exactly(expectedMethodCalls));

        }
    }
}
