using KeyGenerator.Classes;
using Newtonsoft.Json;

namespace KeyGenerator.Codes;

public static class Serialization
{
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
}