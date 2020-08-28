using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Core.Validators
{
    public abstract class PhotoValidator
    {
        public abstract Result<Unit, Error> Validate(Photo photo);

        public sealed class Filename : PhotoValidator
        {
            public override Result<Unit, Error> Validate(Photo photo) => new Regex(@"^[A-Za-z0-9]+$").IsMatch(photo.Filename)
                ? Result<Error>.Success(Unit.Value)
                : Result<Unit>.Error(new Error.InvalidFileName(photo) as Error);
        }

        public sealed class Extension : PhotoValidator
        {
            readonly IEnumerable<string> _extensions;

            public Extension(IEnumerable<string> extensions)
            {
                _extensions = extensions;
            }

            public override Result<Unit, Error> Validate(Photo photo) => _extensions.Contains(photo.Extension)
                ? Result<Error>.Success(Unit.Value)
                : Result<Unit>.Error(new Error.InvalidExtension(photo) as Error);
        }
    }
}