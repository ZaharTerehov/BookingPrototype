using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Attributes.Validation
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
            DateTime departureDate = (DateTime)value;

            DateTime arrivalDate = (DateTime)validationContext.ObjectType.GetProperty(ArrivalDateFieldName).GetValue(validationContext.ObjectInstance, null);

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
