using Angy.BackEnd.Kharonte.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.BackEnd.Kharonte.Data
{
    public class KharonteContext : DbContext
    {
        public KharonteContext(DbContextOptions<KharonteContext> options) : base(options)
        {
        }

        public virtual DbSet<PendingPhoto> PendingPhotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var historicBuilder = modelBuilder.Entity<PendingPhoto>();

            historicBuilder
                .HasKey(product => product.Id);
        }
    }
}