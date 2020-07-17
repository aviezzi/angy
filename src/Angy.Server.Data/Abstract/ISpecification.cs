using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Angy.Server.Data.Abstract
{
    interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        ICollection<Expression<Func<T, object>>> Includes { get; }
        ICollection<string> IncludeStrings { get; }
    }
}