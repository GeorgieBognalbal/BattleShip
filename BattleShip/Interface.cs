using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    internal class Interface
    {
        public void MainMenu() 
        {
            Console.WriteLine(@"











                                       __________         __    __  .__           _________.__    .__              
                                       \______   \_____ _/  |__/  |_|  |   ____  /   _____/|  |__ |__|_____  ______
                                        |    |  _/\__  \\   __\   __\  | _/ __ \ \_____  \ |  |  \|  \____ \/  ___/
                                        |    |   \ / __ \|  |  |  | |  |_\  ___/ /        \|   Y  \  |  |_> >___ \ 
                                        |______  /(____  /__|  |__| |____/\___  >_______  /|___|  /__|   __/____  >
                                               \/      \/                     \/        \/      \/   |__|       \/ 
                                                                
                                                             Press Enter to Start the Game
                                                              Press F1 for Game Tutorial
                                                                  Press Esc to Exit       
");
        }
        public void Tutorial() 
        {
            Console.WriteLine(@"
                                       __________         __    __  .__           _________.__    .__              
                                       \______   \_____ _/  |__/  |_|  |   ____  /   _____/|  |__ |__|_____  ______
                                        |    |  _/\__  \\   __\   __\  | _/ __ \ \_____  \ |  |  \|  \____ \/  ___/
                                        |    |   \ / __ \|  |  |  | |  |_\  ___/ /        \|   Y  \  |  |_> >___ \ 
                                        |______  /(____  /__|  |__| |____/\___  >_______  /|___|  /__|   __/____  >
                                               \/      \/                     \/        \/      \/   |__|       \/ 
                                                            
                                                           Game tutsorial coming soon...
");
        }
    }
}
