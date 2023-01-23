using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.ApplicationCore.QueryOptions;
using Booking.Web.Interfaces;
using Booking.Web.Models;

namespace Booking.Web.Services
{
    public sealed class CountryViewModelService : ICountryViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
                                                                                                                                                                                                                                                                                                                              
        public CountryViewModelService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper= mapper;
            _unitOfWork=unitOfWork;
        }

        public async Task<CountryViewModel> GetCountryViewModelByIdAsync(int id)
        {
            var country = await _unitOfWork.Countries.GetByIdAsync(id);
            
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
            var options = new QueryEntityOptions<Country>().AddSortOption(false, x => x.Name);
            var entities = await _unitOfWork.Countries.GetAllAsync(options);
            var countries = _mapper.Map<List<CountryViewModel>>(entities);

            return countries;
        }
        
        public async Task CreateCountryAsync(CountryViewModel viewModel)
        {
            var dto = _mapper.Map<Country>(viewModel);
            await _unitOfWork.Countries.CreateAsync(dto);
        }

        public async Task DeleteCountryAsync(CountryViewModel viewModel)
        {
            var existingApartmentType = await _unitOfWork.Countries.GetByIdAsync(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Country with id = {viewModel.Id} was not found");

                throw exception;
            }

            await _unitOfWork.Countries.DeleteAsync(existingApartmentType);
        }        

        public async Task UpdateCountry(CountryViewModel viewModel)
        {
            var existingApartmentType = await _unitOfWork.Countries.GetByIdAsync(viewModel.Id);
            if (existingApartmentType is null)
            {
                var exception = new Exception($"Country with id = {viewModel.Id} was not found");
                throw exception;
            }

            Country.CountryDetails details = new Country.CountryDetails(viewModel.Name);
            existingApartmentType.UpdateDetails(details);
            await _unitOfWork.Countries.UpdateAsync(existingApartmentType);
        }
    }
}
