using System;
using System.Text;
using System.Security.Cryptography;

namespace LTWinforms_CuoiKy_Nhom8.BUS
{
    public static class SecurityHelper
    {
        private const int SaltSize = 32;

        public static string GenerateSalt()
        {
            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string ComputeHash(string password, string saltBase64)
        {
            if (password == null)
            {
                password = "";
            }
            var saltBytes = Convert.FromBase64String(saltBase64);
            var pwdBytes = Encoding.UTF8.GetBytes(password);

            var buffer = new byte[saltBytes.Length + pwdBytes.Length];
            Buffer.BlockCopy(saltBytes, 0, buffer, 0, saltBytes.Length);
            Buffer.BlockCopy(pwdBytes, 0, buffer, saltBytes.Length, pwdBytes.Length);

            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(buffer);
                return Convert.ToBase64String(hash);
            }
        }

        public static string HashPassword(string password)
        {
            var salt = GenerateSalt();
            var hash = ComputeHash(password, salt);
            return salt + ":" + hash;
        }

        public static bool VerifyPassword(string storedValue, string password)
        {
            if (string.IsNullOrEmpty(storedValue))
            {
                return false;
            }
            var parts = storedValue.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }
            var salt = parts[0];
            var expectedHash = parts[1];
            var computedHash = ComputeHash(password, salt);
            return SlowEqualsBase64(expectedHash, computedHash);
        }

        private static bool SlowEqualsBase64(string aBase64, string bBase64)
        {
            try
            {
                var a = Convert.FromBase64String(aBase64);
                var b = Convert.FromBase64String(bBase64);
                if (a.Length != b.Length)
                {
                    return false;
                }
                int diff = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    diff |= a[i] ^ b[i];
                }
                return diff == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}