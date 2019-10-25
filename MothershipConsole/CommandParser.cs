using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole
{
    public class CommandParser
    {
        public IEnumerable<Command> Commands;
        public readonly Type CommandsClass;

        public CommandParser(Type commandsClass)
        {
            CommandsClass = commandsClass;

            //find commands in the specified Class
            LoadCommands();
        }


        public bool HandleCommand(string commandPrefix = "")
        {
            Console.WriteLine();
            Console.Write(commandPrefix);

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
                command.Method.Invoke(null, new object[] { });
            }

            return true;
        }


        private void LoadCommands()
        {
            var commands = CommandsClass.GetMethods()
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

            Commands = commands;
        }
    }

    
}
