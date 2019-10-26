using System;
using System.Drawing;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MothershipConsole.Apps;
using MothershipConsole.Model;

namespace MothershipConsole
{
    class MothershipConsole
    {
        private static IEnumerable<ListApp> Apps;
        private static CommandParser Parser;
        
        static void Main(string[] args)
        {
            //write some lines
            Console.WriteLine("Loading main Mothership terminal...");
            Console.WriteLine("...");
#if !DEBUG
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Insert your ship's connection key: ");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Automatically using the last connection key '4,kamzik,muka,7,24,nekricime,asie,mlada boleslav,7,svojsik'");
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("Connecting to ship ZEVL...");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Succesfully connected to space ship ZEVL!");
#endif

            // get apps
            Apps = GetImplementedApps();

            //get commands
            Parser = new CommandParser(typeof(MothershipConsole));

            //infinite command loop
            while (true) {
                Parser.HandleCommand("$-Network/Ship/OS> ");
            }
        }





#region Commands
        // COMMAND DEFINITIONS

        [Command("help")]
        [CommandHelp("Lists all available commands")]
        public static bool HelpCommand(string arg = "")
        {
            Console.WriteLine("Available Commands:");
            foreach (var command in Parser.Commands)
            {
                Console.WriteLine(" - '" + command.Name + "' (" + command.Help + ")");
            }
            return true;
        }

        [Command("apps")]
        [CommandHelp("Lists all installed apps")]
        public static bool AppCommand(string arg = "")
        {
            Console.WriteLine("Available Apps:");
            foreach (var app in Apps)
            {
                Console.WriteLine(" - " + app.Name);
            }
            return true;
        }

        [Command("run")]
        [CommandHelp("Run a specified app i.e. 'run AppName'")]
        public static bool RunCommand(string arg = "")
        {
            //get the app
            ListApp listApp = Apps.Where(a => a.Name == arg).FirstOrDefault();
            if (listApp == null)
            {
                Console.WriteLine("App '" + arg + "' was not found");
                return false;
            }
            App app = (App)Activator.CreateInstance(listApp.Class);

            //show loading
            var percent = 0;
            Random random = new Random();
            int loadRate = random.Next(1, 50);
            while (percent < 100)
            {
                percent += random.Next(1, loadRate);
                percent = Math.Min(percent, 100);
                Console.Write("\rLaunching app " + listApp.Name + "... " + percent + " %");
                System.Threading.Thread.Sleep(200);
            }
            Console.WriteLine();

            //run the app
            var WasAlright = app.Run();

            System.Threading.Thread.Sleep(1000);

            ResetScreen();

            if (WasAlright)
            {
                Console.WriteLine("App '" + listApp.Name + "' was closed");
            }
            else
            {
                Console.WriteLine("There was an error in the '" + listApp.Name + "' app");
            }

            System.Threading.Thread.Sleep(1000);

            return true;
        }

        [Command("clear")]
        [CommandHelp("Clears the screen")]
        public static bool ClearCommand(string arg = "")
        {
            ResetScreen();

            return true;
        }

        [Command("unlock_doors")]
        [CommandHelp("Unlocks all door")]
        public static bool UnlockCommand(string arg = "")
        {
            //Lockdown
            var expected = Repository.ExpectedCrewMembers;
            var members = Repository.CrewMembers;

            var missing = 0;
            foreach (var exp in expected)
            {
                if (members.Where(e => e.Name == exp.Name
                                     && e.Surname == exp.Surname
                                     && e.Team == exp.Team).Count() == 0)
                {
                    missing++;
                }
            }

            if (missing > 0)
            {
                Console.WriteLine(missing + " unexpected humans found on the ship. All door locked for safety.");
            }
            else
            {
                Console.WriteLine("Unlocking all door!!");
            }
            return true;
        }

#endregion









#region Other Methods
        // OTHER METHODS

        static IEnumerable<ListApp> GetImplementedApps()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(MothershipConsole));

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(AppAttribute), true).Length > 0)
                {
                    var listApp = new ListApp();
                    listApp.Class = type;
                    listApp.Name = ((AppAttribute)(type.GetCustomAttributes(typeof(AppAttribute), true).First())).AppName;

                    yield return listApp;
                }
            }
        }

        static void ResetScreen()
        {
            Console.BackgroundColor = Color.Black;
            Console.Clear();
            Console.ForegroundColor = Color.White;
        }

#endregion
    }
}
