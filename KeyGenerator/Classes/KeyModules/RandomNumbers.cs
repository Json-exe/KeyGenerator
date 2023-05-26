namespace KeyGenerator.Classes.KeyModules;

public class RandomNumbers : IKeyModule<KeyPatternType>
{
    public KeyPatternType KeyPatternType => KeyPatternType.RandomNumbers;
    
    public string GenerateKeyPart(Key key)
    {
        return NumberGenerator(key.PatternLength);
    }

    private static string NumberGenerator(int length)
    {
        var random = new Random();
        var result = string.Empty;
        for (var i = 0; i < length; i++)
        {
            result += random.Next(0, 9);
        }

        return result;
    }
}