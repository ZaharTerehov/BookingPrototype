using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DepartureDateAttribute: ValidationAttribute
    {
        private DateTime _arrivalDate { get; init; }

        public DepartureDateAttribute(DateTime arrivalDate) 
        {
            _arrivalDate = arrivalDate;
        }

        public override bool IsValid(object? value)
        {
            if (DateTime.TryParse(value.ToString(), out var departureDate))
            {
                return departureDate.Subtract(_arrivalDate).TotalDays > 0;
            }

            return false;
        }
    }
}
