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
            return string.Empty;
        }

        private static string Decode(string message, string key)
        {
            return string.Empty;
        }
    }
}
