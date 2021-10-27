using MABApi.Controllers;
using MABApi.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MABApi.UnitTests
{
    public class LeaderboardControllerTests : TestsBase
    {
        [Fact]
        public void GetCalled_LeaderboardReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var expectedLeaderboard = new LeaderboardItem[]
            {
                new LeaderboardItem{PlayerId=1, GamesPlayed=2, TotalScore=6},
                new LeaderboardItem{PlayerId=2, GamesPlayed=3, TotalScore=7},
                new LeaderboardItem{PlayerId=3, GamesPlayed=4, TotalScore=8}
            };

            var result = controller.Get(string.Empty).Result;

            var actualPlayers = new List<LeaderboardItem>();
            foreach (var player in (IEnumerable<LeaderboardItem>)result.Value)
            {
                actualPlayers.Add(player);
            }

            for (int i = 0; i < expectedLeaderboard.Length; i++)
            {
                Assert.Equal(expectedLeaderboard[i].ID, actualPlayers[i].ID);
                Assert.Equal(expectedLeaderboard[i].PlayerId, actualPlayers[i].PlayerId);
                Assert.Equal(expectedLeaderboard[i].GamesPlayed, actualPlayers[i].GamesPlayed);
                Assert.Equal(expectedLeaderboard[i].TotalScore, actualPlayers[i].TotalScore);
            }
        }

        [Fact]
        public void GetCalledWithId_IdExists_LeaderboardItemReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var expectedLeaderboard = new LeaderboardItem[]
            {
                new LeaderboardItem{PlayerId=1, GamesPlayed=2, TotalScore=6},
            };

            var result = controller.Get("1").Result;

            var actualPlayers = new List<LeaderboardItem>();
            foreach (var player in (IEnumerable<LeaderboardItem>)result.Value)
            {
                actualPlayers.Add(player);
            }

            for (int i = 0; i < expectedLeaderboard.Length; i++)
            {
                Assert.Equal(expectedLeaderboard[i].ID, actualPlayers[i].ID);
                Assert.Equal(expectedLeaderboard[i].PlayerId, actualPlayers[i].PlayerId);
                Assert.Equal(expectedLeaderboard[i].GamesPlayed, actualPlayers[i].GamesPlayed);
                Assert.Equal(expectedLeaderboard[i].TotalScore, actualPlayers[i].TotalScore);
            }
        }

        [Fact]
        public void GetCalledWithId_IdDoesNotExist_NotFoundReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var result = controller.Get("99").Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void PutCalled_RequestValid_OkReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { ID = 1, PlayerId = 1, GamesPlayed = 3, TotalScore = 7 };

            var result = controller.Put(leaderboardItem).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PutCalled_IdNotFound_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { ID = 99, PlayerId = 1, GamesPlayed = 3, TotalScore = 7 };

            var result = controller.Put(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_IdMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem {PlayerId = 1, GamesPlayed = 3, TotalScore = 7 };

            var result = controller.Put(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_PlayerIdMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { ID = 1, GamesPlayed = 3, TotalScore = 7 };

            var result = controller.Put(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_PlayerIdDoesNotExist_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { ID = 1, PlayerId=99, GamesPlayed = 3, TotalScore = 7 };

            var result = controller.Put(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_GamesPlayedMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { ID = 1, PlayerId = 1, TotalScore = 7 };

            var result = controller.Put(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_TotalScoreMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { ID = 1, PlayerId = 1, GamesPlayed = 3 };

            var result = controller.Put(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_RequestValid_OkReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { PlayerId = 1, GamesPlayed = 3, TotalScore = 7 };

            var result = controller.Post(leaderboardItem).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostCalled_PlayerIdMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { GamesPlayed=1, TotalScore = 4 };

            var result = controller.Post(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_PlayerIdAlreadyHasRecord_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { PlayerId = 1, GamesPlayed = 1, TotalScore = 4 };

            var result = controller.Post(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_PlayerIdDoesNotExist_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { PlayerId = 99, GamesPlayed = 1, TotalScore = 4 };

            var result = controller.Post(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_GamesPlayedMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { PlayerId = 4, TotalScore = 4 };

            var result = controller.Post(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_TotalScoreMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new LeaderboardController(new Mock<ILogger<LeaderboardController>>().Object, mockDb.Object);

            var leaderboardItem = new LeaderboardItem { PlayerId = 4, GamesPlayed = 3 };

            var result = controller.Post(leaderboardItem).Result;

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
