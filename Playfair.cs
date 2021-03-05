using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace TI1
{
    public static class Playfair
    {
        public static void Switch()
        {
            Console.WriteLine("Enter the message:");
            string msg = Console.ReadLine();

            Console.WriteLine("Enter the key");
            string key = Console.ReadLine();

            if (msg.Any(c => c < 'a' || c > 'z') || key.Any(c => c < 'a' || c > 'z'))
            {
                Console.WriteLine("The message and the key must contain only lowercase English letters");
                return;
            }

            Console.WriteLine("Enter 1 to encode");
            Console.WriteLine("Enter 2 to decode");
            int command = Convert.ToInt32(Console.ReadLine());

            switch (command)
            {
            case 1:
                Console.WriteLine(Encode(msg, key));
                break;

            case 2:
                Console.WriteLine(Decode(msg, key));
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
            }
        }

        private static string Encode(string message, string key)
        {
            var M = new char[5, 5];
            
            key = String.Concat(key.Distinct());

            var alphabet = Enumerable.Range(0, 25)
                                     .Select(i => (char)(i + 'a'));

            string otherLetters = String.Concat(alphabet.Except(key));

            // Positions of letters in the matrix
            var positions = new Dictionary<char, (int, int)>();

            // Fill matrix with distinct key letters
            int ind = 0;
            for (; ind < key.Length; ++ind)
            {
                M[ind / 5, ind % 5] = key[ind];
                positions.Add(key[ind], (ind / 5, ind % 5));
            }

            // Fill the remaining matrix symbols with the leftovers
            for (int j = 0; ind < 25; ++ind, ++j)
            {
                M[ind / 5, ind % 5] = otherLetters[j];
                positions.Add(otherLetters[j], (ind / 5, ind % 5));
            }

            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                    Console.Write(M[i, j].ToString() + ' ');
                Console.WriteLine();
            }

            var pairs = new List<(char, char)>();

            int exes = 0;

            // Add 'x' in the middle of duplicate letter pairs
            for (int i = 0; i < message.Length - 1; i += 2)
                if (message[i] != message[i + 1])
                    pairs.Add((message[i], message[i + 1]));
                else
                {
                    pairs.Add((message[i], 'x'));
                    --i;
                    ++exes;
                }

            // Add x if number of chars is odd
            if ((message.Length + exes) % 2 == 1)
                pairs.Add((message[^1], 'x'));

            var res = new StringBuilder();

            Console.WriteLine(String.Join(' ', pairs));

            foreach (var pair in pairs)
            {
                (int, int) pos1 = positions[pair.Item1],
                           pos2 = positions[pair.Item2];

                // Switch based on row/column number equivalence
                switch((pos1.Item1 == pos2.Item1, pos1.Item2 == pos2.Item2))
                {
                    // Rectangle
                    case (false, false):
                        res.Append(M[pos1.Item1, pos2.Item2]);
                        res.Append(M[pos2.Item1, pos1.Item2]);
                        break;

                    // Same row
                    case (true, _):
                        res.Append(M[pos1.Item1, (pos1.Item2 + 1) % 5]);
                        res.Append(M[pos1.Item1, (pos2.Item2 + 1) % 5]);
                        break;

                    // Same column
                    case (_, true):
                        res.Append(M[(pos1.Item1 + 1) % 5, pos1.Item2]);
                        res.Append(M[(pos2.Item1 + 1) % 5, pos1.Item2]);
                        break;
                }
            }

            return res.ToString();
        }

        private static string Decode(string message, string key)
        {
            var M = new char[5, 5];
            
            key = String.Concat(key.Distinct());

            var alphabet = Enumerable.Range(0, 25)
                                     .Select(i => (char)(i + 'a'));

            string otherLetters = String.Concat(alphabet.Except(key));

            // Positions of letters in the matrix
            var positions = new Dictionary<char, (int, int)>();

            // Fill matrix with distinct key letters
            int ind = 0;
            for (; ind < key.Length; ++ind)
            {
                M[ind / 5, ind % 5] = key[ind];
                positions.Add(key[ind], (ind / 5, ind % 5));
            }

            // Fill the remaining matrix symbols with the leftovers
            for (int j = 0; ind < 25; ++ind, ++j)
            {
                M[ind / 5, ind % 5] = otherLetters[j];
                positions.Add(otherLetters[j], (ind / 5, ind % 5));
            }

            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                    Console.Write(M[i, j].ToString() + ' ');
                Console.WriteLine();
            }

            var pairs = new List<(char, char)>();

            // Add 'x' in the middle of duplicate letter pairs
            for (int i = 0; i < message.Length - 1; i += 2)
                if (message[i] != message[i + 1])
                    pairs.Add((message[i], message[i + 1]));
                else
                {
                    pairs.Add((message[i], 'x'));
                    --i;
                }

            // Add x if number of chars is odd
            if (message.Length % 2 == 1)
                pairs.Add((message[^1], 'x'));

            var res = new StringBuilder();

            Console.WriteLine(String.Join(' ', pairs));

            foreach (var pair in pairs)
            {
                (int, int) pos1 = positions[pair.Item1],
                           pos2 = positions[pair.Item2];

                // Switch based on row/column number equivalence
                switch((pos1.Item1 == pos2.Item1, pos1.Item2 == pos2.Item2))
                {
                    // Rectangle
                    case (false, false):
                        res.Append(M[pos1.Item1, pos2.Item2]);
                        res.Append(M[pos2.Item1, pos1.Item2]);
                        break;

                    // Same row
                    case (true, _):
                        res.Append(M[pos1.Item1, (pos1.Item2 + 4) % 5]);
                        res.Append(M[pos1.Item1, (pos2.Item2 + 4) % 5]);
                        break;

                    // Same column
                    case (_, true):
                        res.Append(M[(pos1.Item1 + 4) % 5, pos1.Item2]);
                        res.Append(M[(pos2.Item1 + 4) % 5, pos1.Item2]);
                        break;
                }
            }

            return res.ToString();
        }
    }
}
