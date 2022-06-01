namespace Main;


/// <summary>
/// Left it here if i would need it.
/// </summary>
public class CaesarCipher
{
    public const int Default_Key = 3;
    public const string Default_Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    
    public int Key { get; init; }

    public string Alphabet { get; init; }
    public Dictionary<char, int> Alphabet_Dict { get; init; }

    public CaesarCipher(int key = Default_Key, string alphabet = Default_Alphabet)
    {
        (Key, Alphabet) = (key, alphabet);
        Alphabet_Dict = Alphabet
                .Zip(Enumerable.Range(0, Alphabet.Length))
                .ToDictionary(e => e.First, e => e.Second);
    }
    
    public string Encrypt(string word)
        => Shift(word, Key);
    public string Decrypt(string word)
        => Shift(word, -Key);

    public string Shift(string word, int shift)
    {
        if(shift is 0)
            return word;

        if(word.ToHashSet().Any(letter => Alphabet_Dict.ContainsKey(letter) is false))
            throw new ArgumentException($"First argument [{nameof(word)}] contains letters which are not in this encryptor alphabet!");

        return word.Select(letter => ShiftLetter(letter, shift)).CollectString();
    }

    public char ShiftLetter(char letter, int shift)
        => Alphabet[MyMath.ClampMod(Alphabet_Dict[letter] + shift, mod: Alphabet.Length)];
}