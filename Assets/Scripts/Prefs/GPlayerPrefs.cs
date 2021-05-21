using UnityEngine;

namespace Garaj.Prefs
{
    internal class GPlayerPrefs : IPrefs
    {
        public void Set(string key, int value, bool save = true)
        {
            PlayerPrefs.SetInt(key, value);
            if(save)
                Save();
        }
        
        public void Set(string key, float value, bool save = true)
        {
            PlayerPrefs.SetFloat(key, value);
            if(save)
                Save();
        }

        public void Set(string key, long value, bool save = true)
        {
            PlayerPrefs.SetString(key, value.ToString());
            if(save)
                Save();
        }

        public void Set(string key, string value, bool save = true)
        {
            PlayerPrefs.SetString(key, value);
            if(save)
                Save();
        }

        public void Set(string key, bool value, bool save = true)
        {
            PlayerPrefs.SetInt(key, value? 1 : 0);
            if(save)
                Save();
        }

        public void Set(string key, object value, bool save = true)
        {
            var json = JsonUtility.ToJson(value, true);
            //Debug.Log("serialized json:\n" + json);
            Set(key, json);
        }

        public bool Has(string key) => PlayerPrefs.HasKey(key);
        public bool Has(string key, int value)
        {
            if (Has(key)) return true;
            Set(key, value);
            return false;
        }
        public bool Has(string key, long value)
        {
            if (Has(key)) return true;
            Set(key, value);
            return false;
        }

        public bool Has(string key, float value)
        {
            if (Has(key)) return true;
            Set(key, value);
            return false;
        }

        public bool Has(string key, string value)
        {
            if (Has(key)) return true;
            Set(key, value);
            return false;
        }

        public bool Has(string key, bool value)
        {
            if (Has(key)) return true;
            Set(key, value);
            return false;
        }

        public bool Has(string key, object value)
        {
            if (Has(key)) return true;
            Set(key, value);
            return false;
        }

        public string GetString(string key) => PlayerPrefs.GetString(key);

        public int GetInt(string key) => PlayerPrefs.GetInt(key);
        public long GetLong(string key) => long.Parse(GetString(key));

        public float GetFloat(string key) => PlayerPrefs.GetFloat(key);

        public bool GetBool(string key) => PlayerPrefs.GetInt(key) == 1;

        public T GetScriptableObject<T>(string key)
        {
            var json = GetString(key);
            //Debug.Log("deserializing:\n" + json);
            return JsonUtility.FromJson<T>(json);
        }

        public void Delete(string key) => PlayerPrefs.DeleteKey(key);

        public void Save() => PlayerPrefs.Save();
    }
}