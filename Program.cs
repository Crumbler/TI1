using System;
namespace TI1
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Choose the encryption method:");
                Console.WriteLine("1) Railroad cypher");
                Console.WriteLine("2) Column cipher");
                Console.WriteLine("3) RGrid cipher");
                Console.WriteLine("4) Playfair cipher");

                string choice = Console.ReadLine();
                int choiceNum = 0;

                if (int.TryParse(choice, out choiceNum))
                {
                    Console.WriteLine();
                    
                    switch (choiceNum)
                    {
                    case 1:
                        Rail.Switch();
                        break;

                    case 2:
                        Column.Switch();
                        break;

                    case 3:
                        RGrid.Switch();
                        break;

                    case 4:
                        Playfair.Switch();
                        break;

                    default:
                        Console.WriteLine("No such number");
                        break;
                    }
                    Console.WriteLine();
                }
                else
                    Console.WriteLine("You have not entered a number\n");
            }
        }
    }
}
