using System;
using System.Collections.Generic;
using Logic;
using Newtonsoft.Json;

namespace Manager
{
    /*
     * 使用存档的对象需要实现以下接口，然后在SaveManager的Init当中注册
     */
    public interface ISaveObject
    {
        public string Save();

        public void Load(string json);
    }
    
    public class SaveManager : BaseModel<SaveManager>
    {
        private Dictionary<string, ISaveObject> saveMap;
        
        public void Init()
        {
            saveMap = new Dictionary<string, ISaveObject>();

            saveMap["EventManager"] = EventManager.Instance;
        }

        public string SaveData(string name)
        {
            var map = new Dictionary<string, string>();
            foreach (var kv in saveMap)
            {
                map[kv.Key] = kv.Value.Save();
            }
            string jsonString = JsonConvert.SerializeObject(map);
            return jsonString;
        }

        public void LoadData(string name, string json)
        {
            var map = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            foreach (var kv in map)
            {
                saveMap[kv.Key].Load(kv.Value);
            }
        }
    }
}