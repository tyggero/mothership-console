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
            Console.WriteLine("Available Apps:");
            foreach (var app in Apps)
            {
                Console.WriteLine(" - " + app.Name);
            }

            //get commands
            Commands = GetCommands();
            Console.WriteLine("Available Commands:");
            foreach (var command in Commands)
            {
                Console.WriteLine(" - " + command.Name + " (" + command.Help + ")");
            }

            Console.WriteLine("");

            //infinite command loop
            while (true) {
                Console.WriteLine("press any key to run Testing app");
                Console.ReadKey(true);
                Console.WriteLine();
                var testing = new Testing();
                testing.Run();
                Console.WriteLine();
            }
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
                      .Select(m => new Command() {
                                                    Name = ((CommandAttribute)(m.GetCustomAttributes(typeof(CommandAttribute), true).First())).Name
                                                  , Help = ((CommandHelpAttribute)(m.GetCustomAttributes(typeof(CommandHelpAttribute), true).First())).Help
                                                  , Method = m
                      })
                      .ToList()
                      ;

            return commands;
        }



        [Command("test")]
        [CommandHelp("Yells and does nothing")]
        public static bool TestCommand(string arg)
        {
            Console.WriteLine("I am soooooooooo testy!");

            return true;
        }
    }
}
