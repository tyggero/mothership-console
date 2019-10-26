using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MothershipConsole.Model;

namespace MothershipConsole
{
    public static class Repository
    {
        public static List<StoreApp> StoreApps
        {
            get 
            {
                var fromFile = SaveManager.LoadFromJson<List<StoreApp>>("StoreApps.json");
                if (fromFile != null)
                {
                    return fromFile;
                }
                else
                { 
                    var created = new List<StoreApp>();
                    return created;
                }
            }

            set
            {
                if (value != null)
                {
                    SaveManager.SaveAsJson<List<StoreApp>>(value, "StoreApps.json");
                }
            }
        }
    }
}
