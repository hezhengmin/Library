using System;

namespace Zheng.Utilities.Randomized
{
    public class RandomExtensions
    {
        /// <summary>
        /// 重設密碼亂數
        /// </summary>
        /// <returns></returns>
        public static string GetRandom()
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            int passwordLength = 10;//密碼長度
            char[] chars = new char[passwordLength];
            Random rnd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rnd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
