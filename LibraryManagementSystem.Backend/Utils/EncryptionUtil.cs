using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace LibraryManagementSystem.Backend.Utils
{
    public static class EncryptionUtil
    {
        private static readonly byte[] _key = Encoding.UTF8.GetBytes("v*JWgusYV6F)5#Wq");

        public static string Encrypt(string input)
        {
            if (input == null)
                return string.Empty;

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] outputBytes = new byte[inputBytes.Length];

            for (int i = 0; i < inputBytes.Length; i++)
            {
                outputBytes[i] = (byte)(inputBytes[i] ^ _key[i % _key.Length]);
            }

            return Convert.ToBase64String(outputBytes);
        }

        public static string Decrypt(string input)
        {
            if (input == null)
                return string.Empty;

            byte[] inputBytes = Convert.FromBase64String(input);
            byte[] outputBytes = new byte[inputBytes.Length];

            for (int i = 0; i < inputBytes.Length; i++)
            {
                outputBytes[i] = (byte)(inputBytes[i] ^ _key[i % _key.Length]);
            }

            return Encoding.UTF8.GetString(outputBytes);
        }

        public static bool isEncrypted(string input)
        {
            if(string.IsNullOrEmpty(input))
                return false;

            string encryptionPattern = @"^[a-zA-Z0-9+/]*={0,2}$";

            return Regex.IsMatch(input, encryptionPattern);
        }
    }
}
