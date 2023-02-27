using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<City> Cities { get; }

        public IRepository<Country> Countries { get; }

        public IRepository<ApartmentType> ApartmentTypes { get; }

        public IRepository<Apartment> Apartments { get; }

        public IRepository<User> Users { get; }
        public IRepository<Reservation> Reservations { get; }
        public IRepository<Review> Reviews { get; }

        public UnitOfWork(IRepository<City> cities, IRepository<Country> countries, IRepository<ApartmentType> apartmentTypes, IRepository<Apartment> apartments, IRepository<User> users, IRepository<Reservation> reservations, IRepository<Review> reviews)
        {
            Cities = cities;
            Countries = countries;
            ApartmentTypes = apartmentTypes;
            Apartments = apartments;
            Users = users;
            Reservations = reservations;
            Reviews = reviews;
        }
    }
}
