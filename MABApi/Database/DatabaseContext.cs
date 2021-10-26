using MABApi.Interfaces;
using MABApi.Objects;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MABApi.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<LeaderboardItem> Leaderboard { get; set; }

        public async Task SaveChangesAsync()
        {
            await this.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<LeaderboardItem>().ToTable("Leaderboard");
        }
    }
}