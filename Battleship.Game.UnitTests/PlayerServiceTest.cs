using Battleship.Game.Contracts.Models;
using Battleship.Game.Interfaces;
using Battleship.Game.UnitTests.Data.GeneratePlayersTest;
using Battleship.Game.UnitTests.Models;
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
    public class PlayerServiceTests
    {
        private readonly IPlayerService _testObject;
        private Mock<IUtilService> _utilServiceMock;

        public PlayerServiceTests()
        {
            _utilServiceMock = new Mock<IUtilService>();
            _testObject = new PlayerService(_utilServiceMock.Object);
        }

        [Theory]
        [ClassData(typeof(GeneratePlayersTestData))]
        public void GeneratePlayersTest(GeneratePlayersInputModel input, IList<Player> expectedPlayers)
        {
            var output = _testObject.GeneratePlayers(input.BoardWidth, input.BoardHeight, input.NoOfComputerPlayers);
            var expectedComputerPlayers = expectedPlayers.Count(x => x.IsComputer);
            var outputComputerPlayers = output.Count(x => x.IsComputer);
            Assert.AreEqual(expectedComputerPlayers, outputComputerPlayers);
            foreach (var op in output)
            {
                Assert.AreEqual(input.BoardWidth, op.Board.Dimension.Width);
                Assert.AreEqual(input.BoardHeight, op.Board.Dimension.Height);
            }
        }

        [Fact]
        public void PlayTest()
        {
            _utilServiceMock.Setup(x => x.ReceiveInput(It.IsAny<Dimension>())).Returns(new Cell(null, 0, 0));
            var players = new List<Player>();
            players.Add(new Player()
            {
                IsComputer = false,
                Id = Guid.NewGuid().ToString(),
                Board = new Board(3,3)
            });

            players.Add(new Player()
            {
                IsComputer = false,
                Id = Guid.NewGuid().ToString(),
                Board = new Board(3, 3)
            });

            foreach (var player in players)
            {
                player.Board.SetupBoard();
                player.Board.ArrangeShip(new Cell(null, 0, 0), 1, 2);
            }

            players[0].Board.AttackCell(new Cell(null, 1, 0));

            var gameOver = _testObject.Play(players, players[1]);
            Assert.IsTrue(gameOver);
        }
    }
}
