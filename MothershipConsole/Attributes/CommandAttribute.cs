using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole
{
    public class CommandAttribute : Attribute
    {
        // The constructor is called when the attribute is set.
        public CommandAttribute(string name)
        {
            m_Name = name;
        }

        // Keep a variable internally ...
        protected string m_Name;

        // .. and show a copy to the outside world.
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
    }
}
