using Battleship.Game.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game.Interfaces
{
    public interface IPlayerService
    {
        IList<Player> GeneratePlayers(int boardWidth, int boardHeight, int noOfComputerPlayers);
        bool Play(IList<Player> players, Player currentPlayer);
    }
}
