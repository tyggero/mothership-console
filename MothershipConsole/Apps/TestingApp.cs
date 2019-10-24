using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole.Apps
{
    [App("test_app")]
    class TestingApp : App
    {
        protected override void WriteAppTitle() { Console.WriteLine("Welcome to the Testing App!"); }
        protected override string ReadPrefix { get { return "[TestingApp]->"; } }
        protected override ConsoleColor BackGroundColor { get { return ConsoleColor.DarkMagenta; } }
        protected override ConsoleColor TextColor { get { return ConsoleColor.White; } }

        protected override void DoWhatYouGottaDo()
        {
            var key = ReadKey();
            if(key.Key == ConsoleKey.Escape)
            {
                CrashApp();
            }
            CloseApp();
        }
    }
}
