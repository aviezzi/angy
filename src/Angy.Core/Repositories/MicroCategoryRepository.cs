using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Core.Abstract;
using Angy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.Core.Repositories
{
    public class MicroCategoryRepository : IRepository<MicroCategory>
    {
        private readonly LuciferContext _context;

        public MicroCategoryRepository(LuciferContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MicroCategory>> GetAll() => await _context.MicroCategories.ToListAsync();

        public Task<MicroCategory> GetOne(Guid id) => throw new NotImplementedException();

        public Task<MicroCategory> Create(MicroCategory entity) => throw new NotImplementedException();

        public Task<MicroCategory> Update(Guid id, MicroCategory entity) => throw new NotImplementedException();

        public Task<MicroCategory> Delete(Guid id) => throw new NotImplementedException();
    }
}