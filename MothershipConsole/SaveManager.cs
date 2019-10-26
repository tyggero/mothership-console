using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MothershipConsole
{
    public static class SaveManager
    {
        public static string folderPath = "Data";

        public static bool SaveAsJson<T>(T dataObject, string fileName)
        {
            var serialized = JsonConvert.SerializeObject(dataObject, Formatting.Indented);

            System.IO.Directory.CreateDirectory(folderPath);
            System.IO.File.WriteAllText(folderPath + @"\" + fileName, serialized);

            return true;
        }

        public static T LoadFromJson<T>(string fileName)
        {
            try
            {
                var serialized = System.IO.File.ReadAllText(folderPath + @"\" + fileName);

                T dataObject = JsonConvert.DeserializeObject<T>(serialized);

                return dataObject;
            }
            catch (System.IO.FileNotFoundException)
            {
                return default(T);
            }
        }
    }
}
