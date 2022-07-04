using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Domain;

namespace TicketingSolution.Core.DataServices
{
    public interface ITicketBoockingService
    {
        void Save(TicketBooking ticketBooking);

        IEnumerable<Ticket> GetAvailableTickets(DateTime date);
    }
}
