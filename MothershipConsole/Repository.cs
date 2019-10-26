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

        public static List<CrewMember> ExpectedCrewMembers
        {
            get
            {
                var fromFile = SaveManager.LoadFromJson<List<CrewMember>>("ExpectedCrewMembers.json");
                if (fromFile != null)
                {
                    return fromFile;
                }
                else
                {
                    var created = new List<CrewMember>();
                    return created;
                }
            }

            set
            {
                if (value != null)
                {
                    SaveManager.SaveAsJson<List<CrewMember>>(value, "ExpectedCrewMembers.json");
                }
            }
        }

        public static List<CrewMember> CrewMembers
        {
            get
            {
                var fromFile = SaveManager.LoadFromJson<List<CrewMember>>("CrewMembers.json");
                if (fromFile != null)
                {
                    return fromFile;
                }
                else
                {
                    var created = new List<CrewMember>();
                    return created;
                }
            }

            set
            {
                if (value != null)
                {
                    SaveManager.SaveAsJson<List<CrewMember>>(value, "CrewMembers.json");
                }
            }
        }
    }
}
