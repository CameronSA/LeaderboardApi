using MABApi.Objects;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace MABApi.Database
{
    public class DatabaseSeed
    {
        public static void Seed(DatabaseContext context)
        {
            // check database exists
            context.Database.EnsureCreated();

            // if database has been seeded, do nothing
            if(context.Players.Any())
            {
                return;
            }

            var players = new Player[]
            {
                new Player{FirstName="Joe",LastName="Bloggs",Email="joe@bloggs.com"},
                new Player{FirstName="John",LastName="Smith",Email="john@smith.com"},
                new Player{FirstName="Monty",LastName="Python",Email="monty@python.com"}
            };

            foreach (var player in players)
            {
                context.Players.Add(player);
            }

            var leaderboard = new LeaderboardItem[]
            {
                new LeaderboardItem{PlayerId=1, GamesPlayed=2, TotalScore=6},
                new LeaderboardItem{PlayerId=2, GamesPlayed=3, TotalScore=7},
                new LeaderboardItem{PlayerId=3, GamesPlayed=4, TotalScore=8}
            };

            foreach (var item in leaderboard)
            {
                context.Leaderboard.Add(item);
            }

            context.SaveChanges();
        }
    }
}