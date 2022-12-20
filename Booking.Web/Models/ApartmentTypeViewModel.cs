using Booking.Web.ModelBinders;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Models
{
    //[ModelBinder(BinderType = typeof(ApartmentTypeViewModelBinder))]
    public class ApartmentTypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
