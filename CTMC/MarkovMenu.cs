using System;
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
                case ConsoleKey.Enter:
                    break; //Will need to handle case where no processes are available. 
                case ConsoleKey.Q:
                    return;
            }

            Run();
        }
        
    }
}