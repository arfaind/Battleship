using Battleship.Game.Contracts.Models;
using Battleship.Game.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Battleship.Game.UnitTests
{
    [TestClass]
    public class GameServiceTests
    {
        private readonly IGameService _testObject;
        private readonly IGameService _testObjectWithMockPlayerService;
        private Mock<IPlayerService> _playerService;
        private Mock<IUtilService> _utilServiceMock;

        public GameServiceTests()
        {
            _playerService = new Mock<IPlayerService>();
            _utilServiceMock = new Mock<IUtilService>();
            _testObjectWithMockPlayerService = new GameService(_playerService.Object, _utilServiceMock.Object);
            _testObject = new GameService(new PlayerService(_utilServiceMock.Object), _utilServiceMock.Object);
            _playerService.Setup(x => x.Play(It.IsAny<IList<Player>>(), It.IsAny<Player>())).Returns(true);
            _playerService.Setup(x => x.GeneratePlayers(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new List<Player>()
            {
                new Player(), new Player()
            });
        }


        [Theory]
        [InlineData(3,3,2)]
        [InlineData(3,3,1)]
        public void SetupGameTest(int width, int height, int computerPlayers)
        {
            _testObject.SetupGame(width, height, computerPlayers);
            Assert.AreEqual(_testObject.Players.Count(x => x.IsComputer), computerPlayers);
        }

        [Fact]
        public void ArrangeShipsTest()
        {
            _testObject.SetupGame(4, 4, 2);
            _utilServiceMock.Setup(x => x.RandomPosition(It.IsAny<IList<Cell>>())).Returns(new Cell(null, 0, 0));
            _testObject.ArrangeShips(1, 3, 1);
        }
        
        [Fact]
        public void ArrangeShipsFailedTest()
        {
            _testObject.SetupGame(4, 4, 2);
            _utilServiceMock.Setup(x => x.RandomPosition(It.IsAny<IList<Cell>>())).Returns(new Cell(null, 0, 0));
            Assert.ThrowsException<Exception>(() => _testObject.ArrangeShips(1, 3, 7));
        }

        [Fact]
        public void ArrangeShipsFailedTest2()
        {
            _testObject.SetupGame(4, 4, 2);
            _utilServiceMock.Setup(x => x.RandomPosition(It.IsAny<IList<Cell>>())).Returns(new Cell(null, 0, 0));
            _testObject.ArrangeShips(1, 3, 1);
            _utilServiceMock.Setup(x => x.RandomPosition(It.IsAny<IList<Cell>>())).Returns(new Cell(null, 3, 1));
            _testObject.ArrangeShips(1, 3, 1);


            _utilServiceMock.Setup(x => x.RandomPosition(It.IsAny<IList<Cell>>())).Returns((IList<Cell> cells) =>
            {
                var randomFunctionService = new RandomFunctionService();
                var randomIndex = randomFunctionService.Next(0, cells.Count);
                return cells[randomIndex];
            });
            Assert.ThrowsException<Exception>(() => _testObject.ArrangeShips(1, 4, 1));
        }

        [Fact]
        public void StartPlayFailedTest()
        {
            _testObject.Players = new List<Player>() { null};
            Assert.ThrowsException<InvalidOperationException>(() => _testObject.StartPlay());
        }

        [Fact]
        public void StartPlayTest()
        {
            _testObjectWithMockPlayerService.SetupGame(4, 4, 2);
            _testObjectWithMockPlayerService.StartPlay();
        }
    }
}
