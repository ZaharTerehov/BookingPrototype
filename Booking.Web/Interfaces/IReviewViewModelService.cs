using Booking.Web.Models;

namespace Booking.Web.Interfaces
{
    public interface IReviewViewModelService
    {
        Task<ReviewViewModel> GetReviewViewModelByIdAsync(int id);
        Task CreateReviewAsync(ReviewViewModel viewModel);
    }
}
