using System;
using System.Security.Cryptography;

namespace Spectrum.Data.Core.Utilities
{
    /// <summary>
    /// Stolen from: http://stackoverflow.com/a/10402129
    /// </summary>
    public static class PasswordUtility
    {
        public static string HashPassword(string password)
        {
            //create the salt value
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //create the Rfc2898DericeBytes and get the hash value
            //NOTE: The iterations are set to 2000 but could go as low as 2000
            //or higher depending on the performance hit.  The example was 10000
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            //combine the salt and password bytes for later use
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //turn the combined salt+hash into a string for storage
            string hashResult = Convert.ToBase64String(hashBytes);

            return hashResult;
        }

        public static bool ComparePasswordToHash(string hashedPassword, string providedPassword)
        {
            //Extract the bytes
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            //Get the salt
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            
            //Compute the hash on the password the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(providedPassword, salt, 2000);
            byte[] hash = pbkdf2.GetBytes(20);
            
            //Compare the results
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
