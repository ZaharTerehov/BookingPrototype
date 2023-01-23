using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Interfaces;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Services
{
    public sealed class CityViewModelService : ICityViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityViewModelService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
            _mapper = mapper;
        }
        public async Task CreateCityAsync(CityViewModel viewModel)
        {
            var dto = _mapper.Map<City>(viewModel);
            await _unitOfWork.Cities.CreateAsync(dto);
        }

        public async Task DeleteCityAsync(CityViewModel viewModel)
        {
            var existingCity = await _unitOfWork.Cities.GetByIdAsync(viewModel.Id);
            if (existingCity is null)
            {
                var exception = new Exception($"Country with id = {viewModel.Id} was not found");

                throw exception;
            }

            await _unitOfWork.Cities.DeleteAsync(existingCity);
        }

        public async Task<CityIndexViewModel> GetCitiesAsync(int? countryId)
        {
             var options = new QueryViewModelOption<City, CityViewModel>().AddSortOption(false, x => x.Country.Name)
                                                    .AddSortOption(false, y => y.Name)
                                                    .SetFilterOption(item => (!countryId.HasValue || item.CountryId == countryId))
                                                    .AddSelectOption(item => new CityViewModel() 
                                                                    { 
                                                                        Id = item.Id,
                                                                        Name = item.Name,
                                                                        CountryName = item.Country.Name
                                                                    });
            var cities = await _unitOfWork.Cities.GetAllDtoAsync(options);

            var vm = new CityIndexViewModel()
            {
                Cities = cities,
                Countries = (await GetCountries(true)).ToList(),
            };

            return vm;
        }

        public async Task<CityViewModel> GetCityViewModelByIdAsync(int id)
        {
            var existingCity = await _unitOfWork.Cities.GetByIdAsync(id);

            if (existingCity == null)
            {
                var exception = new Exception($"Appartment type with id = {id} was not found");

                throw exception;
            }

            var dto = _mapper.Map<CityViewModel>(existingCity);

            return dto;
        }

        public async Task<IList<SelectListItem>> GetCountries(bool filter)
        {
            //_logger.LogInformation("GetBrands call");
            var options = new QueryEntityOptions<Country>().AddSortOption(false, y => y.Name);
            var entities = await _unitOfWork.Countries.GetAllAsync(options);
            var countries = _mapper.Map<List<SelectListItem>>(entities);
            if (filter)
            {
                var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
                countries.Insert(0, allItem);
            }
            
            return countries;
        }

        public async Task UpdateCity(CityViewModel viewModel)
        {
            var existingCity = await _unitOfWork.Cities.GetByIdAsync(viewModel.Id);

            if (existingCity is null)
            {
                var exception = new Exception($"Apartment type {viewModel.Id} was not found");
                throw exception;
            }

            City.CityDetails details = new City.CityDetails(viewModel.Name, viewModel.CountryFilterApplied);
            existingCity.UpdateDetails(details);
            await _unitOfWork.Cities.UpdateAsync(existingCity);
        }
    }
}
