using System;
using System.ComponentModel.Design;
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
                    break;
                case 2:
                    break;
                case 3:
                    return;
            }
            RunChainSimMenu();
        }

       
    }
}