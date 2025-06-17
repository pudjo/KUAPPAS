using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KUAPPAS.SP2DOnline
{
    public static class HIMACExtension
    {
        public static string HMACSHA256(this string message, string key)
        {
            var encoding = new System.Text.UTF8Encoding();

            byte[] keyByte = encoding.GetBytes(key);

            byte[] messageBytes = encoding.GetBytes(message);

            using (var hmacSHA256 = new HMACSHA256(keyByte))
            {
                byte[] hashMessage = hmacSHA256.ComputeHash(messageBytes);
                return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
            }
        }
    }
}
