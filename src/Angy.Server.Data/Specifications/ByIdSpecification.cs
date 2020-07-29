using System;
using System.Collections.Generic;
using System.Linq;
using Angy.Model;

namespace Angy.Server.Data.Specifications
{
    public class ByIdSpecification<T> : SpecificationBase<T> where T : EntityBase
    {
        public ByIdSpecification(Guid id)
        {
            Criteria = entity => entity.Id == id;
        }
    }

    public class ByIdsSpecification<TEntity> : SpecificationBase<TEntity> where TEntity : EntityBase
    {
        public ByIdsSpecification(IEnumerable<Guid> ids)
        {
            Criteria = entity => ids.Contains(entity.Id);
        }
    }
}