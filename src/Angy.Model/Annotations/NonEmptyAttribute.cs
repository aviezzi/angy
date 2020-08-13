namespace Angy.Model.Annotations
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Property)]
    public class NonEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value) =>
            value switch
            {
                ICollection collection => collection.Count > 0,
                IEnumerable enumerable => enumerable.Cast<object>().Any(),
                _ => false
            };
    }
}