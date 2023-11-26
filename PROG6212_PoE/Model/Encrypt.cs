using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    internal class Encrypt
    {
        /// <summary>
        /// Compute a SHA-256 hash of the given input string.
        /// </summary>
        /// <param name="pass">The input string to hash.</param>
        /// <returns>The SHA-256 hash of the input string.</returns>
        public static string Hash(string pass)
        {
            using var sha256 = SHA256.Create();
            if (sha256 == null)
            {
                throw new InvalidOperationException("SHA-256 is not supported on this system.");
            }

            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));

            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
