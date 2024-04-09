using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Aplication.Helpers
{
   public class PasswordEncrypted
    {
        public static string ComputeSah256Hash(string password)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
