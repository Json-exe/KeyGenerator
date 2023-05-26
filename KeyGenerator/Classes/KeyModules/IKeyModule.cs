namespace KeyGenerator.Classes.KeyModules;

public interface IKeyModule<TKeyPatternType>
{
    public TKeyPatternType KeyPatternType { get; }
    public string GenerateKeyPart(Key key);
}

public enum KeyPatternType
{
    RandomNumbers,
    RandomLetters,
    StaticText,
    RandomNumbersAndLetters
}