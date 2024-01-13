using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;

namespace ChatApp
{
    public class RC6
    {
        private const int rounds = 20;
        private const int blockSize = 16;
        private uint[] S = new uint[2 * rounds + 4];
        private string string_key = "vwqjpkgjerzicpxb";

        public static uint RotateLeft(uint value, int count)
        {
            return (value << count) | (value >> (32 - count));
        }

        public static uint RotateRight(uint value, int count)
        {
            return (value >> count) | (value << (32 - count));
        }

        public uint Odd(double x)
        {
            uint y = (uint)x;
            if ((double)y < x)
                y += 1;

            if (y % 2 == 0)
                y += 1;
            return y;
        }

        public byte[] getByte()
        {

            byte[] keyEncryptByte = new byte[string_key.Length];

            for (int i = 0; i < string_key.Length; i++)
            {
                keyEncryptByte[i] = (byte)string_key[i];
            }
            return keyEncryptByte;
        }
        public void KeyExpansion(byte[] keys)
        {

            uint[] L = new uint[(int)keys.Length];
            for (int g = 0; g < (int)keys.Length; g++)
                L[g] = Convert.ToUInt32(keys[g]);

            uint Pw = Odd((Math.E - 2) * Math.Pow(2, 32));
            uint Qw = Odd(((1 + Math.Sqrt(5)) / 2 - 1) * Math.Pow(2, 32));

            S[0] = Pw;

            for (int k = 1; k <= (2 * rounds + 3); k++)
                S[k] = S[k - 1] + Qw;

            uint A = 0;
            uint B = 0;
            uint i = 0;
            uint j = 0;

            int v = 3 * Math.Max(L.Length, 2 * rounds + 4);

            for (int k = 1; k <= v; k++)
            {
                A = S[i] = RotateLeft((S[i] + A + B), 3);
                B = L[j] = RotateLeft((L[j] + A + B), 3);
                i = (i + 1) % (2 * rounds + 4);
                j = (j + 1) % (uint)keys.Length;
            }

        }

        private uint[] prepareOneBlock(byte[] plaintext)
        {

            uint[] block = new uint[4];
            for (int i = 0; i < 4; i++)
                block[i] = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    block[i] = (block[i] << 8) ^ (uint)plaintext[i * 4 + j];

                }
            }
            return block;
        }

        private byte[] unprepareOneBlock(uint[] ciphertext)
        {
            byte[] chars = new byte[16];

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    chars[i * 4 + j] = (byte)((ciphertext[i] >> ((3 - j) * 8)) & 255);

            return chars;
        }

        private string dodajBlanko(string text)
        {
            int zaDodati = 16 - text.Length % 16;
            string vratiString = text;
            if (text.Length % 16 != 0)
            {
                for (int i = 0; i < zaDodati; i++)
                    vratiString += " ";
            }
            return vratiString;
        }

        private string[] podelitiString(string text)
        {
            int br = text.Length / 16;
            string[] podeljeniStringovi = new string[br];


            for (int i = 0; i < br; i++)
            {
                for (int j = 0; j < 16; j++)
                    podeljeniStringovi[i] += text[i * 16 + j];
            }
            return podeljeniStringovi;
        }

        private byte[] stringToByteArray(string text)
        {
            byte[] ba = new byte[16];
            for (int i = 0; i < 16; i++)
                ba[i] = (byte)text[i];

            return ba;
        }

        private string byteJaggedArrayToString(byte[][] everything)
        {
            string prihvatniString = "";

            for (int i = 0; i < everything.GetLength(0); i++)
                for (int j = 0; j < everything[i].Length; j++)
                    prihvatniString += (char)everything[i][j];

            return prihvatniString;
        }


        private byte[] EncryptOneBlock(byte[] plaintext)
        {

            uint[] block = prepareOneBlock(plaintext);


            uint A = block[0];
            uint B = block[1];
            uint C = block[2];
            uint D = block[3];

            B += S[0];
            D += S[1];
            for (int i = 1; i <= rounds; i++)
            {
                uint t = (B * (2 * B + 1));
                t = RotateLeft(t, 5);
                uint u = (D * (2 * D + 1));
                u = RotateLeft(u, 5);
                A = RotateLeft(A ^ t, (int)u) + S[2 * i];
                C = RotateLeft(C ^ u, (int)t) + S[2 * i + 1];
                uint temp = A;
                A = B;
                B = C;
                C = D;
                D = temp;
            }

            A += S[2 * rounds + 2];
            C += S[2 * rounds + 3];

            uint[] block2 = { A, B, C, D };
            byte[] returnValue = unprepareOneBlock(block2);
            return returnValue;



        }


        private byte[] DecryptOneBlock(byte[] ciphertext)
        {


            uint[] block = prepareOneBlock(ciphertext);

            uint A = block[0];
            uint B = block[1];
            uint C = block[2];
            uint D = block[3];

            C -= S[2 * rounds + 3];
            A -= S[2 * rounds + 2];
            for (int i = rounds; i >= 1; i--)
            {
                uint temp = D;
                D = C;
                C = B;
                B = A;
                A = temp;
                uint u = (D * (2 * D + 1));
                u = RotateLeft(u, 5);
                uint t = (B * (2 * B + 1));
                t = RotateLeft(t, 5);
                C = (RotateRight(C - S[2 * i + 1], (int)t) ^ u);
                A = (RotateRight(A - S[2 * i], (int)u) ^ t);
            }
            D -= S[1];
            B -= S[0];

            uint[] block2 = { A, B, C, D };
            byte[] returnValue = unprepareOneBlock(block2);
            return returnValue;
        }






        public string Encrypt(byte[] key, string prihvatniString)
        {
            KeyExpansion(key);



           
            prihvatniString = dodajBlanko(prihvatniString);



            string[] podeljeniStringovi = podelitiString(prihvatniString);


            byte[][] arrayOfEncryptedBlocks = new byte[podeljeniStringovi.Length][];
            for (int x = 0; x < arrayOfEncryptedBlocks.Length; x++)
                arrayOfEncryptedBlocks[x] = new byte[16];

            for (int i = 0; i < podeljeniStringovi.Length; i++)
            {
                byte[] oneBlock = stringToByteArray(podeljeniStringovi[i]);
                arrayOfEncryptedBlocks[i] = EncryptOneBlock(oneBlock);
            }

            string izlaz = byteJaggedArrayToString(arrayOfEncryptedBlocks);

            return izlaz;

        }

        public string Decrypt(byte[] key, string ulaz)
        {
            KeyExpansion(key);

            string prihvatniString = (ulaz);
            prihvatniString = dodajBlanko(prihvatniString);


            string[] podeljeniStringovi = podelitiString(prihvatniString);

            byte[][] arrayOfEncryptedBlocks = new byte[podeljeniStringovi.Length][];
            for (int x = 0; x < arrayOfEncryptedBlocks.Length; x++)
                arrayOfEncryptedBlocks[x] = new byte[16];


            for (int i = 0; i < podeljeniStringovi.Length; i++)
            {
                byte[] oneBlock = stringToByteArray(podeljeniStringovi[i]);
                arrayOfEncryptedBlocks[i] = DecryptOneBlock(oneBlock);
            }

            string izlaz = byteJaggedArrayToString(arrayOfEncryptedBlocks);

            return izlaz;

        }
    }
}
