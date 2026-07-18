using System.Security.Cryptography;
using System.Text;

namespace QMS.Core.Utilities;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }

    public static bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput.Equals(hash);
    }
}
