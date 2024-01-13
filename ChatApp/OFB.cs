using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    internal class OFB
    {
        private RC6 rc6;

        public OFB(byte[] key)
        {
            rc6 = new RC6();
            rc6.KeyExpansion(key);
        }

        public string Encrypt(string plaintext, string iv)
        {
            StringBuilder ciphertext = new StringBuilder();
            string encryptedIV = iv;

            for (int i = 0; i < plaintext.Length; i += 16)
            {
                string block = plaintext.Substring(i, Math.Min(16, plaintext.Length - i));

                encryptedIV = rc6.Encrypt(rc6.getByte(), encryptedIV);

                string encryptedBlock = XORStrings(block, encryptedIV);

                ciphertext.Append(encryptedBlock);
            }

            return ciphertext.ToString();
        }

        public string Decrypt(string ciphertext, string iv)
        {
            StringBuilder plaintext = new StringBuilder();
            string encryptedIV = iv;

            for (int i = 0; i < ciphertext.Length; i += 16)
            {
                string block = ciphertext.Substring(i, Math.Min(16, ciphertext.Length - i));

                // Encrypt the IV to produce a pseudorandom stream
                encryptedIV = rc6.Encrypt(rc6.getByte(), encryptedIV);

                // XOR the pseudorandom stream with the ciphertext block to get the plaintext
                string decryptedBlock = XORStrings(block, encryptedIV);

                plaintext.Append(decryptedBlock);
            }

            return plaintext.ToString();
        }

        private string XORStrings(string a, string b)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < Math.Min(a.Length, b.Length); i++)
            {
                char charA = a[i];
                char charB = b[i];

                result.Append((char)(charA ^ charB));
            }

            return result.ToString();
        }
    }
}
