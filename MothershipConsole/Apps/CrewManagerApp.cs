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
    [App("Manazer_Posadky")]
    class CrewManagerApp : App
    {
        private CommandParser Parser;
        private bool WannaClose = false;
        
        protected override void WriteAppTitle() 
        {
            // choose font on http://www.figlet.org/examples.html
            var font = FiggleFonts.Doom ;
            Console.WriteLine(font.Render("Manazer Posadky"));
        }
        protected override string ReadPrefix { get { return "[Manazer Posadky]->"; } }
        protected override Color BackGroundColor { get { return Color.Coral; } }
        protected override Color TextColor { get { return Color.White; } }

        protected override void DoWhatYouGottaDo()
        {
            Parser = new CommandParser(typeof(CrewManagerApp), this);


            while (WannaClose == false)
            {
                Parser.HandleCommand("Zadejte příkaz: ");
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

        [Command("konec")]
        [CommandHelp("Ukončí aplikaci")]
        public bool KonecCommand(string arg = "")
        {
            WannaClose = true;
            return true;
        }

        [Command("seznam_posadky")]
        [CommandHelp("Vypíše registrované členy posádky")]
        public bool SeznamPosadkyCommand(string arg = "")
        {
            foreach(var member in Repository.CrewMembers)
            {
                Console.WriteLine(member.Name + " " + member.Surname);
            }

            return true;
        }

        [Command("zobraz")]
        [CommandHelp("Vypíše podrobný záznam o členovi posádky. Zadejte ve formátu 'zobraz Jméno Příjmení'")]
        public bool ZobrazCommand(string arg = "")
        {
            CrewMember member = Repository.CrewMembers.Where(m => m.Name + " " + m.Surname == arg).FirstOrDefault();

            if (member == null)
            {
                Console.WriteLine("Člen posádky " + arg + " nenalezen");
                return false;
            }

            Console.WriteLine("Jméno: " + member.Name);
            Console.WriteLine("Příjmení: " + member.Surname);
            Console.WriteLine("Tým: " + member.Team);
            Console.WriteLine("Číslo: " + member.PhoneNumber);
            Console.WriteLine("Email: " + member.Email);
            Console.WriteLine("Planeta Narození: " + member.BirthPlanet);

            return true;
        }

        [Command("odeber")]
        [CommandHelp("Smaže záznam o členovi posádky. Zadejte ve formátu 'odeber Jméno Příjmení'")]
        public bool OdeberCommand(string arg = "")
        {
            List<CrewMember> members = Repository.CrewMembers;
            CrewMember member = members.Where(m => m.Name + " " + m.Surname == arg).FirstOrDefault();

            if (member == null)
            {
                Console.WriteLine("Člen posádky " + arg + " nenalezen");
                return false;
            }

            members.RemoveAll(m => m.Name + " " + m.Surname == arg);

            Repository.CrewMembers = members;
            Console.WriteLine("Člen posádky " + arg + " byl úspěšně odebrán");

            return true;
        }

        [Command("pridej_clena")]
        [CommandHelp("Přidá nového člena posádky")]
        public bool PridejClenaCommand(string arg = "")
        {
            CrewMember newMember = new CrewMember();

            Console.Write("Zadejte Jméno: ");
            newMember.Name = Console.ReadLine();
            Console.Write("Zadejte Příjmení: ");
            newMember.Surname = Console.ReadLine();
            Console.Write("Zadejte Barvu Týmu: ");
            newMember.Team = Console.ReadLine();

            List<CrewMember> expected = Repository.ExpectedCrewMembers;
            
            List<CrewMember> matching = expected.Where(e => e.Name == newMember.Name
                                                                          && e.Surname == newMember.Surname
                                                                          && e.Team == newMember.Team).ToList();

            if (matching.Count() == 0)
            {
                Console.WriteLine("Tato osoba se na lodi nenachází! (Zkontrolujte Jméno, Příjmení a Tým");
                return false;
            }

            List<CrewMember> members = Repository.CrewMembers;
            members.Add(newMember);
            Repository.CrewMembers = members;

            Console.WriteLine("Člen úspěšně přidán");

            return true;
        }
    }
}
