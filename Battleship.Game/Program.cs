using System;

namespace Battleship.Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");
                var utilService = new UtilService(new RandomFunctionService(), new ConsoleService());
                var gameService = new GameService(new PlayerService(utilService), utilService);
                gameService.SetupGame(10, 10, 2);
                gameService.ArrangeShips(1, 9, 5);
                gameService.DisplayBoard();
                gameService.StartPlay();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
