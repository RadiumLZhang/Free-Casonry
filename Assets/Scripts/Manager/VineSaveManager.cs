using System.Collections.Generic;
using Logic;
using Newtonsoft.Json;
using UnityEngine;
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
                TimeTickerManager.Instance.AddWaitingEvent(
                    -1,
                    () =>
                    {
                        return VineManager.IsInit;
                    },
                    () =>
                    {
                        if (kv.Value)
                        {
                            VineManager.Vines[kv.Key].Show();
                        }
                        else
                        {
                            VineManager.Vines[kv.Key].Hide();
                        }
                    },
                    0,
                    10,
                    () =>
                    {
                        Debug.LogError("VineSaveManager:Load False!");
                    }
                );
            }
        }
    }
}