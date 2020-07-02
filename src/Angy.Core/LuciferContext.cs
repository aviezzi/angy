using System.Threading.Tasks;
using Angy.Core.Abstract;
using Angy.Shared.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Angy.Core
{
    public class LuciferContext : DbContext, ILuciferContext
    {
        private IDbContextTransaction _transaction;

        public LuciferContext(DbContextOptions<LuciferContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<MicroCategory> MicroCategories { get; set; }

        public async Task BeginTransactionAsync() => _transaction = await Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
    }
}