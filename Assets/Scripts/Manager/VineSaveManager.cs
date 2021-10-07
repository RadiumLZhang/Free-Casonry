using System.Collections.Generic;
using Logic;
using Newtonsoft.Json;
using UnityEngine.UIElements;

namespace Manager
{
    public class VineSaveManager:BaseModel<VineSaveManager>, ISaveObject
    {
        public string Save()
        {
            var map = new Dictionary<int, bool>();
            foreach (var kv in VineManager.Vines)
            {
                map[kv.Key] = kv.Value.Active;
            }
            var jsonString = JsonConvert.SerializeObject(map);
            return jsonString;
        }

        public void Load(string json)
        {
            var map = JsonConvert.DeserializeObject<Dictionary<int, bool>>(json);
            foreach (var kv in map)
            {
                VineManager.Vines[kv.Key].Active = kv.Value;
            }
        }
    }
}