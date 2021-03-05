using System;
using System.Text;
using System.Linq;

namespace TI1
{
    public static class RGrid
    {
        public static void Switch()
        {
            Console.WriteLine("Enter the message:");
            string msg = Console.ReadLine();

            // Side length of the square matrix
            int depth = (int)Math.Ceiling(Math.Sqrt(msg.Length));

            // Number of elements in the square matrix
            int sqrDepth = depth * depth;

            // Number of elements in one of 4 rectangles
            int rCount = sqrDepth / 4;

            Console.WriteLine("Enter {0} numbers from 1 to 4", rCount);
            string sNums = Console.ReadLine();
            if (sNums.Any(c => c < '1' || c > '4') || sNums.Length != rCount)
            {
                Console.WriteLine("Invalid sequence");
                return;
            }

            Console.WriteLine("Enter 1 to encode");
            Console.WriteLine("Enter 2 to decode");
            int command = Convert.ToInt32(Console.ReadLine());

            switch (command)
            {
            case 1:
                Console.WriteLine(Encode(msg, sNums, rCount, depth));
                break;

            case 2:
                Console.WriteLine(Decode(msg, sNums, rCount, depth));
                break;

            default:
                Console.WriteLine("Invalid choice");
                break;
            }
        }

        // Rotates a vector clockwise
        // 1 = 90 degrees
        private static (int, int) RotCW((int, int) v, int angle)
        {
            switch(angle)
            {
                case 0:
                    return v;

                case 1:
                    return (-v.Item2, v.Item1);

                case 2:
                    return (-v.Item1, -v.Item2);

                case 3:
                    return (v.Item2, -v.Item1);
            }

            return default;
        }

        private static (int, int) Add((int, int) a, (int, int) b)
        {
            return (a.Item1 + b.Item1, a.Item2 + b.Item2);
        }

        private static string Encode(string message, string key, int rCount, int depth)
        {
            int[] rSequence = key.Select(c => (int)(c - '1')).ToArray();

            var corners = new (int, int)[4]
            {
                (0, 0),
                (depth - 1, 0),
                (depth - 1, depth - 1),
                (0, depth - 1)
            };

            // Offsets relative to the rectangle
            var rOffsets = new (int, int)[rCount];

            int hDepth = depth / 2 + depth % 2;
            int vDepth = depth / 2;

            for (int i = 0; i < vDepth; ++i)
                for (int j = 0; j < hDepth; ++j)
                    rOffsets[i * hDepth + j] = (j, i);

            // Offsets relative to the square matrix
            var gOffsets = new (int, int)[rCount];
            for (int i = 0; i < rCount; ++i)
                gOffsets[i] = Add(corners[rSequence[i]], RotCW(rOffsets[i], rSequence[i]));

            var M = new char[depth, depth];

            for (int i = 0; i < depth; ++i)
                for (int j = 0; j < depth; ++j)
                    M[i, j] = ' ';

            int maxChar = message.Length;

            bool hasExtra = maxChar == depth * depth && depth % 2 == 1;
            // If the side length is odd, the center element is set separately
            if (hasExtra)
                --maxChar;

            for (int i = 0; i < maxChar; ++i)
            {
                // Choose corner based on i
                int angle = (i / rCount) % 4;
                (int, int) basePos = corners[angle];
                (int, int) offset = RotCW(gOffsets[i % rCount], angle);
                (int, int) pos = Add(basePos, offset);
                M[pos.Item2, pos.Item1] = message[i];
            }

            // If the side length is odd, set the center element separately
            if (hasExtra)
                M[depth / 2, depth / 2] = message[message.Length - 1];

            var res = new StringBuilder();

            for (int i = 0; i < depth; ++i)
                for (int j = 0; j < depth; ++j)
                    res.Append(M[i, j]);

            return res.ToString() + '|';
        }

        private static string Decode(string message, string key, int rCount, int depth)
        {
            int[] rSequence = key.Select(c => (int)(c - '1')).ToArray();

            var corners = new (int, int)[4]
            {
                (0, 0),
                (depth - 1, 0),
                (depth - 1, depth - 1),
                (0, depth - 1)
            };

            // Offsets relative to the rectangle
            var rOffsets = new (int, int)[rCount];

            int hDepth = depth / 2 + depth % 2;
            int vDepth = depth / 2;

            for (int i = 0; i < vDepth; ++i)
                for (int j = 0; j < hDepth; ++j)
                    rOffsets[i * hDepth + j] = (j, i);

            // Offsets relative to the square matrix
            var gOffsets = new (int, int)[rCount];
            for (int i = 0; i < rCount; ++i)
                gOffsets[i] = Add(corners[rSequence[i]], RotCW(rOffsets[i], rSequence[i]));

            var M = new char[depth, depth];

            for (int i = 0; i < depth; ++i)
                for (int j = 0; j < depth; ++j)
                    M[i, j] = ' ';

            for (int i = 0; i < depth; ++i)
                for (int j = 0; j < depth; ++j)
                {
                    if (i * depth + j >= message.Length)
                        goto leaveCycle;
                    M[i, j] = message[i * depth + j];
                }
            leaveCycle:

            int maxChar = message.Length;

            // If the side length is odd, the center element is acquired separately
            bool hasExtra = maxChar == depth * depth && depth % 2 == 1;
            if (hasExtra)
                --maxChar;

            var res = new StringBuilder();

            for (int i = 0; i < maxChar; ++i)
            {
                // Choose corner based on i
                int angle = (i / rCount) % 4;
                (int, int) basePos = corners[angle];
                (int, int) offset = RotCW(gOffsets[i % rCount], angle);
                (int, int) pos = Add(basePos, offset);
                res.Append(M[pos.Item2, pos.Item1]);
            }

            // If the side length is odd, get the center element separately
            if (hasExtra)
                res.Append(M[depth / 2, depth / 2]);

            return res.ToString() + '|';
        }
    }
}
