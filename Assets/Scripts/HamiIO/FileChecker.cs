using System.IO;

namespace HamiIO
{
    internal class FileChecker
    {
        public static bool HasJson(string folderPath, string fileName, bool shouldCreate = false)
        {
            return Has(folderPath, fileName, "json", shouldCreate);
        }

        public static bool HasXml(string folderPath, string fileName, bool shouldCreate = false)
        {
            return Has(folderPath, fileName, "xml", shouldCreate);
        }

        public static bool HasCs(string folderPath, string fileName, bool shouldCreate = false)
        {
            return Has(folderPath, fileName, "cs", shouldCreate);
        }

        private static bool Has(string folderPath, string fileName, string fileExtension, bool shouldCreate)
        {
            string basePath     = CONSTS.__FULL_PATH_TO_RESOURCES + folderPath;
            string completePath = $"{basePath}\\{fileName}.{fileExtension}";
            if (!Directory.Exists(basePath))
            {
                if (!shouldCreate) return false;
                Directory.CreateDirectory(basePath);
            }

            if (!File.Exists(completePath))
            {
                if (!shouldCreate) return false;
                File.Create(completePath).Close();
                return false;
            }

            return true;
        }
    }
}