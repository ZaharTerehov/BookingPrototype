using Booking.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.ApplicationCore.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<City> Cities { get; }
        IRepository<Country> Countries { get; }
        IRepository<ApartmentType> ApartmentTypes { get; }
        IRepository<Apartment> Apartments { get; }
        IRepository<User> Users { get; }
        IRepository<Reservation> Reservations { get; }
        IRepository<Review> Reviews { get; }
    }
}
