using Booking.ApplicationCore.Interfaces;
using Booking.ApplicationCore.Models;
using Booking.Infrastructure.Services;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Booking.Web.ModelBinders
{
    public sealed class ApartmentTypeViewModelBinder
    {
        readonly private IRepository<ApartmentType> _apartmentTypeRepository;
        public ApartmentTypeViewModelBinder(IRepository<ApartmentType> apartmentTypeRepository)
        {
            _apartmentTypeRepository= apartmentTypeRepository;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
            var value = valueProviderResult.FirstValue;
            var id = int.Parse(value);
            var model = _apartmentTypeRepository.GetById(id);

            var result = new ApartmentTypeViewModel()
            {
                Id = model.Id,
                Name = model.Name,
            };

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
