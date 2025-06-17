using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
//using System.Text.Encodings;

using System.IO;//.Enumeration;
using System.Security.Cryptography.X509Certificates;

namespace KUAPPAS
{
    public static class DESEncrypt
    {


        public static string Encrypt(string PlainText, string Key)
        {
            byte[] EncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objOfMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] SecurityKeyArray = objOfMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            objOfMD5CryptoService.Clear();

            var objOfTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            objOfTripleDESCryptoService.Key = SecurityKeyArray;
            objOfTripleDESCryptoService.Mode = CipherMode.ECB;
            objOfTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objOfCryptoTransform = objOfTripleDESCryptoService.CreateEncryptor();

            byte[] ResultArray = objOfCryptoTransform.TransformFinalBlock(EncryptedArray, 0, EncryptedArray.Length);

            objOfTripleDESCryptoService.Clear();

            return Convert.ToBase64String(ResultArray, 0, ResultArray.Length);

        }
        public static string Decrypt(string CipherText, string Key)
        {
            byte[] EncryptArray = Convert.FromBase64String(CipherText);

            MD5CryptoServiceProvider objOfMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] SecurityKeyArray = objOfMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(Key));
            objOfMD5CryptoService.Clear();

            var objOfTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            objOfTripleDESCryptoService.Key = SecurityKeyArray;
            objOfTripleDESCryptoService.Mode = CipherMode.ECB;
            objOfTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objOfCryptoTransform = objOfTripleDESCryptoService.CreateDecryptor();

            byte[] ResultArray = objOfCryptoTransform.TransformFinalBlock(EncryptArray, 0, EncryptArray.Length);

            objOfTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(ResultArray);
        }

        static public string GetMD5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                // Create a new Stringbuilder to collect the bytes 
                // and create a string. 
                StringBuilder sBuilder = new StringBuilder();
                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string. 
                for (int i = 0; i < (data.Length); i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();

            }
        }
        public static  string CalculateSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] hashValue;
            UTF8Encoding objUtf8 = new UTF8Encoding();
            hashValue = sha256.ComputeHash(objUtf8.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < (hashValue.Length); i++)
            {
                sBuilder.Append(hashValue[i].ToString("x2"));
            }
            return sBuilder.ToString();

            //return hashValue;
        }
            //public static string EncrytMD5(string stringToEncrypt){
                  //    string ret="";
        //    byte[] data = UTF8Encoding.UTF8.GetBytes(stringToEncrypt);
        //    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        //    {
        //        byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        //        using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
        //        {
        //            ICryptoTransform transform = tripDes.CreateEncryptor();
        //            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
        //            ret = Convert.ToBase64String(results, 0, results.Length);
        //        }
        //    }
        //    return ret;

        //}

        //public static string DecryptMD5(string strtoDecrypt)
        //{

        //    string ret = "";
        //    byte[] data = Convert.FromBase64String(strtoDecrypt); // decrypt the incrypted text
        //    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        //    {
        //        byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
        //        using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
        //        {
        //            ICryptoTransform transform = tripDes.CreateDecryptor();
        //            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
        //            ret = UTF8Encoding.UTF8.GetString(results);
        //        }
        //    }
        //    return ret;
        //}
    }
}
