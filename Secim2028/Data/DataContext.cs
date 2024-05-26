global using Microsoft.EntityFrameworkCore;
using Secim2028.Models;

namespace Secim2028.Data
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _config;
        public DataContext(DbContextOptions<DataContext> options , IConfiguration config) : base(options)
        {
            _config = config;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetSection("ConnectionStrings:cnctstring").Value;
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.username)
                .HasMaxLength(30);
        }

        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler {  get; set; }
        public DbSet<Sandik> Sandiklar { get; set; }
        public DbSet<OyParti> PartiOylar { get; set; }
        public DbSet<OyAday> AdayOylar { get; set; }
        public DbSet<AdayCumhurbaskani> Adaylar { get; set; }
        public DbSet<SiyasiParti> Partiler { get; set; }
        public DbSet<Ittifak> Ittifaklar { get; set; }
        public DbSet<User> User { get; set; }







    }
}
