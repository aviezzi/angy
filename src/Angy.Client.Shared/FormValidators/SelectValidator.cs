using System;
using System.ComponentModel.DataAnnotations;

namespace Angy.Client.Shared.FormValidators
{
    public class SelectValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext) =>
            Guid.TryParse(value.ToString(), out var guid) && guid != Guid.Empty
                ? null
                : new ValidationResult("Select a option.", new[] { validationContext.MemberName });
    }
}