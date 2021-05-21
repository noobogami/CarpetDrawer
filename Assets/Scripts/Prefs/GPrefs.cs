// ReSharper disable IdentifierTypo
namespace Garaj.Prefs
{
    public class GPrefs
    {
        private static IPrefs Prefs => _prefsBase ?? (_prefsBase = new GPlayerPrefs());

        private static IPrefs _prefsBase;


        public static void Set(string key, int value, bool save = true) => Prefs.Set(key, value, save);
        public static void Set(string key, float value, bool save = true) => Prefs.Set(key, value, save);
        public static void Set(string key, long value, bool save = true) => Prefs.Set(key, value, save);
        public static void Set(string key, string value, bool save = true) => Prefs.Set(key, value, save);
        public static void Set(string key, bool value, bool save = true) => Prefs.Set(key, value, save);
        public static void Set(string key, object value, bool save = true) => Prefs.Set(key, value, save);
        public static bool Has(string key) => Prefs.Has(key);
        /// <summary>
        /// Try to get value and if doesn't exist it will create one with given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">if it doesn't exist create a key with this value</param>
        public static bool Has(string key, int value) => Prefs.Has(key, value);
        /// <summary>
        /// Try to get value and if doesn't exist it will create one with given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">if it doesn't exist create a key with this value</param>
        public static bool Has(string key, float value) => Prefs.Has(key, value);
        /// <summary>
        /// Try to get value and if doesn't exist it will create one with given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">if it doesn't exist create a key with this value</param>
        public static bool Has(string key, long value) => Prefs.Has(key, value);
        /// <summary>
        /// Try to get value and if doesn't exist it will create one with given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">if it doesn't exist create a key with this value</param>
        public static bool Has(string key, string value) => Prefs.Has(key, value);
        /// <summary>
        /// Try to get value and if doesn't exist it will create one with given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">if it doesn't exist create a key with this value</param>
        public static bool Has(string key, bool value) => Prefs.Has(key, value);
        /// <summary>
        /// Try to get value and if doesn't exist it will create one with given value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">if it doesn't exist create a key with this value</param>
        public static bool Has(string key, object value) => Prefs.Has(key, value);
        public static string GetString(string key) => Prefs.GetString(key);
        public static int GetInt(string key) => Prefs.GetInt(key);
        public static long GetLong(string key) => Prefs.GetLong(key);
        public static float GetFloat(string key) => Prefs.GetFloat(key);
        public static bool GetBool(string key) => Prefs.GetBool(key);
        public static T GetScriptableObject<T>(string key) => Prefs.GetScriptableObject<T>(key);
        public static string GetString(string key, string defaultValue)
        {
            Has(key, defaultValue);
            return GetString(key);
        }

        public static int GetInt(string key, int defaultValue)
        {
            Has(key, defaultValue);
            return GetInt(key);
        }
        public static long GetLong(string key, long defaultValue)
        {
            Has(key, defaultValue);
            return GetLong(key);
        }

        public static float GetFloat(string key, float defaultValue)
        {
            Has(key, defaultValue);
            return GetFloat(key);
        }

        public static bool GetBool(string key, bool defaultValue)
        {
            Has(key, defaultValue);
            return GetBool(key);
        }

        public static T GetScriptableObject<T>(string key, T defaultValue)
        {
            Has(key, defaultValue);
            return GetScriptableObject<T>(key);
        }

        public static void Delete(string key) => Prefs.Delete(key);

        public static void Save() => Prefs.Save();
    }
}