using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip
{
    public class Design
    {
        public void PlacementStage_Header()
        {
            Console.WriteLine(@" 
 ____  __     __    ___  ____  _  _  ____  __ _  ____        ____  ____  __    ___  ____ 
(  _ \(  )   / _\  / __)(  __)( \/ )(  __)(  ( \(_  _)      / ___)(_  _)/ _\  / __)(  __)
 ) __// (_/\/    \( (__  ) _) / \/ \ ) _) /    /  )(        \___ \  )( /    \( (_ \ ) _) 
(__)  \____/\_/\_/ \___)(____)\_)(_/(____)\_)__) (__)       (____/ (__)\_/\_/ \___/(____)");
        }

        public void header()
        {
            Console.WriteLine(@"
     ____  __     __   _  _  ____  ____                             ____   __  ____ 
    (  _ \(  )   / _\ ( \/ )(  __)(  _ \                           (  _ \ /  \(_  _)
     ) __// (_/\/    \ )  /  ) _)  )   /                            ) _ ((  O ) )(  
    (__)  \____/\_/\_/(__/  (____)(__\_)                           (____/ \__/ (__)          
");
        }

        public void header_Player2()
        {
            Console.WriteLine(@"
 ____  __     __   _  _  ____  ____    ____ 
(  _ \(  )   / _\ ( \/ )(  __)(  _ \  (___ \
 ) __// (_/\/    \ )  /  ) _)  )   /   / __/
(__)  \____/\_/\_/(__/  (____)(__\_)  (____)
");
        }
        public void MainMenu()
        {
            string[,] menu =
                {
                {@" ███████████             █████     █████    ████            █████████  █████       ███           "},
                {@"▒▒███▒▒▒▒▒███           ▒▒███     ▒▒███    ▒▒███           ███▒▒▒▒▒███▒▒███       ▒▒▒            "},
                {@" ▒███    ▒███  ██████   ███████   ███████   ▒███   ██████ ▒███    ▒▒▒  ▒███████   ████  ████████ "},
                {@" ▒██████████  ▒▒▒▒▒███ ▒▒▒███▒   ▒▒▒███▒    ▒███  ███▒▒███▒▒█████████  ▒███▒▒███ ▒▒███ ▒▒███▒▒███"},
                {@" ▒███▒▒▒▒▒███  ███████   ▒███      ▒███     ▒███ ▒███████  ▒▒▒▒▒▒▒▒███ ▒███ ▒███  ▒███  ▒███ ▒███"},
                {@" ▒███    ▒███ ███▒▒███   ▒███ ███  ▒███ ███ ▒███ ▒███▒▒▒   ███    ▒███ ▒███ ▒███  ▒███  ▒███ ▒███"},
                {@" ███████████ ▒▒████████  ▒▒█████   ▒▒█████  █████▒▒██████ ▒▒█████████  ████ █████ █████ ▒███████ "},
                {@"▒▒▒▒▒▒▒▒▒▒▒   ▒▒▒▒▒▒▒▒    ▒▒▒▒▒     ▒▒▒▒▒  ▒▒▒▒▒  ▒▒▒▒▒▒   ▒▒▒▒▒▒▒▒▒  ▒▒▒▒ ▒▒▒▒▒ ▒▒▒▒▒  ▒███▒▒▒  "},
                {@"                                                                                       ▒███      "},
                {@"                                                                                       █████     "},
                {@"                                                                                      ▒▒▒▒▒      "}

                };

            int x = 30, y = 13;

            foreach (string items in menu)
            {
                Thread.Sleep(50);
                Console.SetCursorPosition(x, y);
                Console.WriteLine(items);

                y++;
            }
        }

        public void MenuOptions()
        {
            string[] options =
            {
                @"                                   Press Enter to Start the Game                                 ",
                @"                                      Press T for Game Tutorial                                  ",
                @"                                         Press Esc to Exit                                       "
            };

            int x = 30, y = 24;

            foreach (string items in options)
            {
                Thread.Sleep(50);
                Console.SetCursorPosition(x, y);
                Console.WriteLine(items);

                y++;
            }
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
                                                            
           A   B   C   D   E   F   G   H   I   J
         ╔═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╦═══╗
     1   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║   this is the game board where you will be placing your ships.
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣   each player will get 5 ships to place on the board. bla bla bla
     2   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
     3   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
     4   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
     5   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
     6   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
     7   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
     8   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
     9   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╠═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╬═══╣
    10   ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║ ~ ║
         ╚═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╩═══╝
");
        }


        public void boarder()
        {
            string[,] boarder =
            {

       {@"┌──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┐"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"│                                                                                                                                                          │"},
       {@"└──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘"}

            };

            foreach (string item in boarder)
            {
                Thread.Sleep(5);
                Console.WriteLine(item);
            }
        }

        public void PlacementBoader()
        {
            string[,] boarder =
            {

       {@"┌──────────────────────────────────────────────PLACEMENT STAGE───────────────────────────────────────────────┐"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"│                                                                                                            │"},
       {@"└────────────────────────────────────────────────────────────────────────────────────────────────────────────┘"}

            };

            foreach (string item in boarder)
            {
                Console.WriteLine(item);
            }
        }

            /*
                DESCRIPTION : 
                    This class is for the global inventory or storage of ASCII ARTS and ASSETS variable
            */

            // -- RESET
            public static string RESET = "\u001b[0m";

            // -- TEXT COLOR
            public string RED = "\u001b[31m";
            public string GREEN = "\u001b[32m";
            public string YELLOW = "\u001b[33m";
            public string BLUE = "\u001b[34m";
            public string MAGENTA = "\u001b[35m";
            public string CYAN = "\u001b[36m";
            public string WHITE = "\u001b[37m";
            public string BLACK = "\u001b[30m";

            // -- BACKGROUND COLOR    
            public string RED_BG = "\u001b[41m";
            public string GREEN_BG = "\u001b[42m";
            public string YELLOW_BG = "\u001b[43m";
            public string BLUE_BG = "\u001b[44m";
            public string MAGENTA_BG = "\u001b[45m";
            public string CYAN_BG = "\u001b[46m";
            public string WHITE_BG = "\u001b[47m";
            public string BLACK_BG = "\u001b[40m";

        /* COLOR NOTES:

            0 - black
            1 - red
            2 - green
            3 - yellow
            4 - blue
            5 - magenta
            6 - cyan
            7 - white

        */
        public string SetColor(string color, string text) 
        { 
            return text = color + text + RESET;
        }
    }
}
