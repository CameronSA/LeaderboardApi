using MABApi.Interfaces;
using MABApi.Objects;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        public async Task<bool> CreateLeaderboardItem(LeaderboardItem leaderboardItem)
        {
            await this.Leaderboard.AddAsync(leaderboardItem);
            await this.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreatePlayer(Player player)
        {
            await this.Players.AddAsync(player);
            await this.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLeaderboardItem(LeaderboardItem leaderboardItem)
        {
            this.Leaderboard.Remove(leaderboardItem);
            await this.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePlayer(Player player)
        {
            this.Players.Remove(player);
            await this.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditLeaderboardItem(LeaderboardItem leaderboardItem)
        {
            this.Leaderboard.Update(leaderboardItem);
            await this.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditPlayer(Player player)
        {
            this.Players.Update(player);
            await this.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<LeaderboardItem>> GetLeaderboard()
        {
            return await this.Leaderboard.ToListAsync();
        }

        public async Task<LeaderboardItem> GetLeaderboardItem(int id)
        {
            return await this.Leaderboard.SingleAsync(x => x.ID == id);
        }

        public async Task<Player> GetPlayer(int id)
        {
            return await this.Players.SingleAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Player>> GetPlayers()
        {
            return await this.Players.ToListAsync();
        }

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