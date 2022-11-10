// red yellow or black cards, 10 of each (1,2,3...)

using System.Security.Cryptography;

namespace CardGame
{
    public class Program
    {
        static void Main()
        {
            files.setupFileSystem();
            List<Player> players = new List<Player>();
            bool game = true;
            bool running = true;
            for (int i = 1; i <= 2; i++)
            {
                Console.WriteLine($"Login for Player {i}: ");
                try
                {
                    players.Add(new Player());
                }
                catch (Exception e)
                {
                    Console.WriteLine("invalid username or password: relaunch the game and try again!");
                    Console.Write("Press Enter to leave: ");
                    Console.ReadLine();
                    return;
                }
            }
            while (running)
            {
                if (game)
                {
                    logic.gameLoop(players);
                };
                Console.Write("Would you like to play again (Y/N)? ");
                string r = Console.ReadLine().ToLower();
                if (r == "y") game = true;
                else running = false;

            }
            
        }
    }
}
