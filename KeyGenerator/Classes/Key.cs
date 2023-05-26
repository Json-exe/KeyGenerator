using KeyGenerator.Classes.KeyModules;
using Newtonsoft.Json;

namespace KeyGenerator.Classes;

public class Key
{
    public string Name { get; set; } = string.Empty;
    public int PatternLength { get; set; }
    public int PatternSize { get; set; }
    public int Keys { get; set; }
    public char Separator { get; set; }

    [JsonIgnore] public List<IKeyModule<KeyPatternType>> KeyModules { get; set; } = new();

    [JsonProperty] private List<Dictionary<KeyPatternType, object>> KeyModuleData { get; set; } = new();

    public void ConvertKeyModulesToData()
    {
        KeyModuleData.Clear();
        foreach (var keyModule in KeyModules)
            KeyModuleData.Add(new Dictionary<KeyPatternType, object>
            {
                { keyModule.KeyPatternType, keyModule }
            });
    }

    public void ConfigureKeyModulesFromData()
    {
        foreach (var keyModuleData in KeyModuleData)
        foreach (var (key, value) in keyModuleData)
            try
            {
                // The value is still a JObject, so we need to convert it to the correct type.
                var valueString = value.ToString();
                if (valueString != null)
                {
                    var valueObject = JsonConvert.DeserializeObject(valueString, key switch
                    {
                        KeyPatternType.RandomLetters => typeof(RandomLetters),
                        KeyPatternType.StaticText => typeof(StaticText),
                        KeyPatternType.RandomNumbers => typeof(RandomNumbers),
                        KeyPatternType.RandomNumbersAndLetters => typeof(Mixed),
                        _ => throw new ArgumentOutOfRangeException()
                    });
                    if (valueObject != null)
                        KeyModules.Add((IKeyModule<KeyPatternType>)valueObject);
                    else
                        Console.WriteLine("A module of your key could not be loaded! Module-Type: " + key);
                }
                else
                {
                    Console.WriteLine("A module of your key could not be loaded! Module-Type: " + key);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("There was an error loading your key! Error: " + e);
            }
    }
}