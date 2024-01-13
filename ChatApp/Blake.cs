using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Blake
    {
        private uint[] h;
        private uint[] s;
        private uint[] t;
        private int buflen;
        private bool nullt;
        private byte[] buf;

        private static readonly uint[] u256 = {
        0x243f6a88, 0x85a308d3, 0x13198a2e, 0x03707344,
        0xa4093822, 0x299f31d0, 0x082efa98, 0xec4e6c89,
        0x452821e6, 0x38d01377, 0xbe5466cf, 0x34e90c6c,
        0xc0ac29b7, 0xc97c50dd, 0x3f84d5b5, 0xb5470917
        };
        private static readonly uint[][] sigma = {
            new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            new uint[] { 14, 10,4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3 },
            new uint[] { 11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4 },
            new uint[] { 7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8 },
            new uint[] { 9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13 },
            new uint[] { 2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9 },
            new uint[] { 12, 5, 1, 15, 14, 13, 4, 10, 0, 7, 6, 3, 9, 2, 8, 11 },
            new uint[] { 13, 11, 7, 14, 12, 1, 3, 9, 5, 0, 15, 4, 8, 6, 2, 10 },
            new uint[] { 6, 15, 14, 9, 11, 3, 0, 8, 12, 2, 13, 7, 1, 4, 10, 5 },
            new uint[] { 10, 2, 8, 4, 7, 6, 1, 5, 15, 11, 9, 14, 3, 12, 13, 0 },
            new uint[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
            new uint[] { 14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3 },
            new uint[] { 11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4 },
            new uint[] { 7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8 },
            new uint[] { 9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13 },
            new uint[] { 2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9 }
            };
        private static readonly byte[] padding = {
            0x80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };

        public Blake()
        {
            h = new uint[8];
            s = new uint[4];
            t = new uint[2];
            buflen = 0;
            nullt = false;
            buf = new byte[64];
        }

        public void Init()
        {
            h[0] = 0x6a09e667;
            h[1] = 0xbb67ae85;
            h[2] = 0x3c6ef372;
            h[3] = 0xa54ff53a;
            h[4] = 0x510e527f;
            h[5] = 0x9b05688c;
            h[6] = 0x1f83d9ab;
            h[7] = 0x5be0cd19;
            t[0] = t[1] = 0;
            buflen = 0;
            nullt = false;
            Array.Clear(s, 0, s.Length);
        }

        public void G(uint[] v, uint[] m, int i,int a, int b, int c, int d, int e)
        {
            v[a] += (m[sigma[i][e]] ^ u256[sigma[i][e + 1]]) + v[b];
            v[d] = ROT(v[d] ^ v[a], 16);
            v[c] += v[d];
            v[b] = ROT(v[b] ^ v[c], 12);
            v[a] += (m[sigma[i][e + 1]] ^ u256[sigma[i][e]]) + v[b];
            v[d] = ROT(v[d] ^ v[a], 8);
            v[c] += v[d];
            v[b] = ROT(v[b] ^ v[c], 7);
        }

        private uint ROT(uint x, int n)
        {
            return ((x << (32 - n)) | (x >> n));
        }

        public void Update(byte[] input, long inlen)
        {
            int left = buflen;
            int fill = 64 - left;

            if (left > 0 && inlen >= fill)
            {
                Array.Copy(input, 0, buf, left, fill);
                t[0] += 512;

                if (t[0] == 0) t[1]++;

                Compress(buf);
                Array.Copy(input, fill, input, 0, inlen - fill);
                inlen -= fill;
                left = 0;
            }

            while (inlen >= 64)
            {
                t[0] += 512;

                if (t[0] == 0) t[1]++;

                Compress(input);
                Array.Copy(input, 64, input, 0, inlen - 64);
                inlen -= 64;
            }

            if (inlen > 0)
            {
                Array.Copy(input, 0, buf, left, inlen);
                buflen = left + (int)inlen;
            }
            else
            {
                buflen = 0;
            }
        }

        public static uint U8TO32_BIG(byte[] p)
        {
            return ((uint)p[0] << 24) |
                   ((uint)p[1] << 16) |
                   ((uint)p[2] << 8) |
                   ((uint)p[3]);
        }

        private void Compress(byte[] block)
        {
            uint[] v = new uint[16];
            uint[] m = new uint[16];
            int i;

            for (i = 0; i < 16; ++i)
            {
                m[i] = U8TO32_BIG(block);
            }

            for (i = 0; i < 8; ++i)
            {
                v[i] = h[i];
            }

            v[8] = s[0] ^ u256[0];
            v[9] = s[1] ^ u256[1];
            v[10] = s[2] ^ u256[2];
            v[11] = s[3] ^ u256[3];
            v[12] = u256[4];
            v[13] = u256[5];
            v[14] = u256[6];
            v[15] = u256[7];

            if (!nullt)
            {
                v[12] ^= t[0];
                v[13] ^= t[0];
                v[14] ^= t[1];
                v[15] ^= t[1];
            }

            for (i = 0; i < 14; ++i)
            {
                G(v,m,i,0, 4, 8, 12, 0);
                G(v, m, i, 1, 5, 9, 13, 2);
                G(v, m, i, 2, 6, 10, 14, 4);
                G(v, m, i, 3, 7, 11, 15, 6);

                G(v, m, i, 0, 5, 10, 15, 8);
                G(v, m, i, 1, 6, 11, 12, 10);
                G(v, m, i, 2, 7, 8, 13, 12);
                G(v, m, i, 3, 4, 9, 14, 14);
            }

            for (i = 0; i < 16; ++i)
            {
                h[i % 8] ^= v[i];
            }

            for (i = 0; i < 8; ++i)
            {
                h[i] ^= s[i % 4];
            }
        }
    }
}
