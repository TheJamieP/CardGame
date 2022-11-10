namespace CardGame;

public class files
{
    public static void setupFileSystem()
    {
        if (!Directory.Exists("./players/"))
        {
            Directory.CreateDirectory("./players/");
        }
        
    }
    public static void saveData(string name, string saveString)
    {
        string path = $"./players/{name}.txt";
        if(!File.Exists(path)) File.Create(path).Dispose();
        File.WriteAllText(path, saveString);
    }
    public static string[] loadDataToList(string name)
    {
        string path = $"./players/{name}.txt";
        string[] fileData = File.ReadAllText(path).Split(",");
        return fileData;
    }
}