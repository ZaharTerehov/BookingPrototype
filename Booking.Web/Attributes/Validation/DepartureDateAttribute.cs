using System.ComponentModel.DataAnnotations;

namespace Booking.Web.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DepartureDateAttribute: ValidationAttribute
    {
        public DepartureDateAttribute(string departureDateFieldName)
        {
            departureDateFieldName = departureDateFieldName;
        }

        private string DepartureDateFieldName { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime earlierDate = (DateTime)value;

            DateTime laterDate = (DateTime)validationContext.ObjectType.GetProperty(DepartureDateFieldName).GetValue(validationContext.ObjectInstance, null);

            if (laterDate.Subtract(earlierDate).TotalDays > 0)
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
