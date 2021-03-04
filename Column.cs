using System;
using System.Text;
using System.Linq;

namespace TI1
{
    public static class Column
    {
        public static void Switch()
        {
            Console.Write("Enter the key phrase: ");
            string key = Console.ReadLine();

            Console.WriteLine("Enter 1 to encode");
            Console.WriteLine("Enter 2 to decode");

            int command = Convert.ToInt32(Console.ReadLine());

            switch (command)
            {
            case 1:
                Console.WriteLine("Enter the message to encode:");
                string s1 = Console.ReadLine();
                Console.WriteLine(Encode(s1, key));
                break;

            case 2:
                Console.WriteLine("Enter the message to decode:");
                string s2 = Console.ReadLine();
                
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
            }
        }

        private static string Encode(string message, string key)
        {
            // Get the relative sorted indices of each char
            int[] clmns = key.Select((c, i) => (c, i))
                           .OrderBy(entry => entry.c)
                           .Select(entry => entry.i)
                           .ToArray();

            

            return string.Empty;
        }
    }
}
