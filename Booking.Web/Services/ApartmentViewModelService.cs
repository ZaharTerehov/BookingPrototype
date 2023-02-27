using AutoMapper;
using Booking.ApplicationCore.Enums;
using Booking.ApplicationCore.Extentions;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Extentions;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Booking.Web.Services.QueryOptions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Services
{
    public sealed class ApartmentViewModelService : IApartmentViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IApartmentTypeViewModelService _apartmentTypeViewModelService;
        private readonly IReservationViewModelService _reservationViewModelService;

        public ApartmentViewModelService(IUnitOfWork unitOfWork, 
                                         IMapper mapper, 
                                         IApartmentTypeViewModelService apartmentTypeViewModelService,
                                         IReservationViewModelService reservationViewModelService)
        {
            _unitOfWork= unitOfWork;
            _mapper= mapper;
            _apartmentTypeViewModelService = apartmentTypeViewModelService;
            _reservationViewModelService = reservationViewModelService;
        }

        public async Task CreateApartmentAsync(ApartmentViewModel viewModel)
        {
            var dto = _mapper.Map<Apartment>(viewModel);
            await _unitOfWork.Apartments.CreateAsync(dto);
        }

        public async Task DeleteApartmentAsync(int id)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(id, x => x.Pictures);
            if (existingApartment is null)
            {
                var exception = new Exception($"Apartment {id} was not found");

                throw exception;
            }

            await _unitOfWork.Apartments.DeleteAsync(existingApartment);
        }     

        public async Task<IList<ApartmentViewModel>> GetApartmentsAsync(ApartmentQueryOptions apartmentOptions)
        {
            string checkIn = apartmentOptions.ArrivalDate.ToYYYYMMDDDateFormat();
            string checkOut = apartmentOptions.DepartureDate.ToYYYYMMDDDateFormat();
            string query = "SELECT * FROM Apartments a" +
                            " WHERE a.Id NOT IN " +
                            "                   (SELECT DISTINCT ApartmentId FROM Reservations r" +
                                                    $" WHERE  (('{checkIn}' >= r.ArrivalDate AND '{checkIn}' < r.DepartureDate) OR" +
                                                    $" ('{checkOut}' > r.ArrivalDate AND '{checkOut}' <= r.DepartureDate) OR" +
                                                    $" ('{checkIn}' <= r.ArrivalDate AND '{checkOut}' >= r.DepartureDate)))";


            var queryOptions = new QueryEntityOptions<Apartment>()
                .AddSqlQuery(query)
                .AddIncludeOption(x => x.City!)
                .AddIncludeOption(x => x.Pictures)
                .AddIncludeOption(x => x.Reviews!)
                .AddSortOption(false, y => y.Price)
                .SetFilterOption(x => 
                    (!apartmentOptions.ApartmentTypeFilterApplied.HasValue || x.ApartmentTypeId == apartmentOptions.ApartmentTypeFilterApplied) &&
                    (!apartmentOptions.CityFilterApplied.HasValue || x.CityId == apartmentOptions.CityFilterApplied) &&                
                     x.PeopleNumber >= apartmentOptions.NeedPeopleNumber &&
                     (
                        string.IsNullOrEmpty(apartmentOptions.SearchText) ||        (
                                 x.Name!.Contains(apartmentOptions.SearchText) ||
                                 x.City!.Name!.Contains(apartmentOptions.SearchText) ||
                                 x.Description!.Contains(apartmentOptions.SearchText))
                      )
                      )                
                .SetCurentPageAndPageSize(apartmentOptions.PageOptions);
            var entities = await _unitOfWork.Apartments.GetAllAsync(queryOptions);
            var viewModelEntities = _mapper.Map<List<ApartmentViewModel>>(entities);
            return viewModelEntities;
        }        

        public async Task<ApartmentViewModel> GetApartmentViewModelByIdAsync(int id)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(id, x => x.City!, x => x.Pictures);
            if (existingApartment == null)
            {
                var exception = new Exception($"Apartment with id = {id} was not found");

                throw exception;
            }

            var dto = _mapper.Map<ApartmentViewModel>(existingApartment);

            return dto;
        }

        public async Task UpdateApartmentAsync(ApartmentViewModel viewModel)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(viewModel.Id, x => x.Pictures);
            if (existingApartment is null)
            {
                var exception = new Exception($"Apartment {viewModel.Id} was not found");
                throw exception;
            }

            Apartment.ApartmentDetails details = new Apartment.ApartmentDetails(viewModel.Name, viewModel.Description,viewModel.Price, 
                                                viewModel.Pictures, viewModel.ApartmentTypeFilterApplied, viewModel.CityFilterApplied,viewModel.Address, viewModel.PeopleNumber);
            existingApartment.UpdateDetails(details);
            await _unitOfWork.Apartments.UpdateAsync(existingApartment);
        }

        public async Task<IList<SelectListItem>> GetApartmentTypes(bool filter, bool itemAllSelected = true)
        {
            var options = new QueryEntityOptions<ApartmentType>().AddSortOption(false, y => y.Name);
            var entities = await _unitOfWork.ApartmentTypes.GetAllAsync(options);
            var apartmentTypes = _mapper.Map<List<SelectListItem>>(entities);
            if (filter)
            {
                apartmentTypes.AddAllItem(itemAllSelected);
            }

            return apartmentTypes;
        }

        public async Task<IList<SelectListItem>> GetCities(bool filter, bool itemAllSelected = true)
        {
            var options = new QueryEntityOptions<City>().AddSortOption(false, y => y.Name!);
            var entities = await _unitOfWork.Cities.GetAllAsync(options);
            var cities = _mapper.Map<List<SelectListItem>>(entities);
            if (filter)
            {
                cities.AddAllItem(itemAllSelected);
            }

            return cities;
        }

        public void DeleteApartmentPictures(int id)
        {
            throw new NotImplementedException();
        }
    }
}
