using Dnata.Automation.BDDFramework.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dnata.Automation.BDDFramework.Helpers
{
   static public class JsonHelper
    {

        public static string ReadFromFile(string fileName)
        {
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                return streamReader.ReadToEnd();
            }
        }

        public static T ReadFromJson <T> (string fileName)
        {
            fileName = Directory.GetCurrentDirectory() + "\\" + fileName + ".json";
            return JsonConvert.DeserializeObject<T>(ReadFromFile(fileName));
        }

        public static T ReadFromJson<T>(string path, string filename, string extn = "json")
        {
            filename = path + "\\" + filename + "." + extn;
            return JsonConvert.DeserializeObject<T>(ReadFromFile(filename));
        }

        public static string ConvertJsonToString (Object JsonObject)
        {
            return JsonConvert.SerializeObject(JsonObject);
        }

        public static void WriteToFile<T>(T JsonObject, string fileName)
        {
            string json = JsonConvert.SerializeObject(JsonObject, Formatting.Indented);
            var filepath = Directory.GetCurrentDirectory() + fileName + DateTime.Now.ToString("hhmmss") + ".json";
            File.WriteAllText(filepath, json);
        }

        public static void WriteToFileInDirectory<T>(T JsonObject,string outputDir, string fileName)
        {
            string json = JsonConvert.SerializeObject(JsonObject, Formatting.Indented);
            Directory.CreateDirectory(outputDir);
            var filepath = Directory.GetCurrentDirectory() + "//" + outputDir + "//" + fileName + DateTime.Now.ToString("hhmmss") + ".json";
            File.WriteAllText(filepath, json);
            
        }

    }
}
