using Battleship.Game.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Interfaces
{
    public interface IGameService
    {
        IList<Player> Players { get; set; }
        void SetupGame(int boardWidth, int boardHeight, int noOfComputerPlayers);
        void ArrangeShips(int shipWidth, int shipLength, int noOfShips);
        void DisplayBoard();
        void StartPlay();
    }
}
