using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;
using Booking.Web.Models;

namespace Booking.Web.Services
{
    public sealed class CountryViewModelService : ICountryViewModelService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IMapper _mapper;
                                                                                                                                                                                                                                                                                                                              
        public CountryViewModelService(IRepository<Country> countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper= mapper;
        }

        public async Task<CountryViewModel> GetCountryViewModelByIdAsync(int id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            
            if (country == null)
            {
                var exception = new Exception($"Country with id = {id} was not found");

                throw exception;
            }

            var dto = _mapper.Map<CountryViewModel>(country);

            return dto;
        }

        public async Task<List<CountryViewModel>> GetCountriesAsync()
        {
            var entities = await _countryRepository.GetAllAsync();
            var countries = _mapper.Map<List<CountryViewModel>>(entities);

            return countries;
        }
        
        public async Task CreateCountryAsync(CountryViewModel viewModel)
        {
            var dto = _mapper.Map<Country>(viewModel);
            await _countryRepository.CreateAsync(dto);
        }

        public async Task DeleteCountryAsync(CountryViewModel viewModel)
        {
            var existingApartmentType = await _countryRepository.GetByIdAsync(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Country with id = {viewModel.Id} was not found");

                throw exception;
            }

            await _countryRepository.DeleteAsync(existingApartmentType);
        }        

        public async Task UpdateCountry(CountryViewModel viewModel)
        {
            var existingApartmentType = await _countryRepository.GetByIdAsync(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Country with id = {viewModel.Id} was not found");
                throw exception;
            }

            Country.CountryDetails details = new Country.CountryDetails(viewModel.Name);
            existingApartmentType.UpdateDetails(details);
            await _countryRepository.UpdateAsync(existingApartmentType);
        }
    }
}
