using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Server.Data.Abstract;
using Angy.Server.Data.Extensions;
using Angy.Server.Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Angy.Server.Data.Repositories
{
    public class AttributeRepository : IRepository<Model.Attribute>
    {
        readonly LuciferContext _context;

        public AttributeRepository(LuciferContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Model.Attribute>> GetAll() => await _context.Attributes.Include(p => p.Descriptions).ToListAsync();

        public async Task<Model.Attribute> GetOne(Guid id) => await _context.Attributes.Specify(new AttributeIdSpecification(id)).SingleOrDefaultAsync();

        public async Task<Model.Attribute> Create(Model.Attribute attribute)
        {
            await _context.Attributes.AddAsync(attribute);
            await _context.SaveChangesAsync();

            return attribute;
        }

        public async Task<Model.Attribute> Update(Guid id, Model.Attribute attribute)
        {
            var entity = await _context.Attributes.Specify(new AttributeIdSpecification(id)).FirstOrDefaultAsync();
            entity.Name = attribute.Name;

            await _context.SaveChangesAsync();

            return attribute;
        }

        public Task<Model.Attribute> Delete(Guid id) => throw new NotImplementedException();
    }
}