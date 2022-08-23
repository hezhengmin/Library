using System.Security.Cryptography;
using System.Text;

namespace Zheng.Utilities.Cryptography
{
    public class SHAExtensions
    {
        /// <summary>
        /// 密碼加密(SHA512)
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static byte[] PasswordSHA512Hash(string password)
        {
            byte[] hashBytes = new byte[64];
            using (SHA512 sha512Hash = SHA512.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(password);
                hashBytes = sha512Hash.ComputeHash(sourceBytes);
            }

            return hashBytes;
        }
    }
}

