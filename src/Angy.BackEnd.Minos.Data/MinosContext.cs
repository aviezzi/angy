using Angy.BackEnd.Minos.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Angy.BackEnd.Minos.Data
{
    public class MinosContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Story> Stories { get; set; }

        public MinosContext(DbContextOptions<MinosContext> options) : base(options)
        {
        }

        public void Upsert(object entity) => ChangeTracker.TrackGraph(entity, e => e.Entry.State = e.Entry.IsKeySet ? EntityState.Modified : EntityState.Added);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SettingBuilder
            var settingBuilder = modelBuilder.Entity<Setting>();

            settingBuilder
                .HasKey(setting => setting.Id);

            // StoryBuilder
            var storyBuilder = modelBuilder.Entity<Story>();

            storyBuilder
                .HasKey(photo => photo.Id);

            storyBuilder
                .HasOne(error => error.Setting)
                .WithMany(settings => settings.Stories)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(error => error.SettingId);

            storyBuilder
                .HasOne(error => error.Photo)
                .WithMany(photo => photo.Stories)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(error => error.PhotoId);
        }
    }
}