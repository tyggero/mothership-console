using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MothershipConsole.Apps;

namespace MothershipConsole
{
    class MothershipConsole
    {
        private static IEnumerable<ListApp> Apps;
        private static IEnumerable<Command> Commands;
        


        static void Main(string[] args)
        {
            Console.WriteLine("Loading main Mothership terminal...");
            Console.WriteLine("...");

            // get apps
            Apps = GetImplementedApps();

            //get commands
            Commands = GetCommands();

            //infinite command loop
            while (true) {
                HandleCommand();
            }
        }





        #region Commands
        // COMMAND DEFINITIONS

        [Command("help")]
        [CommandHelp("Lists all available commands")]
        public static bool HelpCommand(string arg = "")
        {
            Console.WriteLine("Available Commands:");
            foreach (var command in Commands)
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
            int loadRate = random.Next(0, 50);
            while (percent < 100)
            {
                percent += random.Next(0, loadRate);
                percent = Math.Min(percent, 100);
                Console.Write("\rLaunching app " + listApp.Name + "... " + percent + " %");
                System.Threading.Thread.Sleep(200);
            }
            Console.WriteLine();

            //run the app
            if (app.Run())
            {
                Console.WriteLine("App '" + listApp.Name + "' was closed");
            }
            else
            {
                Console.WriteLine("There was an error in the '" + listApp.Name + "' app");
            }

            return true;
        }

        [Command("clear")]
        [CommandHelp("Clears the screen")]
        public static bool ClearCommand(string arg = "")
        {
            Console.Clear();

            return true;
        }

        #endregion









        #region Other Methods
        // OTHER METHODS

        static bool HandleCommand()
        {
            Console.WriteLine();
            Console.Write("$-Network/Ship/OS> ");

            string input = Console.ReadLine();

            string[] words = input.Split(new string[] { " " }, 2, StringSplitOptions.None);
            string inputCommand = words[0];
            string inputArgs = words.Length > 1 ? words[1] : null;

            Command command = Commands.Where(c => c.Name == inputCommand).FirstOrDefault();

            if (command == null)
            {
                Console.WriteLine("Command '" + inputCommand + "' not found");
                Console.WriteLine("Use 'help' for available commands");

                return false;
            }

            if (command.Method.GetParameters().FirstOrDefault()?.ParameterType == typeof(string))
            {
                command.Method.Invoke(null, new object[] { inputArgs });
            }
            else
            {
                command.Method.Invoke(null, new object[] { } );
            }

            return true;
        }


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

        static IEnumerable<Command> GetCommands()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(MothershipConsole));

            var commands = typeof(MothershipConsole).GetMethods()
                      .Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0)
                      .Select(m => new Command()
                      {
                          Name = ((CommandAttribute)(m.GetCustomAttributes(typeof(CommandAttribute), true).First())).Name
                                                  ,
                          Help = ((CommandHelpAttribute)(m.GetCustomAttributes(typeof(CommandHelpAttribute), true).First())).Help
                                                  ,
                          Method = m
                      })
                      .ToList()
                      ;

            return commands;
        }

        #endregion
    }
}
