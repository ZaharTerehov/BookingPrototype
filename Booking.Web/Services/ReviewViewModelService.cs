using AutoMapper;
using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Web.Interfaces;
using Booking.Web.Models;

namespace Booking.Web.Services
{
    public class ReviewViewModelService : IReviewViewModelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewViewModelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task CreateReviewAsync(ReviewViewModel viewModel)
        {
            var dto = _mapper.Map<Review>(viewModel);
            await _unitOfWork.Reviews.CreateAsync(dto);
        }

        public Task<ReviewViewModel> GetReviewViewModelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


    }
}
