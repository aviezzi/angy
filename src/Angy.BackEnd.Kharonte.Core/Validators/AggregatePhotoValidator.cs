using System.Collections.Generic;
using System.Linq;
using Angy.BackEnd.Kharonte.Core.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Core.Validators
{
    public class AggregatePhotoValidator : IAggregateValidator
    {
        readonly IEnumerable<PhotoValidator> _validators;

        public AggregatePhotoValidator(IEnumerable<PhotoValidator> validators)
        {
            _validators = validators;
        }

        public Result<IEnumerable<Photo>, IEnumerable<Error>> Validate(IEnumerable<Photo> photos)
        {
            var success = new List<Photo>();
            var errors = new List<Error>();

            foreach (var photo in photos)
            {
                var temp = Validate(photo).ToList();

                if (!temp.Any())
                    success.Add(photo);
                else
                {
                    errors.AddRange(temp);
                    temp.Clear();
                }
            }

            return new Result<IEnumerable<Photo>, IEnumerable<Error>>(success, errors);
        }

        IEnumerable<Error> Validate(Photo photo) =>
            from validator in _validators
            select validator.Validate(photo)
            into result
            where result.HasError()
            select result.Error;
    }
}