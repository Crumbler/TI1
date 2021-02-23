using System;
using System.Text;
using System.Linq;

namespace TI1
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string input = Console.ReadLine();
                int depth = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(Decode(input, depth));
            }
        }

        static string Encode(string input, int depth)
        {
            var arrays = new StringBuilder[depth];

            for (int i = 0; i < depth; ++i)
                arrays[i] = new StringBuilder();
            
            int divisor = (depth - 1) * 2;

            for (int i = 0; i < input.Length; ++i)
            {
                int ind = Math.Abs((i % divisor) - (depth - 1));
                arrays[ind].Append(input[i]);
            }

            return string.Join(string.Empty, arrays.Reverse());
        }

        static string Decode(string input, int depth)
        {
            var lengths = new int[depth];
            var arrays = new StringBuilder[depth];

            for (int i = 0; i < depth; ++i)
            {
                lengths[i] = 0;
                arrays[i] = new StringBuilder();
            }
            
            int divisor = (depth - 1) * 2;

            for (int i = 0; i < input.Length; ++i)
            {
                int cind = Math.Abs((i % divisor) - (depth - 1));
                lengths[cind] += 1;
            }

            int stringInd = 0;

            for (int i = 0; i < input.Length; ++i)
            {
                while (lengths[stringInd] == 0)
                    ++stringInd;

                arrays[stringInd].Append(input[i]);
                lengths[stringInd] -= 1;
            }

            int[] indices = lengths;

            for (int i = 0; i < depth; ++i)
                indices[i] = 0;

            var res = new StringBuilder();

            for (int i = 0; i < input.Length; ++i)
            {
                int cind = Math.Abs((i % divisor) - (depth - 1));
                res.Append(arrays[cind][indices[cind]]);
                indices[cind] += 1;
            }

            return res.ToString();
        }
    }
}
