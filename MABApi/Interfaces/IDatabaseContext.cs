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
        public DbSet<Player> Players { get; set; }

        public DbSet<LeaderboardItem> Leaderboard { get; set; }

        public Task SaveChangesAsync();
    }
}
