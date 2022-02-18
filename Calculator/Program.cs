using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write down the expression:");
            string exprStr = Console.ReadLine();
            // it is required for the lexer that
            // the expression should end with the end of line symbol
            exprStr += '\0';
            try
            {
                var parser = new AST.Parser(exprStr);
                var rootOperand = parser.Parse();
                var solver = new AST.Solver.TreeSolver(rootOperand);

                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.WriteLine(string.Format("Parsed input expression: {0}", solver.ToString()));

                var res = solver.InitCalcualtion();

                Console.WriteLine(string.Format("The answer is {0}", res));

                ConsoleKeyInfo pressedKey;
                do
                {
                    Console.WriteLine("Do you want to repeat calculation? (Y/N)");
                    pressedKey = Console.ReadKey();

                    while (pressedKey.Key != ConsoleKey.Y && pressedKey.Key != ConsoleKey.N)
                    {
                        Console.WriteLine("\nPlease, press either Y or N");
                        pressedKey = Console.ReadKey();
                    }
                    if (pressedKey.Key == ConsoleKey.Y)
                    {
                        res = solver.InitCalcualtion();
                        Console.WriteLine(string.Format("The answer is {0}", res));
                    }
                    else
                    {
                        break;
                    }
                } while (pressedKey.Key == ConsoleKey.Y);
            } catch (Exception ex)
            {
                Console.WriteLine(string.Format("The exception is raised: {0}", ex.Message));
            }
        }
    }
}
