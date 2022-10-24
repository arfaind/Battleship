using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Interfaces
{
    public interface IRandomFunctionService
    {
        int Next(int min, int max);
    }
}
