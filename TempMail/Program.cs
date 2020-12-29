using System;

namespace TempMail
{
    class Program
    {
        public static string VERSION = "1.0.0";
        public static string AUTHOR = "Lilian DAMIENS";
        public static string DATE = "December 2020";

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Start();
            Exit();
        }

        /// <summary>
        /// Display the version of the program inside the console.
        /// </summary>
        public static void DisplayVersion()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{AUTHOR} - {DATE} - v{VERSION}");
        }

        /// <summary>
        /// Display one of the project's flag inside the console.
        /// </summary>
        public static void DisplayFlag()
        {
            string[] flags = new string[]
            {
                @"  __                                       .__.__   
_/  |_  ____   _____ ______   _____ _____  |__|  |  
\   __\/ __ \ /     \\____ \ /     \\__  \ |  |  |  
 |  | \  ___/|  Y Y  \  |_> >  Y Y  \/ __ \|  |  |__
 |__|  \___  >__|_|  /   __/|__|_|  (____  /__|____/
           \/      \/|__|         \/     \/         ",
                @"   __                                       _ __
  / /____  ____ ___  ____  ____ ___  ____ _(_) /
 / __/ _ \/ __ `__ \/ __ \/ __ `__ \/ __ `/ / / 
/ /_/  __/ / / / / / /_/ / / / / / / /_/ / / /  
\__/\___/_/ /_/ /_/ .___/_/ /_/ /_/\__,_/_/_/   
                 /_/                            ",
                @"  __                                       __ __ 
 |  |_.-----.--------.-----.--------.---.-|__|  |
 |   _|  -__|        |  _  |        |  _  |  |  |
 |____|_____|__|__|__|   __|__|__|__|___._|__|__|
                     |__|                        
                                                 
                                                 
                                                 "
            };
            int flagIndex = new Random().Next(flags.Length);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(flags[flagIndex]);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Start the main methods.
        /// </summary>
        public static void Start()
        {
            Session session = new Session();
            session.NewMail();
            DisplayFlag();
            DisplayVersion();
            string input = "";
            while (!input.Trim().Equals("exit"))
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("tempmail (" + session.mail.address + ") >>> ");
                Console.ForegroundColor = ConsoleColor.White;
                input = Console.ReadLine();
                input = String.IsNullOrWhiteSpace(input) ? "" : input;
                switch (input)
                {
                    case "help":
                        DisplayHelp();
                        break;
                    case "clear":
                        Console.Clear();
                        DisplayFlag();
                        break;
                    case "change mail":
                        session.NewMail();
                        break;
                    case "check mail":
                        session.CheckMail();
                        break;
                    case "save":
                        //Get the second word + parameters
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("[+] Received mails have been saved !");
                        break;
                    case string a when a.Contains("save mail"):
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("[!] Unable to save mail.");
                        break;
                    case string a when a.Contains("get mail"):
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("[!] Unable to get email.");
                        break;
                    case "":
                        break;
                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
        }

        /// <summary>
        /// Exit method, used to log etc...
        /// </summary>
        public static void Exit()
        {
            Console.WriteLine("Thanks for using tempmail program, press any key to exit...");
            Console.ReadKey();
            //todo logs
        }

        /// <summary>
        /// Display help banner inside the console.
        /// </summary>
        public static void DisplayHelp()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            string help = @"
help                             Display all the commands of the program
change mail                      Get a new mail
get mail <id>                    Get a specific received mail
check mail                       Check your current mailbox
save mail <id|optionnal>         Save all mails in the default directory, or save only the given mail throught the id
";
            Console.WriteLine(help);
        }
    }
}
