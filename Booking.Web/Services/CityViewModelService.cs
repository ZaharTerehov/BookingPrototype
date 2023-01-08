using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
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

        public Task DeleteCityAsync(CityViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<CityIndexViewModel> GetCitiesAsync(int? countryId)
        {
            var entities = await _unitOfWork.Cities.GetAllAsync();
            var selectedCities = entities.Where(item => (!countryId.HasValue || item.CountryId == countryId)).ToList();
            var cityes = _mapper.Map<List<CityViewModel>>(selectedCities);

            var vm = new CityIndexViewModel()
            {
                Cities = cityes,
                Countries = (await GetCountries(true)).ToList(),
            };
            //var entities = await _catalogItemRepository.GetAllAsync();
            //var catalogItems = entities
            //    .Where(item => (!brandId.HasValue || item.CatalogBrandId== brandId)
            //                    && (!typeId.HasValue || item.CatalogTypeId == typeId))
            //    .Select(x => new CatalogItemViewModel
            //    {
            //        Id= x.Id,
            //        Name= x.Name,
            //        PictureUrl= x.PictureUrl,
            //        Price= x.Price,
            //    }).ToList();

            //var vm = new CatalogIndexViewModel()
            //{
            //    CatalogItems = catalogItems,
            //    Brands = (await GetBrands()).ToList(),
            //    Types = (await GetTypes()).ToList(),
            //};
            //return vm;

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

        public async Task<IEnumerable<SelectListItem>> GetCountries(bool filter)
        {
            //_logger.LogInformation("GetBrands call");
            var countries = await _unitOfWork.Countries.GetAllAsync();
            var items = countries
                .Select(contry => new SelectListItem() { Value = contry.Id.ToString(), Text = contry.Name })
                .OrderBy(c => c.Text)
                .ToList();
            if (filter)
            {
                var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
                items.Insert(0, allItem);
            }
            

            return items;
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
