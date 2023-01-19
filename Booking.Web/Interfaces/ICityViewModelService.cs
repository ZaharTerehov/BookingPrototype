using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Interfaces
{
    public interface ICityViewModelService
    {
        Task<IList<SelectListItem>> GetCountries(bool filter);
        Task UpdateCity(CityViewModel viewModel);
        Task CreateCityAsync(CityViewModel viewModel);
        Task DeleteCityAsync(CityViewModel viewModel);
        Task<CityIndexViewModel> GetCitiesAsync(int? countryId);
        Task<CityViewModel> GetCityViewModelByIdAsync(int id);
    }
}
