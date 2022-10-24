using Battleship.Game.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Interfaces
{
    public interface IBoardService
    {
        void Display(Board board, bool maskShips);
        IList<Cell> GetNotAttackedCells(Board board);
        bool AttackCell(Board board, Cell attackCell);
        bool AllShipsSinked(Board board);
    }
}
