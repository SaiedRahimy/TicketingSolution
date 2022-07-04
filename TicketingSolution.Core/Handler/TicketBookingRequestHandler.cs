using System;
using TicketingSolution.Core.DataServices;
using TicketingSolution.Domain;

namespace TicketingSolution.Core
{
    public class TicketBookingRequestHandler : ITicketBookingRequestHandler
    {
        private ITicketBoockingService _ticketBoockingService;

        public TicketBookingRequestHandler(ITicketBoockingService ticketBoockingService)
        {
            _ticketBoockingService = ticketBoockingService;
        }

        public ServiceBookigResult BookService(TicketBookingRequest bookingRequest)
        {
            if (bookingRequest is null)
            {
                throw new ArgumentNullException(nameof(bookingRequest));
            }

            var availableTickets = _ticketBoockingService.GetAvailableTickets(bookingRequest.Date);
            var result = ConvertBoockingModels<ServiceBookigResult>(bookingRequest);
            result.Flag = Enums.BookingResultFlag.Failure;
            result.TicketBookingId = null;


            if (availableTickets.Any())
            {
                var ticket = availableTickets.First();
                var ticketBooking = ConvertBoockingModels<TicketBooking>(bookingRequest);
                ticketBooking.TicketID = ticket.ID;
                result.Flag = Enums.BookingResultFlag.Success;
                result.TicketBookingId = ticket.ID;

                _ticketBoockingService.Save(ticketBooking);
            }


            return result;

        }

        private static TModel ConvertBoockingModels<TModel>(ServiceBookigBase booking) where TModel : ServiceBookigBase, new()
        {
            return new TModel
            {
                Name = booking.Name,
                Family = booking.Family,
                Email = booking.Email,
                Date = booking.Date,
            };
        }
    }
}