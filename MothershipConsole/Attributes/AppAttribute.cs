using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole
{
    public class AppAttribute : Attribute
    {
        // The constructor is called when the attribute is set.
        public AppAttribute(string appName)
        {
            m_AppName = appName;
        }

        // Keep a variable internally ...
        protected string m_AppName;

        // .. and show a copy to the outside world.
        public string AppName
        {
            get { return m_AppName; }
            set { m_AppName = value; }
        }
    }
}
