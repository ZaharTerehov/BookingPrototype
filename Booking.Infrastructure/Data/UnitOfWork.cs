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

        public UnitOfWork(IRepository<City> cities, IRepository<Country> countries, IRepository<ApartmentType> apartmentTypes)
        {
            Cities=cities;
            Countries=countries;
            ApartmentTypes=apartmentTypes;
        }
    }
}
