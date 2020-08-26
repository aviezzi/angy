using Angy.BackEnd.Kharonte.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.BackEnd.Kharonte.Data
{
    public class KharonteContext : DbContext
    {
        public KharonteContext(DbContextOptions<KharonteContext> options) : base(options)
        {
        }

        public virtual DbSet<Photo> PendingPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var photoBuilder = modelBuilder.Entity<Photo>();

            photoBuilder
                .HasKey(product => product.Id);

            photoBuilder
                .Property(t => t.Filename)
                .IsRequired();

            photoBuilder
                .Property(t => t.Extension)
                .IsRequired();

            photoBuilder
                .Property(t => t.Inserted)
                .IsRequired();
        }
    }
}