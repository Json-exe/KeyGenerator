using Newtonsoft.Json;

namespace KeyGenerator.Classes.KeyModules;

public class StaticText : IKeyModule<KeyPatternType>
{
    public KeyPatternType KeyPatternType => KeyPatternType.StaticText;

    [JsonProperty]
    private string StaticTextSave { get; set; }
    
    public string GenerateKeyPart(Key key)
    {
        return StaticTextSave;
    }
    
    public StaticText(string staticText)
    {
        StaticTextSave = staticText;
    }
}