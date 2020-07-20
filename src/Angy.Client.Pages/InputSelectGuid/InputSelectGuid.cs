using System;
using Microsoft.AspNetCore.Components.Forms;

namespace Angy.Client.Pages.InputSelectGuid
{
    public class InputSelectGuid<T> : InputSelect<T>
    {
        protected override bool TryParseValueFromString(string value, out T result, out string? validationErrorMessage)
        {
            if (typeof(T) != typeof(Guid)) 
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            
            if (Guid.TryParse(value, out var resultGuid))
            {
                result = (T) (object) resultGuid;
                validationErrorMessage = null;
                return true;
            }

            result = default!;
            validationErrorMessage = "The chosen value is not a valid.";
            return false;

        }
    }
}