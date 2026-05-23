using System;
using System.Security.Cryptography;
using System.Text;

namespace GMS_Kabbo.Data
{
    internal static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;

        public static bool IsHashed(string stored) =>
            !string.IsNullOrEmpty(stored) &&
            stored.Split('.').Length == 3 &&
            int.TryParse(stored.Split('.')[0], out _);

        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty.", nameof(password));

            var salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            var hash = DeriveKey(password, salt, Iterations);
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string stored)
        {
            if (string.IsNullOrEmpty(stored))
                return false;

            if (!IsHashed(stored))
                return TimingSafeEquals(password, stored);

            var parts = stored.Split('.');
            if (!int.TryParse(parts[0], out int iterations))
                return false;

            byte[] salt;
            byte[] expected;
            try
            {
                salt = Convert.FromBase64String(parts[1]);
                expected = Convert.FromBase64String(parts[2]);
            }
            catch (FormatException)
            {
                return false;
            }

            var actual = DeriveKey(password, salt, iterations);
            return TimingSafeEquals(expected, actual);
        }

        private static byte[] DeriveKey(string password, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                return pbkdf2.GetBytes(HashSize);
        }

        private static bool TimingSafeEquals(string a, string b)
        {
            if (a == null || b == null)
                return false;

            var ba = Encoding.UTF8.GetBytes(a);
            var bb = Encoding.UTF8.GetBytes(b);
            if (ba.Length != bb.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < ba.Length; i++)
                diff |= ba[i] ^ bb[i];
            return diff == 0;
        }

        private static bool TimingSafeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
