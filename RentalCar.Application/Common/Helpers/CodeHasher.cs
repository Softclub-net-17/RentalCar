using System.Security.Cryptography;

namespace RentalCar.Application.Common.Helpers
{
    public static class CodeHasher
    {
        public static string HashCode(string code)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var derived = new Rfc2898DeriveBytes(code, salt, 50_000, HashAlgorithmName.SHA256).GetBytes(32);
            return Convert.ToBase64String(salt.Concat(derived).ToArray());
        }

        public static bool VerifyCode(string stored, string provided)
        {
            var bytes = Convert.FromBase64String(stored);
            var salt = bytes[..16];
            var storedHash = bytes[16..];
            var derived = new Rfc2898DeriveBytes(provided, salt, 50_000, HashAlgorithmName.SHA256).GetBytes(32);
            return derived.SequenceEqual(storedHash);
        }
    }
}