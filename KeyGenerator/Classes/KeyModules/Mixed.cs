namespace KeyGenerator.Classes.KeyModules;

public class Mixed : IKeyModule<KeyPatternType>
{
    KeyPatternType IKeyModule<KeyPatternType>.KeyPatternType => KeyPatternType.RandomNumbersAndLetters;

    public string GenerateKeyPart(Key key)
    {
        var numberPartLength = Math.Ceiling(key.PatternLength / 2.0);
        var letterPartLength = key.PatternLength - numberPartLength;
        return NumberGenerator((int)numberPartLength) + LetterMixedGenerator((int)letterPartLength);
    }

    private static string NumberGenerator(int length)
    {
        var random = new Random();
        var result = string.Empty;
        for (var i = 0; i < length; i++) result += random.Next(0, 9);

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