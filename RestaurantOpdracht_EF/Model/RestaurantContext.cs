using Microsoft.EntityFrameworkCore;

namespace RestaurantOpdracht_EF.Model {
    public class RestaurantContext : DbContext {
        public DbSet<KlantEF> Klant { get; set; }
        public DbSet<ReservatieEF> Reservatie { get; set; }
        public DbSet<RestaurantEF> Restaurant { get; set; }
        public DbSet<TafelEF> Tafel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-2GH15DF6\\SQLEXPRESS;Initial Catalog = RestaurantWeb4DB; Integrated Security = True; TrustServerCertificate = True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<RestaurantEF>()
                .HasMany(r => r.Tafels)
                .WithOne(t => t.Restaurant)
                .HasForeignKey(t => t.RestaurantID);

            modelBuilder.Entity<RestaurantEF>()
                .HasMany(r => r.Reservaties)
                .WithOne(rs => rs.Restaurant)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KlantEF>()
                .HasMany(kl => kl.Reservaties)
                .WithOne(rs => rs.Klant)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservatieEF>()
                .HasOne(r => r.Klant)
                .WithMany(kl => kl.Reservaties)
                .HasForeignKey(r => r.KlantID);

            modelBuilder.Entity<ReservatieEF>()
                .HasOne(r => r.Restaurant)
                .WithMany(resto => resto.Reservaties)
                .HasForeignKey(r => r.RestaurantID);
        }
    }
}
