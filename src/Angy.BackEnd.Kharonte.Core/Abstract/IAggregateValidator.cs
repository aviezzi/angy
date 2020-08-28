using System.Collections.Generic;
using Angy.BackEnd.Kharonte.Data.Model;
using Angy.Model;

namespace Angy.BackEnd.Kharonte.Core.Abstract
{
    public interface IAggregateValidator
    {
        Result<IEnumerable<Photo>, IEnumerable<Error>> Validate(IEnumerable<Photo> photo);
    }
}