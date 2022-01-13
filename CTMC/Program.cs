using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
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
            Console.CursorVisible = false;
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

            //Create Session Directory.
            Globals.SimulationLogsPath = Path.Combine(Globals.ApplicationDataPath, "SimulationLogs");

            if (!Directory.Exists(Globals.SimulationLogsPath))
            {
                Directory.CreateDirectory(Globals.SimulationLogsPath);
            }
            
            Globals.MarkovProcesses = new List<MarkovProcess>();
            
            Globals.MainMenuInstance = new MainMenu(Globals.MainMenuHeader, Globals.MainMenuOptions);
            Globals.MatrixMenuInstance = new MatrixMenu(Globals.MatrixMenuHeader, Globals.DefaultOptions);
            Globals.ChainSimMenuInstance = new ChainSimMenu(Globals.ChaimSimMenuHeader, Globals.ChaimSimMenuOptions);
            Globals.MarkovMenuInstance = new MarkovMenu(Globals.MarkovMenuHeader, Globals.DefaultOptions);

            Globals.Controller = new MenuController();
            
            
            while (true)
            {
                Globals.Controller.Run();
            }
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