using System.ComponentModel.DataAnnotations;

namespace TicketingSolution.Domain
{
    public class TicketBooking: ServiceBookigBase
    {
        
        public  int Id { get; set; }                
        public Ticket Ticket { get; set; }
        public int TicketID { get; set; }
    }
}