using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Angy.Server.Data.Abstract
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        ICollection<Expression<Func<T, object>>> Includes { get; }
        ICollection<string> IncludeStrings { get; }
    }
}