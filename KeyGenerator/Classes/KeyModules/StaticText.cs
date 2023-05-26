using Newtonsoft.Json;

namespace KeyGenerator.Classes.KeyModules;

public class StaticText : IKeyModule<KeyPatternType>
{
    public StaticText(string staticText)
    {
        StaticTextSave = staticText;
    }

    [JsonProperty] private string StaticTextSave { get; set; }

    public KeyPatternType KeyPatternType => KeyPatternType.StaticText;

    public string GenerateKeyPart(Key key)
    {
        return StaticTextSave;
    }
}