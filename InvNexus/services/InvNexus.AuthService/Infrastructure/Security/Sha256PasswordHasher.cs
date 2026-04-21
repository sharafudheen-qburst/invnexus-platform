using System.Security.Cryptography;
using System.Text;
using InvNexus.AuthService.Application.Interfaces;

namespace InvNexus.AuthService.Infrastructure.Security;

public class Sha256PasswordHasher : IPasswordHasher
{
    public string Hash(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }

    public bool Verify(string input, string hash)
    {
        var inputHash = Hash(input);
        return string.Equals(inputHash, hash, StringComparison.OrdinalIgnoreCase);
    }
}
