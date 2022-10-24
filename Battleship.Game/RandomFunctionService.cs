using Battleship.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class RandomFunctionService : IRandomFunctionService
    {
        private readonly static Random _randomFunction = new Random();

        public int Next(int min, int max)
        {
            return _randomFunction.Next(min, max);
        }
    }
}
