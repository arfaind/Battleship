using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Game.Contracts.Models
{
    public class Board
    {
        private readonly Cell[,] _boardCells = null;
        private readonly IList<Ship> _ships = new List<Ship>();

        public Dimension Dimension { get; set; }
         
        public Board(int width, int height)
        {
            Dimension = new Dimension(width, height);
            _boardCells = new Cell[Dimension.Width, Dimension.Height];
            SetupBoard();
        }

        public bool AllShipsSinked()
        {
            return _ships.All(x => x.IsSunk());
        }

        public int NumberOfShipsSinked()
        {
            return _ships.Count(x => x.IsSunk());
        }

        public void SetupBoard()
        {
            for (int i = 0; i < _boardCells.GetLength(0); i++)
            {
                for (int j = 0; j < _boardCells.GetLength(1); j++)
                {
                    var cell = new Cell(null, i, j);
                    _boardCells[i, j] = cell;
                }
            }
        }

        public IList<Cell> GetFreeCells()
        {
            var freeCells = new List<Cell>();
            for (int i = 0; i < _boardCells.GetLength(0); i++)
            {
                for (int j = 0; j < _boardCells.GetLength(1); j++)
                {
                    if (_boardCells[i,j].State == Enums.CellStateType.Empty)
                    {
                        var cell = new Cell(null, i, j);
                        freeCells.Add(cell);
                    }
                }
            }
            return freeCells;
        }

        public IList<Cell> GetNotAttackedCells()
        {
            var freeCells = new List<Cell>();
            for (int i = 0; i < _boardCells.GetLength(0); i++)
            {
                for (int j = 0; j < _boardCells.GetLength(1); j++)
                {
                    if (_boardCells[i, j].State != Enums.CellStateType.Tried && _boardCells[i, j].State != Enums.CellStateType.Hit)
                    {
                        var cell = new Cell(null, i, j);
                        freeCells.Add(cell);
                    }
                }
            }
            return freeCells;
        }

        public bool ArrangeShip(Cell freeCell, int shipWidth, int shipLength)
        {
            bool arranged = true;
            var ship = new Ship(shipLength, shipWidth);
            var row = freeCell.Row;
            var col = freeCell.Col;
            List<Tuple<int, int>> positions = new List<Tuple<int, int>>();
            // Try Vertical Orientation
            if (shipWidth <= (Dimension.Width - col) && shipLength <= (Dimension.Height - row))
            {
                for (int i = 1; i <= shipWidth; i++)
                {
                    row = freeCell.Row;
                    if (arranged == false) break;
                    for (int j = 1; j <= shipLength; j++)
                    {
                        if (_boardCells[row, col].State == Enums.CellStateType.Empty)
                        {
                            positions.Add(new Tuple<int, int>(row, col));
                        }
                        else
                        {
                            arranged = false;
                            break;
                        }
                        row++;
                    }
                    col++;
                }
            }
            // Try Horizontal Orientation
            else if (shipWidth <= (Dimension.Height - row) && shipLength <= (Dimension.Width - col))
            {
                for (int i = 1; i <= shipWidth; i++)
                {
                    col = freeCell.Col;
                    if (arranged == false) break;
                    for (int j = 1; j <= shipLength; j++)
                    {
                        if (_boardCells[row, col].State == Enums.CellStateType.Empty)
                        {
                            positions.Add(new Tuple<int, int>(row, col));
                        }
                        else
                        {
                            arranged = false;
                            break;
                        }
                        col++;
                    }
                    row++;
                }
            }
            else
            {
                arranged = false;
            }

            if (arranged)
            {
                foreach (var position in positions)
                {
                    var cell = _boardCells[position.Item1, position.Item2];
                    cell.State = Enums.CellStateType.Ship;
                    cell.Ship = ship;
                }
                _ships.Add(ship);
            }
            return arranged;
        }

        public bool ValidateBoard(int shipWidth, int shipLength, int noOfShips)
        {
            bool isValid = true;

            // If ship width or length exceed the dimension width or height, that means that ship will not fit either horizontally or vertically.
            if(shipWidth > Dimension.Width || shipWidth > Dimension.Height || shipLength > Dimension.Width || shipLength > Dimension.Height)
            {
                isValid = false;
            }
            var noOfCellsNeededByShips = shipLength * shipWidth * noOfShips;
            var totalBoardCells = Dimension.Width * Dimension.Height;
            if (noOfCellsNeededByShips > totalBoardCells)
            {
                isValid = false;
            }

            return isValid;
        }

        public void Display(bool maskShips)
        {
            Console.WriteLine(maskShips ? $"Player Board (X:Hit, -:Attack Pending, M:Miss) ({NumberOfShipsSinked()} Sinked)" : "Player Board (X:Hit, O:Ship, -:Empty)");

            for (int i = 0; i < _boardCells.GetLength(0); i++)
            {
                for (int j = 0; j < _boardCells.GetLength(1); j++)
                {
                    if (_boardCells[i, j].State == Enums.CellStateType.Hit)
                    {
                        Console.Write($"| X ");
                    }
                    else if (_boardCells[i, j].State == Enums.CellStateType.Ship)
                    {
                        Console.Write(maskShips ? $"| - ": $"| O ");
                    }
                    else if(_boardCells[i, j].State == Enums.CellStateType.Tried)
                    {
                        Console.Write($"| M ");
                    }
                    else
                    {
                        Console.Write($"| - ");
                    }
                }
                Console.Write("\n");
            }
        }

        public bool AttackCell(Cell cell)
        {
            bool hit = false;
            var boardCell = _boardCells[cell.Row, cell.Col];
            if (boardCell.State == Enums.CellStateType.Empty)
            {
                boardCell.State = Enums.CellStateType.Tried;
            }
            else if(boardCell.State == Enums.CellStateType.Ship)
            {
                hit = true;
                boardCell.State = Enums.CellStateType.Hit;
                boardCell.Ship.HitsConsumed++;
            }
            return hit;
        }
    }
}
