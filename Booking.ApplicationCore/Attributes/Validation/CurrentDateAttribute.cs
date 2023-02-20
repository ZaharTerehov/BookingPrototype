using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Booking.ApplicationCore.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute() : base() 
        {
            ErrorMessage = "Error! Date must be greater than today!";
        }

        public override bool IsValid(object? value)
        {
            DateTime begin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);

            if (DateTime.TryParse(value.ToString(), out var result))
            {
                return result >= begin;
            }            

            return false;
        }
    }
}
