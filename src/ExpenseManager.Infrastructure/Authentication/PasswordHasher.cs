using System.Security.Cryptography;
using ExpenseManager.Application.Common.Interfaces.Authentication;

namespace ExpenseManager.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const char Delimiter = ':';
    private static readonly HashAlgorithmName HashAlgorithmName = HashAlgorithmName.SHA256;

    public bool Verify(string password, string passwordHash)
    {
        var parts = passwordHash.Split(Delimiter);
        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);
        var verifyHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName, KeySize);

        // doesn't leak timing information
        return CryptographicOperations.FixedTimeEquals(hash, verifyHash);
    }

    public string Hash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 10000, HashAlgorithmName, KeySize);

        return $"{Convert.ToBase64String(salt)}{Delimiter}{Convert.ToBase64String(hash)}";
    }
}