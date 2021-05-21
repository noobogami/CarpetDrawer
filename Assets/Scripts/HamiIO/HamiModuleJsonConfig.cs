using System.IO;
using System.Xml;
using Newtonsoft.Json;
using UnityEngine;
using Formatting = Newtonsoft.Json.Formatting;

namespace HamiIO
{
    public class HamiModuleJsonConfig
    {
        public static void Write<T>(string folderPath, string fileName, T obj)
        {
            FileChecker.HasJson(folderPath, $"{fileName}JSON", true);
            File.WriteAllText($@"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\{fileName}JSON.json",
                JsonConvert.SerializeObject(obj, Formatting.Indented)
            );
            MonoBehaviour.print(
                $@"Json has been created in {CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\{fileName}JSON");
        }

        public static string Read(string folderPath, string fileName)
        {
            if (!FileChecker.HasJson(folderPath, $"{fileName}JSON")) return null;
            return File.ReadAllText($@"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\{fileName}JSON.json");
        }

        public static T Read<T>(string folderPath, string fileName) where T : class
        {
            if (!FileChecker.HasJson(folderPath, $"{fileName}JSON")) return null;
            return JsonConvert.DeserializeObject<T>(
                File.ReadAllText($@"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderPath}\{fileName}JSON.json"));
        }

        public static bool Has(string folderPath, string fileName) =>
            FileChecker.HasJson(folderPath, $"{fileName}JSON");
    }
}