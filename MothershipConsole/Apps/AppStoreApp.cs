using System;
using System.Drawing;
using Colorful;
using Figgle;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MothershipConsole.Model;

namespace MothershipConsole.Apps
{
    [App("App_Store")]
    class AppStoreApp : App
    {
        private CommandParser Parser;
        private bool WannaClose = false;

        protected override void WriteAppTitle() 
        {
            // choose font on http://www.figlet.org/examples.html
            var font = FiggleFonts.Banner ;
            Console.WriteLine(font.Render("App Store"));
        }
        protected override string ReadPrefix { get { return "[App_Store]-> "; } }
        protected override Color BackGroundColor { get { return Color.Bisque; } }
        protected override Color TextColor { get { return Color.Black; } }

        protected override void DoWhatYouGottaDo()
        {
            Parser = new CommandParser(typeof(AppStoreApp), this);

            while (WannaClose == false)
            {
                Parser.HandleCommand(ReadPrefix);
            }
        }



        [Command("help")]
        [CommandHelp("Vypíše dostupné příkazy")]
        public bool HelpCommand(string arg = "")
        {
            Console.WriteLine("Dostupné příkazy:");
            foreach (var command in Parser.Commands)
            {
                Console.WriteLine(" - '" + command.Name + "' (" + command.Help + ")");
            }
            return true;
        }

        [Command("konec")]
        [CommandHelp("Ukončí aplikaci")]
        public bool KonecCommand(string arg = "")
        {
            string ans = "";
            while (ans.ToLower() != "n" && ans.ToLower() != "y")
            {
                Console.Write("Jste si jistí, že si nechcete pořídit ještě víc aplikací? (y/N): ");
                ans = Console.ReadLine();
            }

            if (ans.ToLower() == "n")
            {
                Console.Write("Pokračujeme v nakupování!!!");
                return true;
            }
            else
            {
                WannaClose = true;
                return true;
            }
        }

        [Command("nabidka_aplikaci")]
        [CommandHelp("Vypíše všechny aplikace dostupné k nainstalování")]
        public bool OfferedAppsCommand(string arg = "")
        {
            Console.WriteLine("Aplikace, dostupné na našem obchodě:");
            foreach (var app in Repository.StoreApps)
            {
                Console.WriteLine(" - '" + app.Name + "'");
            }
            Console.WriteLine(" - ...");
            Console.WriteLine("Slabý signál... Některé aplikace se možná nepovedlo načíst.");
            Console.WriteLine("Vraťte se prosím později ;)");

            return true;
        }

        [Command("nainstaluj")]
        [CommandHelp("Vypíše všechny aplikace dostupné k nainstalování")]
        public bool InstallCommand(string arg = "")
        {
            List<ListApp> offered = Repository.StoreApps;

            var app = offered.Where(a => a.Name == arg).FirstOrDefault();
            if (app == null)
            {
                Console.WriteLine("V obchodě není dostupná aplikace " + arg);
                return false;
            }

            List<ListApp> installed = Repository.InstalledApps;
            if (installed.Where(i => i.Name == app.Name).Count() > 0)
            {
                Console.WriteLine("Aplikace " + arg + " již je nainstalována");
                return false;
            }


            Console.WriteLine("Instaluji...");
            Console.WriteLine("");
            System.Threading.Thread.Sleep(7000);
            installed.Add(app);
            Repository.InstalledApps = installed;
            Console.WriteLine("Aplikace " + arg + " byla úspěšně nainstalována");

            return true;
        }
    }
}
