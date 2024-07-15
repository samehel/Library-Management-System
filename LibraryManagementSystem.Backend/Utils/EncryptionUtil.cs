﻿using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementSystem.Backend.Utils
{
    public static class EncryptionUtil
    {
        private static readonly byte[] _key = Encoding.UTF8.GetBytes("v*JWgusYV6F)5#Wq");
        private static readonly byte[] _iv = Encoding.UTF8.GetBytes("Ty!4@Gv(Y9@FY9@");

        public static string Encrypt(string password)
        {
            if (password == null)
                return string.Empty;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream()) 
                {
                    using (CryptoStream csStream = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csStream))
                        {
                            swEncrypt.Write(password);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string password)
        {
            byte[] passwordBytes = Convert.FromBase64String(password);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _key;
                aesAlg.IV = _iv;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(passwordBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
