using System.ComponentModel.DataAnnotations;

namespace SafeSpace.Domain.Validation
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date of birth is required.");

            if (value is DateTime dob)
            {
                int age = DateTime.Today.Year - dob.Year;

                if (dob.Date > DateTime.Today.AddYears(-age))
                {
                    age--;
                }

                if (age >= _minimumAge)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult( $"You must be at least {_minimumAge} years old.");
            }

            return new ValidationResult("Invalid date.");
        }
    }
}
