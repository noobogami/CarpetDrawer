using Newtonsoft.Json;
using UnityEngine;

namespace HamiIO
{
    public class HamiResourcesIO
    {
        public static T LoadJSON<T>(string folderPath, string fileName)
        {
            return
                JsonConvert
                    .DeserializeObject<T>(
                                          Resources
                                              .Load<TextAsset>($"Modules/{folderPath}/{fileName}JSON")
                                              .text
                                         );
        }
    }
}