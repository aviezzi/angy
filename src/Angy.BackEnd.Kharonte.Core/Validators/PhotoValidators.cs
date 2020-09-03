using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;
using Angy.Model.Abstract;

namespace Angy.BackEnd.Kharonte.Core.Validators
{
    public abstract class PhotoValidator
    {
        public abstract IResult<Unit, Errors.Error> Validate(Photo photo);

        public sealed class Filename : PhotoValidator
        {
            public override IResult<Unit, Errors.Error> Validate(Photo photo) => new Regex(@"^[A-Za-z0-9]+$").IsMatch(photo.Filename)
                ? Result<Errors.Error>.Success(Unit.Value)
                : Result<Unit>.Error(new Errors.Error.InvalidFileName(photo) as Errors.Error);
        }

        public sealed class Extension : PhotoValidator
        {
            readonly IEnumerable<string> _extensions;

            public Extension(IEnumerable<string> extensions)
            {
                _extensions = extensions;
            }

            public override IResult<Unit, Errors.Error> Validate(Photo photo) => _extensions.Contains(photo.Extension)
                ? Result<Errors.Error>.Success(Unit.Value)
                : Result<Unit>.Error(new Errors.Error.InvalidExtension(photo) as Errors.Error);
        }
    }
}