using System;
using System.Diagnostics;
using System.IO;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;

namespace CTMC
{
    public class MarkovMenu : Menu
    {
        public MarkovMenu(string header, string[] options)
        {
            Options = options;
            Header = header;
            Selection = 0;
        }

        public void BuildMarkovMenu()
        {
            if (Globals.MarkovProcesses.Count == 0)
            {
                Options = Globals.DefaultOptions;
                return;
            }
            Options = new string[Globals.MarkovProcesses.Count];
            for (int i = 0; i < Options.Length; i++)
            {
                Options[i] = Globals.MarkovProcesses[i].GetName();
            }
        }

        public void Run()
        {
            Console.Clear();

            if (Globals.MarkovProcesses.Count == 0)
            {
                Console.WriteLine("No Markov Processes are currently active, please add a new Markov Process.");
                Console.ReadKey(true);
                return;
            }
            
            DrawOptions();
            ConsoleKeyInfo keyPress = Console.ReadKey();

            switch (keyPress.Key)
            {
                case ConsoleKey.UpArrow:
                    IncrementSelection();
                    break;
                case ConsoleKey.DownArrow:
                    DecrementSelection();
                    break;
                case ConsoleKey.Enter: //Run simulation when one is selected
                    Console.Clear();
                    Console.WriteLine("Please enter maximum time:");
                    Console.CursorVisible = true;
                    
                    try //May have bad input from when we ask for time. Need to catch potential error.
                    {
                        double time = Double.Parse(Console.ReadLine());
                        Stopwatch stopwatch = new Stopwatch(); //Time simulation
                        Console.CursorVisible = false;
                        stopwatch.Start();
                        MarkovProcess m = Globals.MarkovProcesses[Selection]; 
                        m.Simulate(time); //Run sim with max time
                        stopwatch.Stop();
                        Console.WriteLine($"Simulation completed in {stopwatch.ElapsedMilliseconds.ToString()}ms");
                        string path = Path.Combine(Globals.SimulationLogsPath,
                            m.GetName() + "-" +
                            m.GetSimCount().ToString()); //TODO: Fix the construction of sim log file name.
                        m.WriteToFile(path);
                        Console.WriteLine($"Simulation log written to file at {path}");
                        Console.ReadKey(true);
                    }
                    catch (Exception e)
                    {
                        Console.CursorVisible = false;
                        Console.WriteLine("Invalid user input, press any key to continue");
                        Console.ReadKey(true);
                        Run();
                        return;
                    }
                    break; 
                case ConsoleKey.Q:
                    return;
            }

            Run();
        }
        
    }
}