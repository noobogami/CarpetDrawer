using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace HamiIO
{
    public class HamiModuleXmlIO
    {
        public static T Read<T>(string folderName, string fileName) where T : class
        {
            if (!FileChecker.HasXml(folderName, $"{fileName}XML")) return null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            return xmlSerializer.Deserialize(
                                             new StringReader(
                                                              File.ReadAllText($"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderName}\\{fileName}XML.xml")))
                as T;
        }

        public static void Write<T>(string folderName, string fileName, T toSerialize)
        {
            FileChecker.HasXml(folderName, fileName + "XML", true);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            var           write         = new StreamWriter($"{CONSTS.__FULL_PATH_TO_RESOURCES}{folderName}\\{fileName}XML.xml");
            xmlSerializer.Serialize(write.BaseStream, toSerialize);
            write.Close();

            MonoBehaviour.print(
                                $"Xml successfully has been written in {CONSTS.__FULL_PATH_TO_RESOURCES}{folderName}\\{fileName}XML.xml");
        }
    }
}