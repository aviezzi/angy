using System.Linq;
using System.Text.RegularExpressions;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.BackEnd.Kharonte.Options;
using Angy.Model;
using Microsoft.Extensions.Options;

namespace Angy.BackEnd.Kharonte.Validators
{
    public abstract class PhotoValidator
    {
        public abstract Result<Unit, Model.Error> Validate(Photo photo);

        public sealed class Filename : PhotoValidator
        {
            public override Result<Unit, Model.Error> Validate(Photo photo) => new Regex(@"^[A-Za-z0-9]+$").IsMatch(photo.Filename)
                ? Result<Model.Error>.Success(Unit.Value)
                : Result<Unit>.Error(new Model.Error.InvalidFileName(photo.Filename, photo.Extension) as Model.Error);
        }

        public sealed class Extension : PhotoValidator
        {
            readonly KharonteOptions _options;

            public Extension(IOptions<KharonteOptions> options)
            {
                _options = options.Value;
            }

            public override Result<Unit, Model.Error> Validate(Photo photo) => _options.SupportedExtensions.Contains(photo.Extension)
                ? Result<Model.Error>.Success(Unit.Value)
                : Result<Unit>.Error(new Model.Error.InvalidExtension(photo.Filename, photo.Extension) as Model.Error);
        }
    }
}