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
        public DbSet<ApartmentType> ApartmentTypes { get; set; }

        public BookingContext(DbContextOptions<BookingContext> options) : base(options) { }

    }
}
