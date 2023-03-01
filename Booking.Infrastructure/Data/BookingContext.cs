using Booking.ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data
{
    public class BookingContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

        internal Task SaveChangesAsync(IEnumerable<Apartment> enumerable)
        {
            throw new NotImplementedException();
        }
    }
}
