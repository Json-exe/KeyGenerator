using KeyGenerator.Classes;

namespace KeyGenerator.Codes;

public static class SystemHandler
{
    public static string DataPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"JDS\KeyGenerator\Data\");
    public static List<Key> Keys { get; } = new();

    public static void LoadAllKeys()
    {
        Keys.Clear();
        if (!File.Exists(DataPath + "Keys.json")) return;
        Keys.AddRange(Serialization.LoadKeyFromFile(DataPath + "Keys.json"));
    }

    public static void SaveKeys()
    {
        if (!Keys.Any()) return;
        Serialization.SaveKeyToFile(Keys, DataPath + "Keys.json");
    }
}