using System;
using System.Security.Cryptography;
using System.Text;

namespace alice_bot_cs.Tools
{
    public sealed class RNGCryptoRandomService
    {
        private static RNGCryptoServiceProvider _random = new RNGCryptoServiceProvider();

        public static string GetRandomString(int stringlength)
        {
            return GetRandomString(null, stringlength);
        }

        //获得长度为stringLength的随机字符串，以key为字母表
        public static string GetRandomString(string key, int stringLength)
        {
            if (key == null || key.Length < 8)
            {
                key = "abcdefghijklmnopqrstuvwxyz1234567890";
            }

            int length = key.Length;
            StringBuilder randomString = new StringBuilder(length);
            for (int i = 0; i < stringLength; ++i)
            {
                randomString.Append(key[SetRandomSeeds(length)]);
            }

            return randomString.ToString();
        }

        private static int SetRandomSeeds(int length)
        {
            decimal maxValue = (decimal)long.MaxValue;
            byte[] array = new byte[8];
            _random.GetBytes(array);

            return (int)(Math.Abs(BitConverter.ToInt64(array, 0)) / maxValue * length);
        }
    }
}