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
            var map = new Dictionary<int, (bool, int)>();
            foreach (var kv in VineManager.Vines)
            {
                map[kv.Key] = (kv.Value.Active, kv.Value.relationId);
            }
            var jsonString = JsonConvert.SerializeObject(map);
            return jsonString;
        }

        public void Load(string json)
        {
            var map = JsonConvert.DeserializeObject<Dictionary<int, (bool, int)>>(json);
            foreach (var kv in map)
            {
                TimeTickerManager.Instance.AddNowWaitingEvent(
                    -1,
                    () =>
                    {
                        return VineManager.IsInit;
                    },
                    () =>
                    {
                        var vineMono = VineManager.Vines[kv.Key];
                        if (kv.Value.Item1)
                        {
                            vineMono.Show();
                        }
                        else
                        {
                            vineMono.Hide();
                        }
                        
                        vineMono.SetText(kv.Value.Item2);
                    },
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