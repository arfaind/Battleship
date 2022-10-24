using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Interfaces
{
    public interface IConsoleService
    {
        string ReadLine();
        void WriteLine(string message);
    }
}
