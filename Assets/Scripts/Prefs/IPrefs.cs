namespace Garaj.Prefs
{
    internal interface IPrefs
    {
        void Set(string key, int value, bool save = true);
        void Set(string key, float value, bool save = true);
        void Set(string key, long value, bool save = true);
        void Set(string key, string value, bool save = true);
        void Set(string key, bool value, bool save = true);
        void Set(string key, object value, bool save = true);
        bool Has(string key);
        bool Has(string key, int value);
        bool Has(string key, long value);
        bool Has(string key, float value);
        bool Has(string key, string value);
        bool Has(string key, bool value);
        bool Has(string key, object value);
        string GetString(string key);
        int GetInt(string key);
        long GetLong(string key);
        float GetFloat(string key);
        bool GetBool(string key);
        T GetScriptableObject<T>(string key);

        void Delete(string key);
        void Save();
    }
}
