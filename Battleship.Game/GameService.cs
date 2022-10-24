using Battleship.Game.Contracts.Models;
using Battleship.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class GameService : IGameService
    {
        private readonly IPlayerService _playerService;
        private readonly IUtilService _utilService;

        public IList<Player> Players { get; set; }

        public GameService(IPlayerService playerService, IUtilService utilService)
        {
            _playerService = playerService;
            _utilService = utilService;
        }


        public void SetupGame(int boardWidth, int boardHeight, int noOfComputerPlayers)
        {
            Players = _playerService.GeneratePlayers(boardWidth, boardHeight, noOfComputerPlayers);
        }

        public void ArrangeShips(int shipWidth, int shipLength, int noOfShips)
        {
            for (int i = 0; i < noOfShips; i++)
            {
                foreach (var player in Players)
                {
                    var isValid = player.Board.ValidateBoard(shipWidth, shipLength, noOfShips);
                    if (isValid)
                    {
                        var freeCells = player.Board.GetFreeCells();
                        var cell = _utilService.RandomPosition(freeCells);
                        while (!player.Board.ArrangeShip(cell, shipWidth, shipLength))
                        {
                            freeCells.Remove(cell);
                            if (freeCells.Count == 0)
                            {
                                throw new Exception("Can not place ship on board, required free space not available. Reduce the ship count and try again.");
                            }
                            cell = _utilService.RandomPosition(freeCells);
                        }
                    }
                    else
                    {
                        throw new Exception("Can not place ship on board, required free space not available. Reduce the ship count and try again.");
                    }
                }
            }
        }

        public void DisplayBoard()
        {
            foreach (var player in Players)
            {
                player.Board.Display(false);
            }
        }

        public void StartPlay()
        {
            var playerTurn = 0;
            while (true)
            {
                var currentPlayer = Players[playerTurn];

                if (currentPlayer == null)
                {
                    throw new InvalidOperationException("Player null, players setup not proper");
                }

                Console.WriteLine($"Player {playerTurn + 1} turn");
                bool gameOver = _playerService.Play(Players, currentPlayer);
                if (gameOver)
                {
                    Console.WriteLine($"Player {playerTurn + 1} {currentPlayer.IsComputer} wins!!! ");
                    break;
                }

                playerTurn++;
                // Reset the Player turn to first player once the loop is completed.
                if (playerTurn == Players.Count)
                {
                    playerTurn = 0;
                }
            }
        }
    }
}
