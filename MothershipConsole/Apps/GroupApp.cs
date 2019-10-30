using System;
using System.Drawing;
using Colorful;
using Figgle;
using Console = Colorful.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MothershipConsole.Apps;
using MothershipConsole.Model;

namespace MothershipConsole.Apps
{
    [App("GroupApp")]
    class GroupApp : App
    {
        protected override void WriteAppTitle() 
        {
            // choose font on http://www.figlet.org/examples.html
            var font = FiggleFonts.Epic ;
            Console.WriteLine(font.Render("Group App"));
        }
        protected override string ReadPrefix { get { return "[GroupApp]->"; } }
        protected override Color BackGroundColor { get { return Color.Purple; } }
        protected override Color TextColor { get { return Color.White; } }

        protected override void DoWhatYouGottaDo()
        {
            List<CrewMember> crewMembers = Repository.CrewMembers;
            Console.WriteLine("Počet členů: " + crewMembers.Count);

            int input = 0;

            while (input <= 0 || input > crewMembers.Count)
            {
                try
                {
                    Console.Write("Zadejte počet týmů (číslo větší než nula): ");
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    
                }
            }

            List<List<CrewMember>> teams = new List<List<CrewMember>>();

            for (int i = 0; i < input; i = i + 1)
            {
                List<CrewMember> team = new List<CrewMember>();

                teams.Add(team);
            }

            int NumberMember = 0;
            int NumberTeam = 0;
            foreach (CrewMember member in crewMembers)
            {
                teams[NumberTeam].Add(member);
                NumberTeam ++ ;
                if (NumberTeam == input)
                {
                    NumberTeam = 0;
                }
            }

            foreach (var team in teams)
            {
                Console.WriteLine("Team " + (teams.IndexOf(team) + 1));
                
                foreach (var member in team)
                {
                    Console.Write(member.Name + " ");
                    Console.WriteLine(member.Surname);
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
