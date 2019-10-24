using System;
using System.Drawing;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole.Apps
{
    [App("test_app")]
    class TestingApp : App
    {
        protected override void WriteAppTitle() { Console.WriteAscii("THE TESTING APP!", Color.White); }
        protected override string ReadPrefix { get { return "[TestingApp]->"; } }
        protected override Color BackGroundColor { get { return Color.DarkMagenta; } }
        protected override Color TextColor { get { return Color.White; } }

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
