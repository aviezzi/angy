using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Model;
using Angy.Server.Data.Abstract;
using Angy.Server.Data.Extensions;
using Angy.Server.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Angy.Server.Data.Repositories
{
    public class AttributeDescriptionRepository : IAttributeDescriptionRepository
    {
        readonly LuciferContext _context;

        public AttributeDescriptionRepository(LuciferContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttributeDescription>> GetAll() => await _context.AttributeDescriptions.ToListAsync();

        public async Task<AttributeDescription> GetOne(Guid id) => await _context.AttributeDescriptions.Specify(new AttributeDescriptionGetByIdSpecification(id)).FirstOrDefaultAsync();

        public Task<AttributeDescription> Create(AttributeDescription entity) => throw new NotImplementedException();

        public Task<AttributeDescription> Update(Guid id, AttributeDescription entity) => throw new NotImplementedException();
        
        public Task<AttributeDescription> Delete(Guid id) => throw new NotImplementedException();

        public async Task<IEnumerable<AttributeDescription>> GetByProductId(Guid productId) => await _context.AttributeDescriptions.Specify(new AttributeDescriptionGetByProductIdSpecification(productId)).ToListAsync();
    }
}