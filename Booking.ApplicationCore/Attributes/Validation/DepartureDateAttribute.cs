using System.ComponentModel.DataAnnotations;

namespace Booking.ApplicationCore.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DepartureDateAttribute: ValidationAttribute
    {
        private string ArrivalDateFieldName { get; init; }
        public DepartureDateAttribute(string departureDateFieldName)
        {
            ArrivalDateFieldName = departureDateFieldName;
        }        

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime.TryParse(value.ToString(), out var departureDate);
            object arrivalDateObject = validationContext.ObjectType.GetProperty(ArrivalDateFieldName).GetValue(validationContext.ObjectInstance, null);
            DateTime.TryParse(arrivalDateObject.ToString(), out var arrivalDate);

            if (departureDate.Subtract(arrivalDate).TotalDays > 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Daparture date is not later than arrival date");
            }
        }
    }
}
