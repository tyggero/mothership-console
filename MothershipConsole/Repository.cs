using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MothershipConsole.Model;
using MothershipConsole.Apps;

namespace MothershipConsole
{
    public static class Repository
    {
        
        public static List<ListApp> InstalledApps
        {
            get
            {
                var fromFile = SaveManager.LoadFromJson<List<ListApp>>("InstalledApps.json");
                if (fromFile != null)
                {
                    return fromFile;
                }
                else
                {
                    var created = new List<ListApp>();
                    created.Add(new ListApp() { Name = "App_Store", Class = typeof(AppStoreApp)});
                    return created;
                }
            }

            set
            {
                if (value != null)
                {
                    SaveManager.SaveAsJson<List<ListApp>>(value, "InstalledApps.json");
                }
            }
        }

        public static List<Experience> Experiences
        {
            get
            {
                var fromFile = SaveManager.LoadFromJson<List<Experience>>("Experiences.json");
                if (fromFile != null)
                {
                    return fromFile;
                }
                else
                {
                    var created = new List<Experience>();
                    created.Add(new Experience() { Name = "Vládní systém pro pamětníky" });
                    created.Add(new Experience() { Name = "Trenér historických tanců" });
                    created.Add(new Experience() { Name = "Stand up komikové (tutorial)" });
                    created.Add(new Experience() { Name = "Reptiliáne nezlob se" });
                    created.Add(new Experience() { Name = "Městečko Pavel-Mop" });
                    created.Add(new Experience() { Name = "Vesmírné závory" });
                    created.Add(new Experience() { Name = "Malování 4D" });
                    created.Add(new Experience() { Name = "2D Racer" });
                    created.Add(new Experience() { Name = "Emulátor Crazy Froga" });
                    created.Add(new Experience() { Name = "Závody v usínání" });

                    created.Add(new Experience() { Name = "Analýza systémů vesmírné lodi", KeyToUnlock = "ZEVL907a" });

                    return created;
                }
            }

            set
            {
                if (value != null)
                {
                    SaveManager.SaveAsJson<List<Experience>>(value, "Experiences.json");
                }
            }
        }

        public static List<ListApp> StoreApps
        {
            get
            {
                Assembly assembly = Assembly.GetAssembly(typeof(MothershipConsole));

                var apps = new List<ListApp>();

                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(AppAttribute), true).Length > 0)
                    {
                        var listApp = new ListApp();
                        listApp.Class = type;
                        listApp.Name = ((AppAttribute)(type.GetCustomAttributes(typeof(AppAttribute), true).First())).AppName;

                        apps.Add(listApp);
                    }
                }

                return apps;
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
