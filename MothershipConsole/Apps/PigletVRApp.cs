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
    [App("PigletVR")]
    class PigletVRApp : App
    {
        private CommandParser Parser;
        private bool WannaClose = false;
        
        protected override void WriteAppTitle() 
        {
            // choose font on http://www.figlet.org/examples.html
            var font = FiggleFonts.EftiFont ;
            Console.WriteLine(font.Render("Piglet VR"));
        }
        protected override string ReadPrefix { get { return "Co si přeješ?? "; } }
        protected override Color BackGroundColor { get { return Color.Gold; } }
        protected override Color TextColor { get { return Color.Chocolate; } }

        protected override void DoWhatYouGottaDo()
        {
            Parser = new CommandParser(typeof(PigletVRApp), this);


            while (WannaClose == false)
            {
                Parser.HandleCommand(ReadPrefix);
            }
        }

        [Command("help")]
        [CommandHelp("Vypíše dostupné příkazy")]
        public bool PomocCommand(string arg = "")
        {
            Console.WriteLine("Dostupné příkazy:");
            foreach (var command in Parser.Commands)
            {
                Console.WriteLine(" - '" + command.Name + "' (" + command.Help + ")");
            }
            return true;
        }

        [Command("nechci_nic_zazit")]
        [CommandHelp("Ukončí aplikaci")]
        public bool KonecCommand(string arg = "")
        {
            WannaClose = true;
            return true;
        }

        [Command("moje_VR_zazitky")]
        [CommandHelp("Vypíše tvé aktivované zážitky pro Virtuální Realitu")]
        public bool MojeZazitkyCommand(string arg = "")
        {
            Console.WriteLine("Toto jsou tvé aktivované zážitky pro VR!");
            foreach(var exp in Repository.Experiences.Where(e => String.IsNullOrEmpty(e.KeyToUnlock)))
            {
                Console.WriteLine(exp.Name, Color.DarkViolet);
            }

            return true;
        }

        [Command("aktivovat_zazitek_kodem")]
        [CommandHelp("Aktivuj nový zážitek, pokud máš aktivační klíč ;)")]
        public bool ActivateCommand(string arg = "")
        {
            //show loading
            var percent = 0;
            Random random = new Random();
            int loadRate = random.Next(1, 50);
            while (percent < 100)
            {
                percent += random.Next(1, loadRate);
                percent = Math.Min(percent, 100);
                Console.Write("\rZkouším aktivovat klíčem ... " + percent + " %");
                System.Threading.Thread.Sleep(200);
            }
            Console.WriteLine();

            List<Experience> exps = Repository.Experiences;
            Experience exp = exps.Where(e => e.KeyToUnlock == arg).FirstOrDefault();
            if (String.IsNullOrEmpty(arg) || exp == null)
            {
                Console.WriteLine("Klíč " + arg + " nic neodemkl :(", Color.Red);
                return false;
            }

            exp.KeyToUnlock = null; //this makes it activated
            Repository.Experiences = exps;

            Console.WriteLine("Klíč aktivoval novou aplikaci '" + exp.Name + "'");
            

            return true;
        }

        [Command("spustit")]
        [CommandHelp("Spustí vybraný VR zážitek (po příkazu zadej mezeru a název zážitku)")]
        public bool RunExperienceCommand(string arg = "")
        {
            Experience exp = Repository.Experiences.Where(e => String.IsNullOrEmpty(e.KeyToUnlock)
                                                            && e.Name == arg)
                                                                      .FirstOrDefault();
            if (String.IsNullOrEmpty(arg) || exp == null)
            {
                Console.WriteLine("Zážitek " + arg + " jsme tu bohužel nenašli :(", Color.Red);
                Console.WriteLine("(Pokud od něj máš aktivační klíč, můžeš si ho odemknout)");
                return false;
            }
            //show loading
            var percent = 0;
            Random random = new Random();
            int loadRate = random.Next(1, 50);
            while (percent < 100)
            {
                percent += random.Next(1, loadRate);
                percent = Math.Min(percent, 100);
                Console.Write("\rSpouštíme zážitek ... " + percent + " %");
                System.Threading.Thread.Sleep(200);
            }
            Console.WriteLine();

            Console.WriteLine(exp.Name + " běží. Nasaďte si své prasečí příslušenství ;)", Color.DarkMagenta);

            return true;
        }
    }
}
