using Battleship.Game.Contracts.Models;
using Battleship.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class PlayerService : IPlayerService
    {
        private readonly IUtilService _utilService;
        private readonly IBoardService _boardService;

        public PlayerService(IUtilService utilService)
        {
            _utilService = utilService;
        }

        public IList<Player> GeneratePlayers(int boardWidth, int boardHeight, int noOfComputerPlayers)
        {
            var players = new List<Player>();
            for (int i = 0; i < 2; i++)
            {
                Player player = new Player
                {
                    Id = Guid.NewGuid().ToString(),
                    Board = new Board(boardWidth, boardHeight),
                    IsComputer = noOfComputerPlayers > 0
                };
                players.Add(player);
                noOfComputerPlayers--;
            }
            return players;
        }

        public bool Play(IList<Player> players, Player currentPlayer)
        {
            bool gameOver = false;
            foreach (var player in players)
            {
                if(player.Id != currentPlayer.Id)
                {
                    Cell attackCell = null;
                    player.Board.Display(true);
                    if (currentPlayer.IsComputer)
                    {
                        var notAttackedCells = player.Board.GetNotAttackedCells();
                        attackCell = _utilService.RandomPosition(notAttackedCells);
                    }
                    else
                    {
                        attackCell = _utilService.ReceiveInput(player.Board.Dimension);
                    }
                    var hit = player.Board.AttackCell(attackCell);
                    if (hit)
                    {
                        gameOver = player.Board.AllShipsSinked();
                    }
                }
            }
            return gameOver;
        }
    }
}
