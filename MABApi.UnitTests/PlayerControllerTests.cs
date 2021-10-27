using MABApi.Controllers;
using MABApi.Interfaces;
using MABApi.Objects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MABApi.UnitTests
{
    public class PlayerControllerTests : TestsBase
    {
        [Fact]
        public void GetCalled_ListOfPlayersReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

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
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var expectedPlayers = new Player[]
            {
                new Player{FirstName="Joe",LastName="Bloggs",Email="joe@bloggs.com"}
            };

            var result = controller.Get("1").Result;

            var actualPlayers = new List<Player>();
            foreach (var player in (IEnumerable<Player>)result.Value)
            {
                actualPlayers.Add(player);
            }

            for (int i = 0; i < expectedPlayers.Length; i++)
            {
                Assert.Equal(expectedPlayers[i].ID, actualPlayers[i].ID);
                Assert.Equal(expectedPlayers[i].FirstName, actualPlayers[i].FirstName);
                Assert.Equal(expectedPlayers[i].LastName, actualPlayers[i].LastName);
                Assert.Equal(expectedPlayers[i].Email, actualPlayers[i].Email);
            }
        }

        [Fact]
        public void GetCalledWithId_IdDoesNotExist_NotFoundReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var result = controller.Get("99").Result;
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void PutCalled_RequestValid_OkReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                ID = 1,
                FirstName = "edit",
                LastName = "edit",
                Email = "edit@test.com"
            };

            var result = controller.Put(player).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PutCalled_IdNotFound_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                ID=99,
                FirstName = "edit",
                LastName = "edit",
                Email = "edit@test.com"
            };

            var result = controller.Put(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_IdMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                FirstName = "edit",
                LastName = "edit",
                Email = "edit@test.com"
            };

            var result = controller.Put(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_FirstNameMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                ID = 1,
                LastName = "edit",
                Email = "edit@test.com"
            };

            var result = controller.Put(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_LastNameMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                ID=1,
                FirstName = "edit",
                Email = "edit@test.com"
            };

            var result = controller.Put(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutCalled_EmailInvalid_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                ID = 1,
                FirstName = "edit",
                LastName = "edit",
                Email = "edittest.com"
            };

            var result = controller.Put(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_RequestValid_OkReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                FirstName = "create",
                LastName = "create",
                Email = "create@test.com"
            };

            var result = controller.Post(player).Result;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void PostCalled_FirstNameMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                LastName = "create",
                Email = "create@test.com"
            };

            var result = controller.Post(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_LastNameMissing_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                FirstName = "create",
                Email = "create@test.com"
            };

            var result = controller.Post(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PostCalled_EmailInvalid_BadRequestReturned()
        {
            var mockDb = this.GetMockPopulatedDB();
            var controller = new PlayerController(new Mock<ILogger<PlayerController>>().Object, mockDb.Object);

            var player = new Player
            {
                FirstName = "create",
                LastName = "create",
                Email = "createtest.com"
            };

            var result = controller.Post(player).Result;

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
