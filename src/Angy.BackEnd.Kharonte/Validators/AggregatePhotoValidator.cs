using System.Collections.Generic;
using System.Linq;
using Angy.BackEnd.Kharonte.Abstract;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Validators
{
    public class AggregatePhotoValidator : IAggregateValidator
    {
        readonly IEnumerable<PhotoValidator> _validators;

        public AggregatePhotoValidator(IEnumerable<PhotoValidator> validators)
        {
            _validators = validators;
        }

        public Result<IEnumerable<Photo>, IEnumerable<Model.Error>> Validate(IEnumerable<Photo> photos)
        {
            var success = new List<Photo>();
            var errors = new List<Model.Error>();

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

            return new Result<IEnumerable<Photo>, IEnumerable<Model.Error>>(success, errors);
        }

        IEnumerable<Model.Error> Validate(Photo photo) =>
            from validator in _validators
            select validator.Validate(photo)
            into result
            where result.HasError()
            select result.Error;
    }
}