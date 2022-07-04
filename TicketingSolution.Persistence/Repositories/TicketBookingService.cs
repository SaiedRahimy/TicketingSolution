using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Core.DataServices;
using TicketingSolution.Domain;

namespace TicketingSolution.Persistence.Repositories
{
    public class TicketBookingService : ITicketBoockingService
    {
        private readonly  TicketingSolutionDbContext _context;

        public TicketBookingService(TicketingSolutionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ticket> GetAvailableTickets(DateTime date)
        {
         //  var unAvailableTickets=_context.TicketBookings.Where(c=>c.Date==date).Select(t=>t.TicketID).ToList();
         //return  _context.Tickets.Where(c=> !unAvailableTickets.Contains( c.ID)).ToList();

            return _context.Tickets.Where(c =>!c.TicketBooking.Any(t => t.Date == date)).ToList();  

        }

        public void Save(TicketBooking ticketBooking)
        {
            _context.Add(ticketBooking);
            _context.SaveChanges();
        }
    }
}
