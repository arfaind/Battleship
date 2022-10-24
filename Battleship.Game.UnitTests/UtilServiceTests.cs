using Battleship.Game.Contracts.Models;
using Battleship.Game.Interfaces;
using Battleship.Game.UnitTests.Data.RandomPositionTest;
using Battleship.Game.UnitTests.Data.ReceiveInputTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Battleship.Game.UnitTests
{
    [TestClass]
    public class UtilServiceTests
    {
        private IUtilService _testObject;
        private Mock<IRandomFunctionService> _randomServiceMock;
        private Mock<IConsoleService> _consoleServiceMock;

        public UtilServiceTests()
        {
            _randomServiceMock = new Mock<IRandomFunctionService>();
            _consoleServiceMock = new Mock<IConsoleService>();
            _testObject = new UtilService(_randomServiceMock.Object, _consoleServiceMock.Object);

            _randomServiceMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Returns((int min, int max) =>
            {
                return 1;
            });
        }

        [Theory]
        [ClassData(typeof(RandomPositionTestData))]
        public void RandomPositionTest(IList<Cell> input, Cell expectedOutput)
        {
            var output = _testObject.RandomPosition(input);
            Assert.AreEqual(expectedOutput.Row, output.Row);
            Assert.AreEqual(expectedOutput.Col, output.Col);
        }

        [Theory]
        [ClassData(typeof(ReceiveInputTestData))]
        public void ReceiveInputTest(Dimension input, Cell expectedOutput)
        {
            _consoleServiceMock.Setup(x => x.ReadLine()).Returns("2");
            var output = _testObject.ReceiveInput(input);
            Assert.AreEqual(expectedOutput.Row, output.Row);
            Assert.AreEqual(expectedOutput.Col, output.Col);

        }
    }
}
