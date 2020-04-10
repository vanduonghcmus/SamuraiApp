using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;


namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public SamuraiContext()
        {

        }
        public SamuraiContext(DbContextOptions<SamuraiContext> options)
        {

        }
        public virtual DbSet<Samurais> Samurais { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<Clans> Clans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // To protect potentially sensitive infomation in your connection string, 
                optionsBuilder.UseSqlServer(
                "Data Source=(localdb)\\mssqllocaldb; Initial Catalog = SamuraiAppData"
                );
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quotes>(entity =>
            {
                entity.HasIndex(x => x.SamuraiId);
                entity.HasOne(x => x.Samurai).WithMany(x => x.Quotes).HasForeignKey(x => x.SamuraiId);
            });
            modelBuilder.Entity<Samurais>(entity =>
            {
                entity.HasIndex(x => x.ClanId);
                entity.HasOne(x => x.Clan).WithMany(x => x.Samurais).HasForeignKey(x => x.ClanId);
            });
            

        }
    }
}
