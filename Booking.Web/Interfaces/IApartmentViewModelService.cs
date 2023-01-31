﻿using Booking.ApplicationCore.Enums;
using Booking.ApplicationCore.Models;
using Booking.Web.Models;
using Booking.Web.Services.QueryOptions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Booking.Web.Interfaces
{
    public interface IApartmentViewModelService
    {
        Task<IList<ApartmentViewModel>> GetApartmentsAsync(ApartmentQueryOptions options);

        Task CreateApartmentAsync(ApartmentViewModel viewModel);
        Task UpdateApartmentAsync(ApartmentViewModel viewModel);
        Task DeleteApartmentAsync(ApartmentViewModel viewModel);
        Task<ApartmentViewModel> GetApartmentViewModelByIdAsync(int id);
        Task<IList<SelectListItem>> GetApartmentTypes(bool filter, bool itemAllSelected = true);
        Task<IList<SelectListItem>> GetCities(bool filter, bool itemAllSelected = true);

    }
}
