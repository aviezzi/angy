using System.Collections.Generic;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.BackEnd.Kharonte.Model;

namespace Angy.BackEnd.Kharonte.Abstract
{
    public interface IAggregateValidator
    {
        Angy.Model.Result<IEnumerable<Photo>, IEnumerable<Error>> Validate(IEnumerable<Photo> photo);
    }
}