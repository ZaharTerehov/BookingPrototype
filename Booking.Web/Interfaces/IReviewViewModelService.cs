using Booking.Web.Models;
using Booking.Web.Services.QueryOptions;

namespace Booking.Web.Interfaces
{
    public interface IReviewViewModelService
    {
        Task<ReviewViewModel> GetReviewViewModelByIdAsync(int id);
        Task CreateReviewAsync(ReviewViewModel viewModel);
        Task<ReviewViewModel> GetNewReviewViewModelAsync(ReviewOptions reviewOptions);
    }
}
