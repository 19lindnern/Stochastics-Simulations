using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Security.Policy;

namespace CTMC
{
    internal class Program
    {

        private static Menu MainMenu;
        private static Menu MatrixMenu;
        private static int InitialState;
        private static Matrix Q;
        private static MarkovProcess M;
        
        
        public static void Main(string[] args)
        {
            InitialState = 0;
            
            //Set up application data path
            Globals.ApplicationDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string[] paths = {Globals.ApplicationDataPath, "CTMC"};
            Globals.ApplicationDataPath = Path.Combine(paths);
            
            //Set up matrices path
            paths[0] = Globals.ApplicationDataPath;
            paths[1] = "Matrices";
            Globals.MatricesPath = Path.Combine(paths);
            
            //Create Data Path if it doesnt exist
            if (!Directory.Exists(Globals.ApplicationDataPath))
            {
                Directory.CreateDirectory(Globals.ApplicationDataPath);
            }
            
            //Create Matrix Path if it doesnt exist
            if (!Directory.Exists(Globals.MatricesPath))
            {
                Directory.CreateDirectory(Globals.MatricesPath);
            }
            
            MainMenu = new Menu(Globals.MainMenuHeader, Globals.MainMenuOptions);
            
            while (true)
            {
                RunMainMenu();
            }
        }

        public static void RunMainMenu()
        {
            Console.Clear();
            MainMenu.DrawOptions();
            
            ConsoleKeyInfo keyPress = Console.ReadKey();

            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                MainMenu.IncrementSelection();
            }
            
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                MainMenu.DecrementSelection();
            }
            
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                switch (MainMenu.GetSelection())
                {
                    case 0:
                        Console.Clear();
                        BuildMatrixMenu();
                        RunMatrixMenu();
                        break;
                    case 1:
                        Console.Clear();
                        RunSimulationMenu();
                        break;
                    case 2:
                        Console.Clear();
                        SetInitialState();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }
            }
            
            Console.Clear();
            return;
            
        }

        public static void SetInitialState()
        {
            Console.Clear();
            try
            {
                Console.WriteLine($"Set initial state. Must be between 0 and {Q.GetCols().ToString()}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Please choose a matrix first!");
                Console.ReadKey(true);
                return;
            }
            
            string input = Console.ReadLine();
            try
            {
                int state = Int32.Parse(input);
                if (state >= Q.GetCols() || state < 0)
                {
                    throw new ArgumentException("Invalid user input");
                }

                InitialState = state;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input. Press any key to continue");
                Console.ReadKey(true);
                SetInitialState();
            }
        }
        public static void RunSimulationMenu()
        {
            
        }
        
        

        public static void RunMatrixMenu()
        {
            MatrixMenu.DrawOptions();
            
            ConsoleKeyInfo keyPress = Console.ReadKey();

            if (keyPress.Key == ConsoleKey.UpArrow)
            {
                MatrixMenu.IncrementSelection();
            }
            
            else if (keyPress.Key == ConsoleKey.DownArrow)
            {
                MatrixMenu.DecrementSelection();
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
                    Q = new Matrix(paths[MatrixMenu.GetSelection()]);
                    Console.WriteLine($"{paths[MatrixMenu.GetSelection()]} loaded successfully.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey(true);
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
            RunMatrixMenu();
        }

        public static void BuildMatrixMenu()
        {
            string[] options = Directory.GetFiles(Globals.MatricesPath);
            for (int i = 0; i < options.Length; i++)
            {
                options[i] = Path.GetFileName(options[i]);
            }
            string header = "Select a matrix file. Press 'Q' to return to the main menu.";
            MatrixMenu = new Menu(header, options);
        }
        
        
        private static void GetMatrixFromUser(string matrixFilePath)
        {
            int cols = 0;
            int rows = 0;
            bool stageValid = false;
            Console.Out.WriteLine($"Matrix file not found, creating file at {matrixFilePath}");
            Console.Out.WriteLine($"Enter dimensions: rows cols");
            using (System.IO.StreamWriter writer = new StreamWriter(matrixFilePath))
            {
                while (!stageValid)
                {
                    try
                    {
                        string[] input = Console.ReadLine().Split(' ');
                        if (input.Length < 2)
                        {
                            Console.Out.WriteLine("Please enter 2 dimensions");
                        }

                        rows = int.Parse(input[0], NumberStyles.Integer);
                        cols = int.Parse(input[1], NumberStyles.Integer);
                        writer.WriteLine(input[0] + " " + input[1]);
                        stageValid = true;
                    }
                    catch (System.NullReferenceException e)
                    {
                        Console.Out.WriteLine("Invalid Input");
                    }
                }

                for (int i = 0; i < rows; i++)
                {
                    stageValid = false;
                    while (!stageValid)
                    {
                        Console.Out.WriteLine($"Enter {cols} real number entries for row {i}:");
                        string input = Console.ReadLine();
                        if (input.Split(' ').Length != cols)
                        {
                            Console.Out.WriteLine("Incorrect number of entries.");
                            continue;
                        }
                        writer.WriteLine(input);
                        stageValid = true;
                    }
                }


            }
        }
    }
}