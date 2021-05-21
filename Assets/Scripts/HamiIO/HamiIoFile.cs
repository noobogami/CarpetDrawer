using System.IO;
using System.Linq;
using UnityEngine;

namespace HamiIO
{
    public class HamiIoFile
    {
        public static void DeleteAllFiles(string folder)
        {
            if (!Directory.Exists(CONSTS.__FULL_PATH_TO_RESOURCES + folder)) return;
            DirectoryInfo di = new DirectoryInfo(CONSTS.__FULL_PATH_TO_RESOURCES + folder);
            foreach (FileInfo file in di.GetFiles()) file.Delete();
        }


        public static void DeleteAllFilesExcept(string folder, string[] exceptions)
        {
            if (!Directory.Exists(CONSTS.__FULL_PATH_TO_RESOURCES + folder)) return;
            DirectoryInfo di = new DirectoryInfo(CONSTS.__FULL_PATH_TO_RESOURCES + folder);
            foreach (FileInfo file in di.GetFiles())
            {
                bool existBefore = false;
                foreach (string exception in exceptions)
                    if (file.Name.Contains(exception))
                    {
                        existBefore = true;
                    }

                if (!existBefore)
                {
                    file.Delete();
                    MonoBehaviour.print($"{file.FullName} has been deleted");
                }
            }
        }
    }
}