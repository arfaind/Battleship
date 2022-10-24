using Battleship.Game.Contracts.Models;
using Battleship.Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Game
{
    public class UtilService : IUtilService
    {
        private readonly IRandomFunctionService _randomFunctionService;
        private readonly IConsoleService _consoleService;

        public UtilService(IRandomFunctionService randomFunctionService, IConsoleService consoleService)
        {
            _randomFunctionService = randomFunctionService;
            _consoleService = consoleService;
        }

        public Cell RandomPosition(IList<Cell> cells)
        {
            var randomIndex = _randomFunctionService.Next(0, cells.Count);
            return cells[randomIndex];

        }

        public Cell ReceiveInput(Dimension boardDimensions)
        {
            Cell cell = null;
            while (true)
            {
                _consoleService.WriteLine("Enter Input Row to Attack:");
                var inputRow = _consoleService.ReadLine();
                _consoleService.WriteLine("Enter Input Column to Attack:");
                var inputCol = _consoleService.ReadLine();

                if (int.TryParse(inputRow, out int row) && int.TryParse(inputCol, out int col)
                    && row <= boardDimensions.Height && col <= boardDimensions.Width)
                {
                    cell = new Cell(null, row - 1, col - 1);
                    break;
                }
                else
                {
                    _consoleService.WriteLine("Invalid Input, Please enter again");
                }
            }
            return cell;
        }
    }
}
