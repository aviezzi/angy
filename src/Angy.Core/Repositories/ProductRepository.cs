using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Angy.Core.Abstract;
using Angy.Core.Extensions;
using Angy.Core.Specifications;
using Angy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.Core.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly LuciferContext _context;

        public ProductRepository(LuciferContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAll() => await _context.Products.Include(p => p.MicroCategory).ToListAsync();

        public async Task<Product> GetOne(Guid id) => await _context.Products.Specify(new ProductIdSpecification(id)).SingleOrDefaultAsync();

        public async Task<Product> Create(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(Guid id, Product entity)
        {
            var product = await _context.Products.Specify(new ProductIdSpecification(id)).FirstOrDefaultAsync();
            var micro = await _context.MicroCategories.FirstOrDefaultAsync(m => m.Id == entity.MicroCategory.Id);

            product.Name = entity.Name;
            product.Description = entity.Description;
            product.MicroCategory = micro;

            await _context.SaveChangesAsync();

            return product;
        }

        public Task<Product> Delete(Guid id) => throw new NotImplementedException();
    }
}