using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface ICountryViewModelService
    {
        Task UpdateCountry(CountryViewModel viewModel);
        Task CreateCountryAsync(CountryViewModel viewModel);
        Task DeleteCountryAsync(CountryViewModel viewModel);
        Task<List<CountryViewModel>> GetCountriesAsync();
        Task<CountryViewModel> GetCountryViewModelByIdAsync(int id);
    }
}
