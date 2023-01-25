using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Booking.Web.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
            ErrorMessage = "Error! Date must be greater than today!";
        }

        public override bool IsValid(object? value)
        {
            DateTime _Begin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            DateTime.TryParse(value.ToString(), out var result);

            if (result < _Begin)
            {
                return false;
            }

            return true;
        }
    }
}
