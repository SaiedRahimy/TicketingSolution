using TicketingSolution.Core.Enums;
using TicketingSolution.Domain;

namespace TicketingSolution.Core
{
    public class ServiceBookigResult : ServiceBookigBase
    {
        public BookingResultFlag Flag { get; set; }
        public int? TicketBookingId { get; set; }

    }
}