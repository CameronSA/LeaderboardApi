using MABApi.Controllers;
using MABApi.Interfaces;
using MABApi.Objects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MABApi.UnitTests
{
    public class PlayerControllerTests
    {
        [Fact]
        public void GetCalled_ListOfPlayersReturned()
        {
            var mockDb = this.GetMockDB().Object;
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb);

            var expectedPlayers = new Player[]
            {
                new Player{FirstName="Joe",LastName="Bloggs",Email="joe@bloggs.com"},
                new Player{FirstName="John",LastName="Smith",Email="john@smith.com"},
                new Player{FirstName="Monty",LastName="Python",Email="monty@python.com"}
            };

            var result = controller.Get(string.Empty).Result;

            var actualPlayers = new List<Player>();
            foreach(var player in (IEnumerable<Player>)result.Value)
            {
                actualPlayers.Add(player);
            }

            for(int i = 0; i < expectedPlayers.Length; i++)
            {
                Assert.Equal(expectedPlayers[i].ID, actualPlayers[i].ID);
                Assert.Equal(expectedPlayers[i].FirstName, actualPlayers[i].FirstName);
                Assert.Equal(expectedPlayers[i].LastName, actualPlayers[i].LastName);
                Assert.Equal(expectedPlayers[i].Email, actualPlayers[i].Email);
            }
        }

        [Fact]
        public void GetCalledWithId_IdExists_PlayerReturned()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void GetCalledWithId_IdDoesNotExist_NotFoundReturned()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PutCalled_RequestValid_OkReturned()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PutCalled_RequestInvalid_BadRequestReturned()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PostCalled_RequestValid_OkReturned()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void PostCalled_RequestInvalid_BadRequestReturned()
        {
            throw new NotImplementedException();
        }

        private Mock<IDatabaseContext> GetMockDB()
        {
            var players = new Player[]
            {
                new Player{FirstName="Joe",LastName="Bloggs",Email="joe@bloggs.com"},
                new Player{FirstName="John",LastName="Smith",Email="john@smith.com"},
                new Player{FirstName="Monty",LastName="Python",Email="monty@python.com"}
            };


            // this needs mocking properly
            var playersDbSet = new Mock<DbSet<Player>>();


            var mockDb = new Mock<IDatabaseContext>();
            mockDb.SetupGet(x => x.Players).Returns(playersDbSet.Object);
            mockDb.SetupSet(x => x.Players = It.IsAny<DbSet<Player>>());


            return mockDb;
        }
    }
}
