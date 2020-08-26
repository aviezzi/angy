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
        public virtual DbSet<PhotoError> PhotoErrors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var photoBuilder = modelBuilder.Entity<Photo>();

            photoBuilder
                .HasKey(photo => photo.Id);

            photoBuilder
                .Property(photo => photo.Filename)
                .IsRequired();

            photoBuilder
                .Property(photo => photo.Extension)
                .IsRequired();

            photoBuilder
                .Property(photo => photo.Inserted)
                .IsRequired();

            var errorBuilder = modelBuilder.Entity<PhotoError>();

            errorBuilder
                .HasKey(photo => photo.Id);

            errorBuilder
                .Property(photo => photo.Filename)
                .IsRequired();

            errorBuilder
                .Property(photo => photo.Extension)
                .IsRequired();

            errorBuilder
                .Property(photo => photo.Inserted)
                .IsRequired();

            errorBuilder
                .Property(photo => photo.Message)
                .IsRequired();
        }
    }
}