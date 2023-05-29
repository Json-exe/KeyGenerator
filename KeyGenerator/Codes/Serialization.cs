using KeyGenerator.Classes;
using KeyGenerator.Classes.Database;
using Newtonsoft.Json;

namespace KeyGenerator.Codes;

public static class Serialization
{
    #region Keys

    public static void SaveKeyToFile(List<Key> keys, string filePath)
    {
        keys.ForEach(x => x.ConvertKeyModulesToData());

        var json = JsonConvert.SerializeObject(keys, Formatting.Indented);

        File.WriteAllText(filePath, json);
    }


    public static List<Key> LoadKeyFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);

        var key = JsonConvert.DeserializeObject<List<Key>>(json);

        key?.ForEach(x => x.ConfigureKeyModulesFromData());

        return key ?? new List<Key>();
    }

    #endregion

    #region Database

    public static void SaveDatabasesToFile(DatabaseManager dbManager, string filePath)
    {
        dbManager.ConvertKeyModulesToData();

        var json = JsonConvert.SerializeObject(dbManager, Formatting.Indented);

        File.WriteAllText(filePath, json);
    }


    public static DatabaseManager LoadDatabasesFromFile(string filePath)
    {
        var json = File.ReadAllText(filePath);

        var dbManager = JsonConvert.DeserializeObject<DatabaseManager>(json);

        dbManager?.ConfigureKeyModulesFromData();

        return dbManager ?? new DatabaseManager();
    }

    #endregion
    
}