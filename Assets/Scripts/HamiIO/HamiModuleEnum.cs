using System.IO;
using UnityEngine;

namespace HamiIO
{
    public class HamiModuleEnum
    {
        public static void Write(string folderPath, string fileName, string[] items)
        {
            string basePath     = CONSTS.__FULL_PATH_TO_RESOURCES + folderPath;
            string completePath = $"{basePath}\\E{fileName}.cs";
            if (!Directory.Exists(basePath)) Directory.CreateDirectory(basePath);
            if (!File.Exists(completePath)) File.Create(completePath).Close();
            File.WriteAllText(completePath, $"public enum E{fileName}\n{{\n\t {string.Join(",\n\t", items)} \n}}");

            MonoBehaviour.print($"Enum has been created in {completePath}");
        }

        public static bool Has(string folderName, string fileName)
        {
            return File.Exists($"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderName}\\E{fileName}.cs");
        }

	public static bool CreateEmptyIfNotExist(string folderName, string fileName)
        {
            if (Has(folderName, fileName)) return false;
            Write(folderName, fileName, new[] {""});
            return true;
        }
    }
}