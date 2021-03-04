using System;
using System.Text;
using System.Linq;

namespace TI1
{
    public static class Rail
    {
        public static void Switch()
        {
            Console.Write("Enter the depth: ");
            int depth = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter 1 to encode");
            Console.WriteLine("Enter 2 to decode");

            int command = Convert.ToInt32(Console.ReadLine());

            switch (command)
            {
            case 1:
                Console.WriteLine("Enter the message to encode:");
                string s1 = Console.ReadLine();
                Console.WriteLine(Encode(s1, depth));
                break;

            case 2:
                Console.WriteLine("Enter the message to decode:");
                string s2 = Console.ReadLine();
                Console.WriteLine(Decode(s2, depth));
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
            }
        }

        private static string Encode(string input, int depth)
        {
            if (input.Length <= 1 | depth <= 1)
                return input;

            var arrays = new StringBuilder[depth];

            // Allocate depth substrings
            for (int i = 0; i < depth; ++i)
                arrays[i] = new StringBuilder();
            
            // Divisor for substring formula
            int divisor = (depth - 1) * 2;

            for (int i = 0; i < input.Length; ++i)
            {
                // Calculate substring index based on char index
                int ind = Math.Abs((i % divisor) - (depth - 1));
                arrays[ind].Append(input[i]);
            }
            
            return string.Join(string.Empty, arrays.Reverse());
        }

        private static string Decode(string input, int depth)
        {
            if (input.Length <= 1 || depth <= 1)
                return input;

            var lengths = new int[depth];
            var arrays = new StringBuilder[depth];

            for (int i = 0; i < depth; ++i)
            {
                lengths[i] = 0;
                arrays[i] = new StringBuilder();
            }
            
            int divisor = (depth - 1) * 2;

            // Calculate how many chars each substring will have
            for (int i = 0; i < input.Length; ++i)
            {
                int cind = Math.Abs((i % divisor) - (depth - 1));
                ++lengths[cind];
            }

            int stringInd = 0;

            // Fill the substrings
            for (int i = 0; i < input.Length; ++i)
            {
                arrays[stringInd].Append(input[i]);
                --lengths[stringInd];

                if (lengths[stringInd] == 0)
                    ++stringInd;
            }

            // Reverse the substring order
            arrays = arrays.Reverse().ToArray();

            int[] indices = lengths;

            for (int i = 0; i < depth; ++i)
                indices[i] = 0;

            var res = new StringBuilder();

            // Reconstruct string by ping-ponging among substrings
            for (int i = 0; i < input.Length; ++i)
            {
                int cind = Math.Abs((i % divisor) - (depth - 1));
                res.Append(arrays[cind][indices[cind]]);
                ++indices[cind];
            }

            return res.ToString();
        }
    }
}
