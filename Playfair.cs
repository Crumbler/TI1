using System;
using System.Text;
using System.Linq;

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

            int i = 0;
            for (; i < key.Length; ++i)
                M[i / 5, i % 5] = key[i];

            for (int j = 0; i < 25; ++i, ++j)
                M[i / 5, i % 5] = otherLetters[j];

            for (i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                    Console.Write(M[i, j].ToString() + ' ');
                Console.WriteLine();
            }

            return string.Empty;
        }

        private static string Decode(string message, string key)
        {
            return string.Empty;
        }
    }
}
