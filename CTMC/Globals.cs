using Microsoft.SqlServer.Server;

namespace CTMC
{
    public static class Globals
    {
        public const string MainMenuHeader = 
            @" 
  __  __               _                   _____  _             _           _____  _             
 |  \/  |             | |                 / ____|| |           (_)         / ____|(_)            
 | \  / |  __ _  _ __ | | __ ___ __   __ | |     | |__    __ _  _  _ __   | (___   _  _ __ ___   
 | |\/| | / _` || '__|| |/ // _ \\ \ / / | |     | '_ \  / _` || || '_ \   \___ \ | || '_ ` _ \  
 | |  | || (_| || |   |   <| (_) |\ V /  | |____ | | | || (_| || || | | |  ____) || || | | | | | 
 |_|  |_| \__,_||_|   |_|\_\\___/  \_/    \_____||_| |_| \__,_||_||_| |_| |_____/ |_||_| |_| |_|
            ";
        public static readonly string[] MainMenuOptions = {"Get Matrix","Run Simulation", "Set Initial State", "Exit"};
        public static string ApplicationDataPath;
        public static string MatricesPath;
    }
}