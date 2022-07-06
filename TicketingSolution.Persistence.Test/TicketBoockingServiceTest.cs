using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Domain;
using TicketingSolution.Persistence.Repositories;
using Xunit;

namespace TicketingSolution.Persistence.Test
{
    public class TicketBoockingServiceTest
    {

        [Fact]
        public void Should_Save_Ticket_Booking()
        {
            var dbOptions = new DbContextOptionsBuilder<TicketingSolutionDbContext>().
                UseInMemoryDatabase("ShouldSaveTest", op => op.EnableNullChecks(false)).Options;
            using var context = new TicketingSolutionDbContext(dbOptions);

            var ticketBooking = new TicketBooking { Id = 1, TicketID = 1, Date = new DateTime(2022, 07, 03) };
            var ticketBookingService = new TicketBookingService(context);

            ticketBookingService.Save(ticketBooking);


            //Assert
            var bookings = context.TicketBookings.ToList();
            var booking=Assert.Single(bookings);

            Assert.Equal(ticketBooking.Date, booking.Date);
        }

        [Fact]
        public void Should_Retern_Available_Service()
        {
            var date = new DateTime(2022, 07, 03);

            var dbOptions = new DbContextOptionsBuilder<TicketingSolutionDbContext>().
                UseInMemoryDatabase("AvailableTicketTest",op=>op.EnableNullChecks(false)).Options;

            using var context=new TicketingSolutionDbContext(dbOptions);
            context.Add(new Ticket { ID = 1, Name = "Ticket 1" });
            context.Add(new Ticket { ID = 2, Name = "Ticket 2" });
            context.Add(new Ticket { ID = 3, Name = "Ticket 3" });
            
            
            context.Add(new TicketBooking {Id=1, TicketID = 1, Date=date  });
            context.Add(new TicketBooking {Id=2, TicketID = 2, Date=date.AddDays(-1) });


            context.SaveChanges();

            var ticketBookingService=new TicketBookingService(context);



            //Act
            var availableServices= ticketBookingService.GetAvailableTickets(date);


            //Assert

            Assert.Equal(2, availableServices.Count());
            Assert.Contains(availableServices, q => q.ID == 2);
            Assert.Contains(availableServices, q => q.ID == 3);
            //Assert.DoesNotContain(availableServices, q => q.ID == 3);

        }

        [Fact]
        public void Relation_Error()
        {
            var date = new DateTime(2022, 07, 03);

            var dbOptions = new DbContextOptionsBuilder<TicketingSolutionDbContext>().
                UseInMemoryDatabase("RelationTest", op => op.EnableNullChecks(false)).Options;

            using var context = new TicketingSolutionDbContext(dbOptions);
            context.Add(new Ticket { ID = 1,  });
                        
            context.Add(new TicketBooking { Id = 2, TicketID = 2, Date = date.AddDays(-1) });


            context.SaveChanges();

            var ticketBookingService = new TicketBookingService(context);



            //Act
            var availableServices = ticketBookingService.GetAvailableTickets(date);


            //Assert

            Assert.Equal(1, availableServices.Count());
           
        }
    }
}
