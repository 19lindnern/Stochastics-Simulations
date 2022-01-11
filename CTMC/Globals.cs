using System.Collections.Generic;
using Microsoft.SqlServer.Server;

namespace CTMC
{
    public static class Globals
    {
        //Strings
        public const string MainMenuHeader = 
            @" 
  __  __               _                   _____  _             _           _____  _             
 |  \/  |             | |                 / ____|| |           (_)         / ____|(_)            
 | \  / |  __ _  _ __ | | __ ___ __   __ | |     | |__    __ _  _  _ __   | (___   _  _ __ ___   
 | |\/| | / _` || '__|| |/ // _ \\ \ / / | |     | '_ \  / _` || || '_ \   \___ \ | || '_ ` _ \  
 | |  | || (_| || |   |   <| (_) |\ V /  | |____ | | | || (_| || || | | |  ____) || || | | | | | 
 |_|  |_| \__,_||_|   |_|\_\\___/  \_/    \_____||_| |_| \__,_||_||_| |_| |_____/ |_||_| |_| |_|
            ";
        public const string ChaimSimMenuHeader = 
            @" 
  __  __               _                   _____  _             _           _____  _             
 |  \/  |             | |                 / ____|| |           (_)         / ____|(_)            
 | \  / |  __ _  _ __ | | __ ___ __   __ | |     | |__    __ _  _  _ __   | (___   _  _ __ ___   
 | |\/| | / _` || '__|| |/ // _ \\ \ / / | |     | '_ \  / _` || || '_ \   \___ \ | || '_ ` _ \  
 | |  | || (_| || |   |   <| (_) |\ V /  | |____ | | | || (_| || || | | |  ____) || || | | | | | 
 |_|  |_| \__,_||_|   |_|\_\\___/  \_/    \_____||_| |_| \__,_||_||_| |_| |_____/ |_||_| |_| |_|
            ";
        public const string MatrixMenuHeader = "Select a matrix file. Press 'Q' to return to the main menu.";
        public const string MarkovMenuHeader = "Select Markov Process to simulate";
        
        
        //Readonly string lists
        public static readonly string[] DefaultOptions = {"Press Q to return to previous menu."};
        public static readonly string[] MainMenuOptions = 
            {"Markov Process","Birth Death Process (Coming soon)", "Time Dependent Markov Process(Coming Before I Die)", "Exit"};
        public static readonly string[] ChaimSimMenuOptions =
            {"Run Simulation", "Add New Markov Process", "Edit Initial States", "Back"};
        
        //Paths
        public static string ApplicationDataPath;
        public static string MatricesPath;
        
        //Menus
        public static MainMenu MainMenuInstance;
        public static MatrixMenu MatrixMenuInstance;
        public static MarkovMenu MarkovMenuInstance;
        public static ChainSimMenu ChainSimMenuInstance;
        public static MenuController Controller;
        
        //Mathematical objects
        public static Matrix Q;
        public static List<MarkovProcess> MarkovProcesses;
    }
}