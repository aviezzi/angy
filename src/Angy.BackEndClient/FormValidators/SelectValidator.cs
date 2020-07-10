namespace Angy.BackEndClient.FormValidators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SelectValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value,
            ValidationContext validationContext)
        {
            var result = Guid.TryParse(value.ToString(), out var guid);

            if (result && guid != Guid.Empty) return null;

            return new ValidationResult("Select a option.",
                new[] { validationContext.MemberName });
        }
    }
}