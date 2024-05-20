using System.Security.Cryptography;
using System.Text;

namespace Apps.Settings;

internal static class EncryptionHelper
{
    public static string Sha256Hash(string data)
    {
        return Convert.ToHexString(Sha256HashBytes(data));
    }

    public static byte[] Sha256HashBytes(string data)
    {
        var inputBytes = Encoding.UTF8.GetBytes(data);
        return SHA256.HashData(inputBytes);
    }

    public static byte[] GetEncryptionKey(string password)
    {
        return Sha256HashBytes(password);
    }

    public static string EncryptString(string plaintext, string password)
    {
        return EncryptString(plaintext, GetEncryptionKey(password));
    }

    public static string EncryptString(string plaintext, byte[] encryptionKey)
    {
        using (Aes aes = Aes.Create())
        {
            var iv = aes.IV;
            aes.Key = encryptionKey;
            using (var memStream = new System.IO.MemoryStream())
            {
                memStream.Write(iv, 0, iv.Length);  // Add the IV to the first 16 bytes of the encrypted value
                using (var cryptStream = new CryptoStream(memStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {
                    using (var writer = new System.IO.StreamWriter(cryptStream))
                    {
                        writer.Write(plaintext);
                    }
                }
                var buf = memStream.ToArray();
                return Convert.ToBase64String(buf, 0, buf.Length);
            }
        }          
    }

    public static string DecryptString(string encrypted, string password)
    {
        return DecryptString(encrypted, GetEncryptionKey(password));
    }

    public static string DecryptString(string encrypted, byte[] encryptionKey)
    {
        var bytes = Convert.FromBase64String(encrypted);
        using (Aes aes = Aes.Create())
        {
            aes.Key = encryptionKey;

            using (var memStream = new System.IO.MemoryStream(bytes))
            {

                var iv = new byte[16];
                memStream.Read(iv, 0, 16);  // Pull the IV from the first 16 bytes of the encrypted value
                aes.IV = iv;

                using (var cryptStream = new CryptoStream(memStream, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Read))
                {
                    using (var reader = new System.IO.StreamReader(cryptStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}