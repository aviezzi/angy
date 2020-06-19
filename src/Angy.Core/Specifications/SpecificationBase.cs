using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Angy.Core.Abstract;

namespace Angy.Core.Specifications
{
    public abstract class SpecificationBase<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public ICollection<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public ICollection<string> IncludeStrings { get; } = new List<string>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression) => Includes.Add(includeExpression);
        protected virtual void AddInclude(string includeString) => IncludeStrings.Add(includeString);
    }
}