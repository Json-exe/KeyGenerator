using KeyGenerator.Classes;
using KeyGenerator.Classes.Database;

namespace KeyGenerator.Codes;

public static class SystemHandler
{
    public static string DataPath { get; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"JDS\KeyGenerator\Data\");

    public static List<Key> Keys { get; } = new();
    public static DatabaseManager DatabaseManager { get; set; } = new();

    public static void LoadData()
    {
        Keys.Clear();
        if (File.Exists(DataPath + "Keys.json"))
            Keys.AddRange(Serialization.LoadKeyFromFile(DataPath + "Keys.json"));
        if (File.Exists(DataPath + "DatabaseManager.json"))
            DatabaseManager = Serialization.LoadDatabasesFromFile(DataPath + "DatabaseManager.json");
    }

    public static void SaveKeys()
    {
        if (!Keys.Any()) return;
        Serialization.SaveKeyToFile(Keys, DataPath + "Keys.json");
    }

    public static void SaveDatabaseManager()
    {
        if (!DatabaseManager.Databases.Any()) return;
        Serialization.SaveDatabasesToFile(DatabaseManager, DataPath + "DatabaseManager.json");
    }
}