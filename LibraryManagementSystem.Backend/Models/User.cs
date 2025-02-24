﻿using LibraryManagementSystem.Backend.Utils;

namespace LibraryManagementSystem.Backend.Models
{
    public class User
    {
        public int ID { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public string? Username { get; set; }

        private string? _password;
        public string Password 
        {
            get 
            {
                if (this._password == null)
                    return string.Empty;

                return EncryptionUtil.Decrypt(this._password); 
            }
            set { this._password = EncryptionUtil.Encrypt(value); }
        }
       
    }
}
