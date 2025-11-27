using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Validators {
    public class NotPastDateAttribute : ValidationAttribute {
        public NotPastDateAttribute() {
            ErrorMessage = "Purchase date cannot be earlier than today's date.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value == null) {
                return new ValidationResult("Purchase date is required.");
            }

            if (value is DateTime purchaseDate) {
                // Compare only the date part (ignore time)
                var today = DateTime.Today;

                if (purchaseDate.Date < today) {
                    return new ValidationResult(ErrorMessage ?? "Purchase date cannot be in the past.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date format.");
        }
    }
}
