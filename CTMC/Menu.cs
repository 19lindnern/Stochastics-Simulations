using System;
using System.Reflection.Emit;

namespace CTMC
{
    public class Menu
    {
        protected int Selection;
        protected string[] Options;
        protected string Header;

        protected Menu()
        {
            Header = "Default Menu Header";
            Options = new string[2];
            Selection = 0;
        }
        
        public Menu(string header, string[] options)
        {
            Header = header;
            Options = options;
            Selection = 0;
        }
        
        private int mod(int x, int m) {
            return (x%m + m)%m;
        }

        public void DrawOptions()
        {
            Console.WriteLine(Header);
            for (int i = 0; i < Options.Length; i++)
            {
                if (Selection == i)
                {
                    Console.WriteLine(">" + Options[i]);
                }
                else
                {
                    Console.WriteLine(Options[i]);
                }
            }
        }

        public void IncrementSelection()
        {
            Selection = (Selection + 1) % Options.Length;
        }
        
        public void DecrementSelection()
        {
            Selection = mod(Selection - 1, Options.Length);
        }

        public int GetSelection()
        {
            return Selection;
        }

        public string GetOption(int i)
        {
            return Options[i];
        }
        
    }
}