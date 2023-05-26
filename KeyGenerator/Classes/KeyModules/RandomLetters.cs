using Newtonsoft.Json;

namespace KeyGenerator.Classes.KeyModules;

public class RandomLetters : IKeyModule<KeyPatternType>
{
    public RandomLetters(LetterType letterType)
    {
        LetterType = letterType;
    }

    [JsonProperty] private LetterType LetterType { get; set; }

    public KeyPatternType KeyPatternType => KeyPatternType.RandomLetters;

    public string GenerateKeyPart(Key key)
    {
        return LetterType switch
        {
            LetterType.Lowercase => LetterGenerator(key.PatternLength),
            LetterType.Uppercase => LetterUppercaseGenerator(key.PatternLength),
            LetterType.Mixed => LetterMixedGenerator(key.PatternLength),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static string LetterGenerator(int length)
    {
        // This would generate a string with randomly letters of the length of the given length.
        var random = new Random();
        var result = string.Empty;
        for (var i = 0; i < length; i++) result += (char)random.Next(97, 122);
        return result;
    }

    private static string LetterUppercaseGenerator(int length)
    {
        // This would generate a string with randomly uppercase letters of the length of the given length.
        var random = new Random();
        var result = string.Empty;
        for (var i = 0; i < length; i++) result += (char)random.Next(65, 90);
        return result;
    }

    private static string LetterMixedGenerator(int length)
    {
        // This would generate a string with randomly uppercase and lowercase letters of the length of the given length.
        var random = new Random();
        var result = string.Empty;
        for (var i = 0; i < length; i++)
        {
            var randomInt = random.Next(0, 2);
            if (randomInt == 0)
                result += (char)random.Next(65, 90);
            else
                result += (char)random.Next(97, 122);
        }

        return result;
    }
}

public enum LetterType
{
    Lowercase,
    Uppercase,
    Mixed
}