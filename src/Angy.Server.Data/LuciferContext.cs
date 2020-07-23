using Angy.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.Server.Data
{
    public class LuciferContext : DbContext
    {
        public LuciferContext(DbContextOptions<LuciferContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<MicroCategory> MicroCategories { get; set; }
        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<AttributeDescription> AttributeDescriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var productBuilder = modelBuilder.Entity<Product>();

            productBuilder
                .HasKey(p => p.Id);

            productBuilder
                .HasOne(p => p.MicroCategory)
                .WithMany(m => m.Products)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey("MicroCategoryId");

            var microCategoryBuilder = modelBuilder.Entity<MicroCategory>();

            microCategoryBuilder
                .HasKey(m => m.Id);
            
            microCategoryBuilder
                .HasOne(c => c.MicroCategoryParent)
                .WithMany()
                .IsRequired(required: false)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey("MicroCategoryParentId");

            var attributeBuilder = modelBuilder.Entity<Attribute>();

            attributeBuilder
                .HasKey(a => a.Id);

            var attributeDescriptionBuilder = modelBuilder.Entity<AttributeDescription>();

            attributeDescriptionBuilder
                .HasOne(ad => ad.Attribute)
                .WithMany(ad => ad.Descriptions)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey("AttributeId");

            attributeDescriptionBuilder
                .HasOne(a => a.Product)
                .WithMany(a => a.Descriptions)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey("ProductId");
        }
    }
}