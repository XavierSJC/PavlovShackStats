using Microsoft.EntityFrameworkCore;
using PavlovShackStats.Models;

namespace PavlovShackStats.Data
{
    public class PavlovShackStatsContext : DbContext
    {
        public PavlovShackStatsContext(DbContextOptions<PavlovShackStatsContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Map> Maps { get; set; } = null!;
        public DbSet<GameMode> GameModes { get; set; }
        public DbSet<Match> Matchers { get; set; }
        public DbSet<MatchPlayerStats> MatchersPlayerStats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Map>().ToTable("Map");
            modelBuilder.Entity<GameMode>().ToTable("GameMode");
            modelBuilder.Entity<Match>().ToTable("Match");
            modelBuilder.Entity<MatchPlayerStats>().ToTable("MatchPlayerStats");
        }
    }
}
