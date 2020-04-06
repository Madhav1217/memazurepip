using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.IO;

namespace Aliera.Utilities.Helpers
{
    public class EncryptionHelper
    {
        public static string HashCode(string text)
        {
            string hashCode = "";
            byte[] salt = System.Text.Encoding.ASCII.GetBytes("salt");
          
            hashCode = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: text,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashCode;


            
        }
    }
}
