using System;
using System.Drawing;
using Colorful;
using Figgle;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole.Apps
{
    [App("App_Store")]
    class AppStoreApp : App
    {
        protected override void WriteAppTitle() 
        {
            // choose font on http://www.figlet.org/examples.html
            var font = FiggleFonts.Banner ;
            Console.WriteLine(font.Render("App Store"));
        }
        protected override string ReadPrefix { get { return "[App_Store]->"; } }
        protected override Color BackGroundColor { get { return Color.Bisque; } }
        protected override Color TextColor { get { return Color.Black; } }

        protected override void DoWhatYouGottaDo()
        {
            Console.WriteLine("App Store se právě aktualizuje. Může to trvat i několik hodin. Zkuste to prosím později.");
            Console.WriteLine("Stiskněte kteroukoliv klávesu...");
            var key = ReadKey();
            if(key.Key == ConsoleKey.Escape)
            {
                CrashApp();
            }
            CloseApp();
        }
    }
}
