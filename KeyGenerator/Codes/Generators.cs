namespace KeyGenerator.Codes;

public static class Generators
{
    public static string LetterMixedGenerator(int length)
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

    public static string MixedGenerator(int length)
    {
        // This would generate a string with randomly numbers and letters of the length of the given length.
        var random = new Random();
        var result = string.Empty;
        for (var i = 0; i < length; i++)
        {
            var randomInt = random.Next(0, 2);
            if (randomInt == 0)
                result += (char)random.Next(65, 90);
            else
                result += random.Next(0, 9);
        }

        return result;
    }

    public static string GuidGenerator(int length)
    {
        // This would generate a GUID, removes the dashes and returns the string with the length of the given length.
        var guid = Guid.NewGuid().ToString().Replace("-", string.Empty);
        return guid[..length];
    }
}