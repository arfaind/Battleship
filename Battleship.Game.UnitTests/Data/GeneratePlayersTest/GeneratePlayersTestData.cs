using Battleship.Game.Contracts.Models;
using Battleship.Game.UnitTests.Models;
using Battleship.Game.UnitTests.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.UnitTests.Data.GeneratePlayersTest
{
    public class GeneratePlayersTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            var data = JsonFileReader.Read<GenericTestModel<GeneratePlayersInputModel, IList<Player>>[]>("Data.GeneratePlayersTest.GeneratePlayersData.json");
            foreach (var dt in data)
            {
                yield return new object[] { dt.Input, dt.Output };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
