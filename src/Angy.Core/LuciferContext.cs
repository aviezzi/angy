using Angy.Model.Model;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var productBuilder = modelBuilder.Entity<Product>();

            productBuilder
                .HasKey(p => p.Id);

            productBuilder
                .HasIndex(p => p.Name)
                .IsUnique();

            productBuilder
                .HasOne(p => p.MicroCategory)
                .WithMany(m => m.Products)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey("MicroCategoryId");

            var microCategoryBuilder = modelBuilder.Entity<MicroCategory>();

            microCategoryBuilder
                .HasKey(m => m.Id);

            microCategoryBuilder
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}