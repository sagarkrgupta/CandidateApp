using System;
using System.Security.Cryptography;
using System.Text;

namespace CandidateApp.Domain.Shared.Utilities
{
    public static class AESUtil
    {
        // Method to generate a random AES secret key
        public static byte[] GenerateSecretKey()
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 128; // Set the key size to 128 bits
                aesAlg.GenerateKey();
                return aesAlg.Key;
            }
        }

        // Method to encrypt an integer id to a string using AES
        public static string EncryptIntToString(int id)
        {
            byte[] key = GenerateSecretKey();

            // Convert the integer to a byte array
            byte[] idBytes = Encoding.UTF8.GetBytes(id.ToString());

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key; // Use the generated secret key
                aesAlg.GenerateIV(); // Generate a random initialization vector (IV)
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = encryptor.TransformFinalBlock(idBytes, 0, idBytes.Length);

                // Combine the IV and the encrypted data for transmission
                byte[] result = new byte[aesAlg.IV.Length + encryptedBytes.Length];
                Array.Copy(aesAlg.IV, 0, result, 0, aesAlg.IV.Length);
                Array.Copy(encryptedBytes, 0, result, aesAlg.IV.Length, encryptedBytes.Length);

                // Return the result as a Base64 encoded string
                return Convert.ToBase64String(result);
            }
        }

        // Method to decrypt the encrypted string back to an integer
        public static int DecryptStringToInt(string encryptedStr)
        {
            byte[] key = GenerateSecretKey();

            // Decode the Base64 encoded string
            byte[] encryptedBytes = Convert.FromBase64String(encryptedStr);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key; // Use the same secret key
                                  // Extract the IV from the beginning of the encrypted data
                byte[] iv = new byte[aesAlg.BlockSize / 8];
                Array.Copy(encryptedBytes, 0, iv, 0, iv.Length);
                aesAlg.IV = iv;

                // The rest is the actual encrypted data
                byte[] cipherText = new byte[encryptedBytes.Length - iv.Length];
                Array.Copy(encryptedBytes, iv.Length, cipherText, 0, cipherText.Length);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);

                // Convert the decrypted bytes back to an integer
                string decryptedString = Encoding.UTF8.GetString(decryptedBytes);
                return int.Parse(decryptedString);
            }
        }


        //// Generate a random AES key (you should store this key securely in a real application)
        //byte[] key = GenerateSecretKey();

        //// Sample integer
        //int id = 1249;

        //// Encrypt the integer
        //string encryptedString = EncryptIntToString(id, key);
        //Console.WriteLine("Encrypted ID: " + encryptedString);

        //    // Decrypt the string back to the original integer
        //    int decryptedId = DecryptStringToInt(encryptedString, key);
        //Console.WriteLine("Decrypted ID: " + decryptedId);


    }
}
