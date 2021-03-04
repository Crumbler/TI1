using System;
using System.Text;
using System.Linq;

namespace TI1
{
    public static class RGrid
    {
        public static void Switch()
        {
            Console.WriteLine("Enter 1 to encode");
            Console.WriteLine("Enter 2 to decode");

            int command = Convert.ToInt32(Console.ReadLine());

            switch (command)
            {
            case 1:
                Console.WriteLine("Enter the message to encode:");
                string s1 = Console.ReadLine();

                int dim = s1.Length / 4;
                Console.WriteLine("Enter {0} quadrant number", s1.Length - s1.Length % 2);
                string key = Console.ReadLine();

                Console.WriteLine(Encode(s1, key));
                break;

            case 2:
                Console.WriteLine("Enter the message to decode:");
                string s2 = Console.ReadLine();
                Console.WriteLine(Decode(s2, string.Empty));
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
