namespace TicketingSolution.Core
{
    public interface ITicketBookingRequestHandler
    {
        ServiceBookigResult BookService(TicketBookingRequest bookingRequest);
    }
}