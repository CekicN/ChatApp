using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp
{
    public class Bifid
    {
        private char[,] polybiusSquare;
        public Bifid()
        {
            polybiusSquare = CreatePolybiusSquare();
        }

        private char[,] CreatePolybiusSquare()
        {
            string alphabet = "phqgmeaylnofdxkrcvszwbuti";
            alphabet = alphabet.ToUpper();
            char[,] polybiusSquare = new char[5, 5];

            for (int i = 0; i < 25; i++)
            {
                polybiusSquare[i / 5, i % 5] = alphabet[i];
            }

            return polybiusSquare;
        }

        public string Encrypt(string message)
        {
            message = message.Replace(" ", "");
            message = message.ToUpper();
            var tuples = message.Select(FindPosition).Where(r => r.Item1 != -1 && r.Item2 != -1);

            var row = tuples.Select(tuple => tuple.Item1);
            var col = tuples.Select(tuple => tuple.Item2);

            var numbers = row.Concat(col).ToList();

            var cypherText = Enumerable.Range(0, numbers.Count() - 1)
                .Where(i => i % 2 == 0)
                .Select(i => GetLetter(numbers[i], numbers[i + 1]))
                .ToArray();

            return new string(cypherText);
        }

        public Tuple<int,int> FindPosition(char ch)
        {
            if (ch == '\r' || ch == '\n')
                return Tuple.Create(-1, -1);

            for (int rowIdx = 0; rowIdx < 5; rowIdx++)
            {
                for (int colIdx = 0; colIdx < 5; colIdx++)
                {
                    if (polybiusSquare[rowIdx, colIdx] == ch)
                    {
                        return Tuple.Create(rowIdx, colIdx);
                    }
                }
            }

            throw new ArgumentException($"Character '{ch}' not found in the Polybius square.");
        }

        private char GetLetter(int x, int y)
        {
            return this.polybiusSquare[x,y];
        }

        public string Decrypt(string cypherText)
        {
            var tuples = cypherText.Select(FindPosition).SelectMany(tuple => new[] { tuple.Item1, tuple.Item2 });

            var half = tuples.Count() / 2;
            var xCoordinates = tuples.Take(half);
            var yCoordinates = tuples.Skip(half);

            var message = xCoordinates.Zip(yCoordinates, GetLetter);

            return new string(message.ToArray());
        }

    }
}
