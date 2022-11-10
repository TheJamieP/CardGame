namespace CardGame;

public class Player
{
    // TODO: save to external file
    public string Name;
    private string Password;
    public int Wins, winHistory, cards;
    public (int, string) CurrentCard = (0, "s");
    public Player()
    {
        login(this);
    }
     void login(Player p)
    {
        Dictionary<string, string> usernamesAndPasswords = new Dictionary<string, string>()
        {
            // {key, value}
            {"jamie", "p"}, 
            {"p", "jamie"} 
        };
        for (int loginAttempt = 1; loginAttempt < 3; loginAttempt++)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();
            foreach (KeyValuePair<string, string> account in usernamesAndPasswords)
            {
                if (account.Key == username && account.Value == password)
                {
                    p.Name = username;
                    p.Password = password;
                    Console.WriteLine($"Welcome: {username}");
                    loadData(this);
                    return;
                }
            }
        }
        throw new Exception("Invalid username or Password");
    }

    public static void saveData(Player p)
    {
        string saveString = $"{p.Name},{p.Password},{p.winHistory},{p.cards}";
        files.saveData(p.Name, saveString);
    }

    private void loadData(Player p)
    {
        string[] fileData = files.loadDataToList(p.Name);
        p.winHistory = Convert.ToInt16(fileData[2]);
        p.cards = Convert.ToInt16(fileData[3]);
    }
}