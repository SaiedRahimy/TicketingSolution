using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSolution.Domain;

namespace TicketingSolution.Persistence
{
    public class TicketingSolutionDbContext : DbContext
    {
        public TicketingSolutionDbContext(DbContextOptions<TicketingSolutionDbContext> options) : base(options)
        {

        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketBooking> TicketBookings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TicketBooking>().HasKey(c=>c.Id);            
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket { ID = 1, Name = "Shiraz" },
                new Ticket { ID = 2, Name = "esfahan" },
                new Ticket { ID = 3, Name = "tehran" }

);

        }


    }
}
