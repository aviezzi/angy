using Angy.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.Core
{
    public class LuciferContext : DbContext
    {
        public LuciferContext(DbContextOptions<LuciferContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<MicroCategory> MicroCategories { get; set; }
    }
}