using System.Collections.Generic;
using Logic;
using Logic.Event;
using Newtonsoft.Json;

namespace Manager
{
    public class EmergencyManager : BaseModel<EmergencyManager>, ISaveObject
    {
        /********************************* 接口 ***********************************************/

        public Emergency GetEmergencyByID(long id)
        {
            if (!map.TryGetValue(id, out var emergency))
            {
                emergency = new Emergency(id);
                map[id] = emergency;
            }

            return emergency;
        }

        /********************************* 实现 ***********************************************/

        private Dictionary<long, Emergency> map = new Dictionary<long, Emergency>();

        public string Save()
        {
            var jsonMap = new Dictionary<long, int>();
            foreach (var kv in map)
            {
                jsonMap[kv.Key] = kv.Value.GetChoice();
            }
            var jsonString = JsonConvert.SerializeObject(jsonMap);
            return jsonString;
        }

        public void Load(string json)
        {
            var jsonMap = JsonConvert.DeserializeObject<Dictionary<long, int>>(json);
            foreach (var kv in jsonMap)
            {
                GetEmergencyByID(kv.Key).SetChoice(kv.Value);
            }
        }
    }
}