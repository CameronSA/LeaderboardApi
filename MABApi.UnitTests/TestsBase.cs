using MABApi.Interfaces;
using MABApi.Objects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MABApi.UnitTests
{
    public class TestsBase
    {
        protected Mock<IDatabaseContext> GetMockPopulatedDB()
        {
            IEnumerable<Player> players = new Player[]
            {
                new Player{FirstName="Joe",LastName="Bloggs",Email="joe@bloggs.com"},
                new Player{FirstName="John",LastName="Smith",Email="john@smith.com"},
                new Player{FirstName="Monty",LastName="Python",Email="monty@python.com"},
                new Player{FirstName="Dave",LastName="Bob",Email="dave@bob.com"},
            };

            IEnumerable<LeaderboardItem> leaderboard = new LeaderboardItem[]
            {
                new LeaderboardItem{PlayerId=1, GamesPlayed=2, TotalScore=6},
                new LeaderboardItem{PlayerId=2, GamesPlayed=3, TotalScore=7},
                new LeaderboardItem{PlayerId=3, GamesPlayed=4, TotalScore=8}
            };

            var mockDb = new Mock<IDatabaseContext>();
            mockDb.Setup(x => x.SaveChangesAsync());
            mockDb.Setup(x => x.GetPlayers()).Returns(Task.FromResult(players));
            
            for(int i=0; i<players.Count(); i++)
            {
                mockDb.Setup(x => x.GetPlayer(i+1)).Returns(Task.FromResult(players.ToList()[i]));
            }

            mockDb.Setup(x => x.GetPlayer(99)).Throws(new Exception());

            mockDb.Setup(x => x.EditPlayer(It.IsAny<Player>())).Returns(Task.FromResult(true));
            mockDb.Setup(x => x.CreatePlayer(It.IsAny<Player>())).Returns(Task.FromResult(true));
            mockDb.Setup(x => x.DeletePlayer(It.IsAny<Player>())).Returns(Task.FromResult(true));

            mockDb.Setup(x => x.GetLeaderboard()).Returns(Task.FromResult(leaderboard));

            for (int i = 0; i < leaderboard.Count(); i++)
            {
                mockDb.Setup(x => x.GetLeaderboardItem(i + 1)).Returns(Task.FromResult(leaderboard.ToList()[i]));
            }

            mockDb.Setup(x => x.GetLeaderboardItem(99)).Throws(new Exception());

            mockDb.Setup(x => x.EditLeaderboardItem(It.IsAny<LeaderboardItem>())).Returns(Task.FromResult(true));
            mockDb.Setup(x => x.CreateLeaderboardItem(It.IsAny<LeaderboardItem>())).Returns(Task.FromResult(true));
            mockDb.Setup(x => x.DeleteLeaderboardItem(It.IsAny<LeaderboardItem>())).Returns(Task.FromResult(true));

            return mockDb;
        }
    }
}
