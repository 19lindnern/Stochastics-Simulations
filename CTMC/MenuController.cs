using System;
using System.ComponentModel.Design;
using System.Management.Instrumentation;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CTMC
{
    public class MenuController
    {
        public void Run()
        {
            int option = Globals.MainMenuInstance.Run();
            switch (option)
            {
                case 0:
                    Console.Clear();
                    RunChainSimMenu();
                    break;
                case -1:
                    return;
            }
        }

        private void RunChainSimMenu()
        {
            switch (Globals.ChainSimMenuInstance.Run())
            {
                case -1:
                    Console.Clear();
                    RunChainSimMenu();
                    return;
                case 0:
                    Console.Clear();
                    Globals.MarkovMenuInstance.BuildMarkovMenu();
                    Globals.MarkovMenuInstance.Run();
                    break;
                case 1:
                    AddNewMarkovProcess();
                    break;
                case 2:
                    break;
                case 3:
                    return;
            }
            RunChainSimMenu();
        }

        private int GetInitialState()
        {
            Console.Clear();
            try
            {
                Console.WriteLine($"Set initial state. Must be between 0 and {Globals.Q.GetCols().ToString()}");
            }
            catch (Exception e)
            {
                Console.WriteLine("Please choose a matrix first!");
                Console.ReadKey(true);
                return -1;
            }
            
            string input = Console.ReadLine();
            try
            {
                int state = Int32.Parse(input);
                if (state >= Globals.Q.GetCols() || state < 0)
                {
                    throw new ArgumentException("Invalid user input");
                }
                return state;
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Input. Press any key to continue");
                Console.ReadKey(true);
                return GetInitialState();
            }
            
        }

        private string GetName()
        {
            Console.Clear();
            Console.WriteLine("Please enter a name for the process:");
            return Console.ReadLine();
        }

        private void AddNewMarkovProcess()
        {
            Console.Clear();
            Globals.MatrixMenuInstance.BuildMatrixMenu();
            Globals.MatrixMenuInstance.Run();
            Console.CursorVisible = true;
            int initialState = GetInitialState();
            Console.CursorVisible = false;
                    
                    
            if (initialState != -1)
            {
                Console.CursorVisible = true;
                string name = GetName();
                Console.CursorVisible = false; 
                MarkovProcess m = new MarkovProcess(Globals.Q, initialState, name);
                Globals.MarkovProcesses.Add(m);
            }

            else
            {
                Console.WriteLine("Failed to create markov process. Press any key to continue.");
                Console.ReadKey(true);
            }
        }

    }
}