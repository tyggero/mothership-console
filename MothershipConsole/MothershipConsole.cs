using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MothershipConsole.Apps;

namespace MothershipConsole
{
    class MothershipConsole
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Loading main Mothership terminal...");

            while (true) {
                Console.WriteLine("press any key to run Testing app");
                Console.ReadKey(true);
                Console.WriteLine();
                var testing = new Testing();
                testing.Run();
                Console.WriteLine();
            }
        }
    }
}
