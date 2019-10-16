using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole
{
    public abstract class App
    {
        protected virtual string ReadPrefix
        {
            get { return "[UnknownApp]->"; }
        }

        protected ConsoleKeyInfo ReadKey()
        {
            Console.Write(ReadPrefix);
            return Console.ReadKey();
        }

        public abstract bool Run();
    }
}
