using Battleship.Game.Contracts.Models;
using Battleship.Game.UnitTests.Models;
using Battleship.Game.UnitTests.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.UnitTests.Data.RandomPositionTest
{
    internal class RandomPositionTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var data = JsonFileReader.Read<GenericTestModel<IList<Cell>, Cell>[]>("Data.RandomPositionTest.RandomPositionCellData.json");
            foreach (var dt in data)
            {
                yield return new object[] { dt.Input, dt.Output};
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
