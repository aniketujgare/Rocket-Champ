﻿using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System;

public static class Helper 
{
    //Serialize

    public static string serialize<T>(this T toSerialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }
    //Deserialize
    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(typeof(T));
        StringReader reader = new StringReader(toDeserialize);
        return (T)xml.Deserialize(reader);
    }
    private static string hash = "281099@aniketujgare";
    //Encrypt
    public static string Encrypt(string input)
    {
        string r = "";
        byte[] data = UTF8Encoding.UTF8.GetBytes(input);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using(TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key =key, Mode = CipherMode.ECB})
            {
                ICryptoTransform tr = trip.CreateEncryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }
        return r;
    }
    //Decrypt
    public static string Decrypt(string input)
    {
        byte[] data = Convert.FromBase64String(input);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB })
            {
                ICryptoTransform tr = trip.CreateDecryptor();
                byte[] result = tr.TransformFinalBlock(data, 0, data.Length);
                return UTF8Encoding.UTF8.GetString(result);
            }
        }
    }
    //D
}
