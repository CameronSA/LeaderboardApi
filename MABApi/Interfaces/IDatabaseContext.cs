using MABApi.Objects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MABApi.Interfaces
{
    public interface IDatabaseContext
    {
        public Task SaveChangesAsync();

        public Task<IEnumerable<Player>> GetPlayers();

        public Task<Player> GetPlayer(int id);

        public Task<bool> EditPlayer(Player player);

        public Task<bool> CreatePlayer(Player player);

        public Task<bool> DeletePlayer(Player player);

        public Task<IEnumerable<LeaderboardItem>> GetLeaderboard();

        public Task<LeaderboardItem> GetLeaderboardItem(int id);

        public Task<bool> EditLeaderboardItem(LeaderboardItem leaderboardItem);

        public Task<bool> CreateLeaderboardItem(LeaderboardItem leaderboardItem);

        public Task<bool> DeleteLeaderboardItem(LeaderboardItem leaderboardItem);
    }
}
