using System;
using System.IO;
using System.Linq;

namespace CTMC
{
    public class MatrixMenu : Menu
    {
        public MatrixMenu(string header, string[] options)
        {
            Header = header;
            Options = options;
            Selection = 0;
        }

        public void Run()
        {
            DrawOptions();
            
            ConsoleKeyInfo keyPress = Console.ReadKey();

            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                IncrementSelection();
            }
            
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                DecrementSelection();
            }
            
            else if (keyPress.Key == ConsoleKey.Q)
            {
                Console.Clear();
                return;
            }
            
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                string[] paths = Directory.GetFiles(Globals.MatricesPath);
                try
                {
                    Globals.Q = new Matrix(paths[Selection]);
                    Console.WriteLine($"{paths[Selection]} loaded successfully.");

                    if (!Globals.Q.IsGenerator())
                    {
                        Console.WriteLine($"{paths[Selection]} does not contain a valid generator matrix.");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey(true);
                        Console.Clear();
                        Run();
                    }
                    
                    
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                    return;
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Matrix file invalid.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
                }
            }
            Console.Clear();
            Run();
        }

        public void BuildMatrixMenu()
        {
            string[] options = Directory.GetFiles(Globals.MatricesPath);
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = Path.GetFileName(options[i]);
            }
            Options = options;
        }
    }
}