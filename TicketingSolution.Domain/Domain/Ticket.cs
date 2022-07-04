using System.ComponentModel.DataAnnotations;

namespace TicketingSolution.Domain
{
    public class Ticket
    {
        
        public int ID { get; set; }
        public string Name { get; set; }

        public List<TicketBooking> TicketBooking { get; set; }
    }
}