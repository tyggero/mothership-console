using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole
{
    public class CommandHelpAttribute : Attribute
    {
        // The constructor is called when the attribute is set.
        public CommandHelpAttribute(string help)
        {
            m_Help = help;
        }

        // Keep a variable internally ...
        protected string m_Help;

        // .. and show a copy to the outside world.
        public string Help
        {
            get { return m_Help; }
            set { m_Help = value; }
        }
    }
}
