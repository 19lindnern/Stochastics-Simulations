using System.Runtime.Remoting.Messaging;
using System;

namespace CTMC
{
    public class MainMenu : Menu
    {
        public MainMenu(string header, string[] options)
        {
            Header = header;
            Options = options;
            Selection = 0;
        }

        public int Run() //Returns -1 if enter is not pressed. Returns index of selected option if enter is pressed.
        {
            Console.Clear();
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
            
            else if (keyPress.Key == ConsoleKey.Enter)
            {
                switch (Selection)
                {
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        return Selection;
                }
            }
            
            Console.Clear();
            return -1;
        }
        
    }
}