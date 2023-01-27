using AutoMapper;
using Booking.ApplicationCore.Enums;
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
        private readonly IApartmentTypeViewModelService _ApartmentTypeViewModelService;

        public ApartmentViewModelService(IUnitOfWork unitOfWork, IMapper mapper, IApartmentTypeViewModelService ApartmentTypeViewModelService)
        {
            _unitOfWork= unitOfWork;
            _mapper= mapper;
            _ApartmentTypeViewModelService = ApartmentTypeViewModelService;
        }

        public async Task CreateApartmentAsync(ApartmentViewModel viewModel)
        {
            var dto = _mapper.Map<Apartment>(viewModel);
            await _unitOfWork.Apartments.CreateAsync(dto);
        }

        public async Task DeleteApartmentAsync(ApartmentViewModel viewModel)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(viewModel.Id);
            if (existingApartment is null)
            {
                var exception = new Exception($"Apartment {viewModel.Id} was not found");

                throw exception;
            }

            await _unitOfWork.Apartments.DeleteAsync(existingApartment);
        }

        public async Task<IList<ApartmentViewModel>> GetApartmentsAsync(ApartmentQueryOptions apartmentOptions)
        {
            var queryOptions = new QueryViewModelOption<Apartment, ApartmentViewModel>().AddSortOption(false, y => y.Price)
                .SetFilterOption(x => 
                (!apartmentOptions.ApartmentTypeFilterApplied.HasValue || x.ApartmentTypeId == apartmentOptions.ApartmentTypeFilterApplied) &&
                (!apartmentOptions.CityFilterApplied.HasValue || x.CityId == apartmentOptions.CityFilterApplied))                
                .AddSelectOption(x => new ApartmentViewModel 
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price= x.Price,
                    Picture= x.Picture,
                    CityName = x.City.Name
                })
                .SetCurentPageAndPageSize(apartmentOptions.PageOptions);
            var entities = await _unitOfWork.Apartments.GetAllDtoAsync(queryOptions);
            //var apartment = _mapper.Map<List<ApartmentViewModel>>(entities);
            //return apartment;
            return entities;
        }        

        public async Task<ApartmentViewModel> GetApartmentViewModelByIdAsync(int id)
        {
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(id, x => x.City);
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
            var existingApartment = await _unitOfWork.Apartments.GetByIdAsync(viewModel.Id);
            if (existingApartment is null)
            {
                var exception = new Exception($"Apartment {viewModel.Id} was not found");
                throw exception;
            }

            Apartment.ApartmentDetails details = new Apartment.ApartmentDetails(viewModel.Name, viewModel.Description,viewModel.Price, 
                                                viewModel.Picture, viewModel.ApartmentTypeFilterApplied, viewModel.CityFilterApplied,viewModel.Address, viewModel.PeopleNumber);
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
            var options = new QueryEntityOptions<City>().AddSortOption(false, y => y.Name);
            var entities = await _unitOfWork.Cities.GetAllAsync(options);
            var cities = _mapper.Map<List<SelectListItem>>(entities);
            if (filter)
            {
                cities.AddAllItem(itemAllSelected);
            }

            return cities;
        }
    }
}
