using Battleship.Game.Contracts.Models;
using Battleship.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class BoardService : IBoardService
    {
        public void Display(Board board, bool maskShips)
        {
            board.Display(maskShips);
        }

        public IList<Cell> GetNotAttackedCells(Board board)
        {
            return board.GetNotAttackedCells();
        }

        public bool AttackCell(Board board, Cell attackCell)
        {
            return board.AttackCell(attackCell);
        }
        
        public bool AllShipsSinked(Board board)
        {
            return board.AllShipsSinked();
        }
    }
}
