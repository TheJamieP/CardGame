namespace CardGame;

public class logic
{
    public static void gameLoop(List<Player> players)
    {
        List<(int, string)>deck = GenerateDeck();
        Console.WriteLine("Dealing cards");
        for (var i = 0; i < 3; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
        while (deck.Count > 2)
        {
            foreach (Player player in players)
            {
                player.CurrentCard = deck[0];
                Console.WriteLine("Press enter to pick your card: ");
                Console.Read();
                Console.WriteLine($"{player.Name} picked a {player.CurrentCard.Item2} {player.CurrentCard.Item1} card");
                deck.RemoveAt(0);
                Thread.Sleep(500);
            }
            // Handle win based of card colour being the same
            if (players[0].CurrentCard.Item2 == players[1].CurrentCard.Item2)
            {
                if (players[0].CurrentCard.Item1 > players[1].CurrentCard.Item1)
                {
                    handleWin(players, players[0]);
                }
                handleWin(players, players[1]);
            }
            else
            {
                switch (players[0].CurrentCard.Item2)
                {
                    case "black":
                        if (players[1].CurrentCard.Item2 == "red") handleWin(players, players[1]);
                        else handleWin(players, players[0]);
                        break;
                    
                    case "red":
                        if (players[1].CurrentCard.Item2 == "yellow") handleWin(players, players[1]);
                        else handleWin(players, players[0]);
                        break;
                    
                    case "yellow":
                        if (players[1].CurrentCard.Item2 == "black") handleWin(players, players[1]);
                        else handleWin(players, players[0]);
                        break;
                }
                
            }
            
        }
        if (players[0].Wins > players[1].Wins)
        {
            Console.WriteLine("Player One Wins!");
            players[0].winHistory++;
        }
        else if (players[0].Wins == players[1].Wins)
        {
            Console.WriteLine("Draw!");
        }
        else
        {
            Console.WriteLine("Player Two Wins!");
            players[1].winHistory++;
        }
        players[0].Wins = 0;
        players[1].Wins = 0;
        Player.saveData(players[0]);
        Player.saveData(players[1]);
    }
    private static List<(int, string)> ShuffleCards(List<(int, string)> deck)
        {
            List<(int, string)> newDeck = new List<(int, string)>();
            int i = deck.Count;
            while (i != 1)
            {
                int randomInt = new Random().Next(1, deck.Count -1 );
                newDeck.Add(deck[randomInt]);
                deck.RemoveAt(randomInt);
                i--;
            }
            newDeck.Add(deck[0]);
            return newDeck;
        }
        public static List<(int, string)> GenerateDeck()
        {
            List<(int,string)> deck = new List<(int,string)>();

            string[] colours = { "red", "yellow", "black" };
            foreach (string colour in colours)
            {
                for (var i = 1; i <= 10; i++)
                {
                    deck.Add((i, colour));
                }
            }
            return ShuffleCards(deck);
        }
        
        private static void handleWin(List<Player> playerList, Player Victor)
        {
            List<(int, string)> cards = new List<(int, string)>();
            foreach (var p in playerList)
            {
                if (Victor.Name == p.Name)
                {
                    Victor.cards++;
                    Victor.CurrentCard = (0, "");
                    p.CurrentCard = (0, "");
                    Victor.Wins++;
                    
                    Console.WriteLine($"\n{Victor.Name} has won this round, they have won {Victor.Wins} total this game!");
                }
            }
        }
}