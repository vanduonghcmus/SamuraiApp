using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;
using Microsoft.Extensions.Logging;


namespace SamuraiApp.Data
{
    public class SamuraiContext:DbContext
    {
        public SamuraiContext()
        {

        }
        //public class SamuraiContextNoTracking : DbContext
        //{
        //    public SamuraiContextNoTracking()
        //    {
        //        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //    }
        //}
        public virtual DbSet<Samurais> Samurais { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<Clans> Clans { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<SamuraiBattle> SamuraiBattles { get; set; }
        public DbSet<SamuraiBattleStat> SamuraiBattleStats { get; set; }

        // Create a field called Console
        public static readonly ILoggerFactory ConsoleLoggerFactory
            =LoggerFactory.Create(builder=>
            {
                builder
                    .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
                    .AddConsole();
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                 .UseLoggerFactory(ConsoleLoggerFactory)
                 .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog= SamuraiAppData");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quotes>(entity =>
            {
                entity.HasIndex(x => x.SamuraiId);
                entity.HasOne(x => x.Samurai).WithMany(x => x.Quote).HasForeignKey(x => x.SamuraiId);
            });
            modelBuilder.Entity<Samurais>(entity =>
            {
                entity.HasIndex(x => x.ClanId);
                entity.HasOne(x => x.Clan).WithMany(x => x.Samurais).HasForeignKey(x => x.ClanId);
            });
            modelBuilder.Entity<SamuraiBattle>(entity =>
            {
                entity.HasKey(x => new { x.BattleId, x.SamuraiId });
            });
            modelBuilder.Entity<Horse>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.ToTable("Horses");
            });
            modelBuilder.Entity<SamuraiBattleStat>(entity =>
            {
                entity.HasNoKey().ToView("SamuraiBattleStats");
            });
            // we're not done yet though!
        }
    }
}
