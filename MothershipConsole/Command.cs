using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole
{
    public class Command
    {
        public string Name { get; set; }
        public string Help { get; set; }
        public MethodInfo Method { get; set; }
    }
}
