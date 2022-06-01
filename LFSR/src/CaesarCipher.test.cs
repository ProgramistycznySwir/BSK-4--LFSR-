using Xunit;


namespace Main;

public class CaesarCipher_Tests
{
    [Theory]
    [InlineData("CRYPTOGRAPHY", "FUBSWRJUDSKB", 3)]
    // [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", "AEIMRWZBDFHJLNPSUVYCGKOTX", new int[] { 3, 1, 4, 2 })]
    public void WordEncryption(string word, string encryptedWord_expected, int key)
    {
        // Arrange:
        CaesarCipher encryptor = new(key: key);
        // Act:
        string encryptedWord = encryptor.Encrypt(word);
        // Assert:
        Assert.Equal(encryptedWord_expected, encryptedWord);
    }

    [Theory]
    [InlineData("FUBSWRJUDSKB", "CRYPTOGRAPHY", 3)]
    public void WordDecryption(string word, string decryptedWord_expected, int key)
    {
        // Arrange:
        CaesarCipher encryptor = new(key: key);
        // Act:
        string decryptedWord = encryptor.Decrypt(word);
        // Assert:
        Assert.Equal(decryptedWord_expected, decryptedWord);
    }

    [Theory]
    [InlineData("CRYPTOGRAPHYOSA", 3)]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", 3)]
    [InlineData("CRYPTOGRAPHYOSA", 5)]
    [InlineData("ABCDEFGHIJKLMNOPRSTUWVXYZ", 5)]
    public void TwoWayEncryption(string word, int key)
    {
        // Arrange:
        CaesarCipher encryptor = new(key: key);
        // Act:
        string encryptorOutput = encryptor.Decrypt(encryptor.Encrypt(word));
        // Assert:
        Assert.Equal(word, encryptorOutput);
    }
}