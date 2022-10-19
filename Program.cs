// red yellow or black cards, 10 of each (1,2,3...)

namespace CardGame
{
    public class Player
    {
        //TODO: player class:
        // TODO: sign-in
        //TODO: authenticate
        // TODO: wins, cards, name
        // TODO: save to external file
        public string Name;
        public int Wins;
        public List<(int, string)> Cards = new List<(int, string)>();
        public (int, string) CurrentCard = (0, "s");
        public Player(string name)
        {
            this.Name = name;
        }
    }
    public class Program
    {
        static void gameLoop(List<Player> players, List<(int,string)> deck)
        {
            bool game = true;
            while (deck.Count > 2)
            {
                //TODO: to every ending possible.
                //TODO: implement win based off card colour
                foreach (Player player in players)
                {
                    player.CurrentCard = deck[0];
                    deck.RemoveAt(0);
                }
                Console.WriteLine($"Player1:  {players[0].CurrentCard}  Player2:  {players[1].CurrentCard}");
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
                    //currentcard = (int item1, string item2) where item1 = colour and item2 = colour
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
            if (players[0].Cards.Count > players[1].Cards.Count) Console.WriteLine("Player One Wins!");
            else Console.WriteLine("Player Two Wins!");
        }
        //TODO: refactor Auth for 2 users
        static bool Auth(string? username, string? password)
        {
            username = username.ToLower();
            password = password.ToLower();
            Dictionary<string, string> usernamesAndPasswords = new Dictionary<string, string>()
            {
                // {key, value}
                {"username1", "password1"},
                {"username2", "password2"}
            };
            foreach (KeyValuePair<string, string> account in usernamesAndPasswords)
            {
                if (account.Key == username && account.Value == password) return true;
            }
            

            return false;
        }
        static List<(int, string)> ShuffleCards(List<(int, string)> deck)
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
        static List<(int, string)> GenerateDeck()
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
            return deck;
        }
        
        static void handleWin(List<Player> playerList, Player Victor)
        {
            List<(int, string)> cards = new List<(int, string)>();
            foreach (var p in playerList)
            {
                if (playerList.IndexOf(p) - 1 > 0 && Victor == p)
                {
                    p.Cards.Append(p.CurrentCard);
                    p.CurrentCard = (0, "");
                    p.Cards.Append(playerList[0].CurrentCard);
                    playerList[0].CurrentCard = (0, "");
                }
                else
                {
                    p.Cards.Append(p.CurrentCard);
                    p.CurrentCard = (0, "");
                    p.Cards.Append(playerList[1].CurrentCard);
                    playerList[1].CurrentCard = (0, "");
                    
                }
            }
        }    
        
        static void Main()
        {
            /*
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();
             if (!Auth(username, password)) Console.WriteLine("Invalid Username and or Password"); return;
            */
            bool game = true;
            var deck = ShuffleCards(GenerateDeck());
            string username1 = "username1";
            string username2 = "username2";
            List<Player> players = new List<Player>()
            {
                new Player(username1),
                new Player(username2)
            };
            bool running = true;
            while (running)
            {
                if (game) gameLoop(players, deck);
                Console.Write("Would you like to play again? ");
                
            }
            
        }
    }
}
