using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole.Model
{
    public class StoreApp
    {
        public enum InstallState
        {
            NotInstalled,
            Downloading,
            Installing
        }
        
        public string Name;
        public string Description;
        public InstallState State;
    }
}
