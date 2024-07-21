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
            if (string.IsNullOrEmpty(input))
                return false;

            // Check if the string matches Base64 encoding pattern
            string base64Pattern = @"^[a-zA-Z0-9+/]*={0,2}$";
            if (!Regex.IsMatch(input, base64Pattern))
                return false;

            try
            {
                // Attempt to decode from Base64
                byte[] decodedBytes = Convert.FromBase64String(input);

                // Further validation can be added here based on encryption characteristics
                // For example, check length or other patterns

                return decodedBytes.Length > 0;  // Return true if decoded bytes are non-empty
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
