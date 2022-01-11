using System;

namespace CTMC
{
    public class ChainSimMenu : Menu
    {
        public ChainSimMenu(string header, string[] options)
        {
            Options = options;
            Header = header;
            Selection = 0;
        }

        public int Run()
        {
            Console.Clear();
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
                    return Selection;
            }

            return -1;
        }
    }
}