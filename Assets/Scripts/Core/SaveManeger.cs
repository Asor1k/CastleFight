using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace CastleFight {
    public class SaveManager
    {
        public static T Load<T>(string filename) where T : class
        {
            string path = PathForFilename(filename);
            if (FileExists(filename))
            {
                return JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            else
            { 
                return default;
            }
        }

        public static bool FileExists(string filename)
        {
            return File.Exists(PathForFilename(filename));
        }

        public static void Save<T>(string filename, T data) where T : class
        {
            string path = PathForFilename(filename);
            //Debug.Log(path);
            File.WriteAllText(path, JsonUtility.ToJson(data));
        }

        private static string PathForFilename(string filename)
        {
            string path = filename; 
#if UNITY_STANDALONE
            path = Path.Combine(Application.dataPath, filename);
#elif UNITY_IOS || UNITY_ANDROID
		    path = Path.Combine(Application.persistentDataPath, filename);
#endif
            return path;
        }
    }
}