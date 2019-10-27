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
    [App("Wolf_ProblemSolver")]
    class ProblemSolverApp : App
    {
        protected override void WriteAppTitle() 
        {
            // choose font on http://www.figlet.org/examples.html
            var font = FiggleFonts.Block ;
            Console.WriteLine(font.Render("WOLF"));
        }
        protected override string ReadPrefix { get { return ""; } }
        protected override Color BackGroundColor { get { return Color.DarkRed; } }
        protected override Color TextColor { get { return Color.Yellow; } }

        protected override void DoWhatYouGottaDo()
        {
            while (true)
            {
                ResetScreen();
                Console.WriteLine("[ESC] = Ukončit; [0-9] = Detail problému", Color.White);

                Console.WriteLine("Jsem Wolf, řeším problémy");
                Console.WriteLine("Analyzuji problémy lodi ZEVL...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Následuje až 10 nejakutnějších problémů, seřazených dle priority:");
                Console.WriteLine("");


                List<Problem> sortedUnsolved  = Repository.Problems
                                                            .Where(p => p.IsSolved == false) // not solved
                                                            .Where(p => String.IsNullOrEmpty(p.Solution)) // not even pending
                                                            .OrderBy(p => p.Priority)
                                                            .Take(10)
                                                            .ToList();

                foreach (var problem in sortedUnsolved)
                {
                    var index = sortedUnsolved.IndexOf(problem);
                    Console.WriteLine("  " + index + ". " + problem.Description);
                    Console.WriteLine();
                }

                // wait for key press
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.D0:
                        if(sortedUnsolved.Count() > 0)
                            Detail(sortedUnsolved[0]);
                        break;

                    case ConsoleKey.D1:
                        if (sortedUnsolved.Count() > 1)
                            Detail(sortedUnsolved[1]);
                        break;

                    case ConsoleKey.D2:
                        if (sortedUnsolved.Count() > 2)
                            Detail(sortedUnsolved[2]);
                        break;

                    case ConsoleKey.D3:
                        if (sortedUnsolved.Count() > 3)
                            Detail(sortedUnsolved[3]);
                        break;

                    case ConsoleKey.D4:
                        if (sortedUnsolved.Count() > 4)
                            Detail(sortedUnsolved[4]);
                        break;

                    case ConsoleKey.D5:
                        if (sortedUnsolved.Count() > 5)
                            Detail(sortedUnsolved[5]);
                        break;

                    case ConsoleKey.D6:
                        if (sortedUnsolved.Count() > 6)
                            Detail(sortedUnsolved[6]);
                        break;

                    case ConsoleKey.D7:
                        if (sortedUnsolved.Count() > 7)
                            Detail(sortedUnsolved[7]);
                        break;

                    case ConsoleKey.D8:
                        if (sortedUnsolved.Count() > 8)
                            Detail(sortedUnsolved[8]);
                        break;

                    case ConsoleKey.D9:
                        if (sortedUnsolved.Count() > 9)
                            Detail(sortedUnsolved[9]);
                        break;
                }
            }
        }

        private void Detail(Problem problem)
        {
            while (true)
            {
                ResetScreen();
                Console.WriteLine("[ESC] = zpět na seznam problémů", Color.White);

                Console.WriteLine("Problem Detail:");
                Console.WriteLine(problem.Description);
                Console.WriteLine("Priorita (nula je nejnižší): " + problem.Priority);

                // wait for key press
                var key = ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.Escape:
                        return;

                    case ConsoleKey.S:
                        ;
                        break;
                }
            }
        }
    }
}
