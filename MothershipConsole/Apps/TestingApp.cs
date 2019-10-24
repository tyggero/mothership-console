using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MothershipConsole.Apps
{
    [App("test_app")]
    class TestingApp : App
    {
        protected override string ReadPrefix
        {
            get { return "[Testing]->"; }
        }

        public override bool Run()
        {
            var key = ReadKey();
            if(key.Key == ConsoleKey.Escape)
            {
                return false;
            }
            return true;
        }
    }
}
