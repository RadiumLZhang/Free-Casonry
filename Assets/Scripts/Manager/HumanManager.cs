using System;
using System.Collections.Generic;
using HumanInfo;
using Logic;
using Logic.Human;
using Newtonsoft.Json;

namespace Manager
{
    public class HumanManager : BaseModel<HumanManager>, ISaveObject
    {
        /********************************* 接口 ***********************************************/
        
        /**
         * 根据id获取人
         */
        public Human GetHuman(long id)
        {
            return humanMap[id];
        }
        
        /************************************** 实现 ******************************************/
        
        private Dictionary<long, Human> humanMap;

        public void Init()
        {
            if (humanMap == null)
            {
                humanMap = new Dictionary<long, Human>();
                foreach (var person in HumanInfoLoader.Instance.People)
                {
                    humanMap[person.HumanId] = new Human(person.HumanId);
                }
            }
        }
        private Human LoadHuman(long id)
        {
            Human human = new Human(id);
            if (human.Config != null)
            {
                humanMap[id] = human;
                return human;
            }
            return null;
        }

        public string Save()
        {
            var jsonMap = new Dictionary<string, string>();
            jsonMap["humanMap"] = JsonConvert.SerializeObject(humanMap);
            var jsonString = JsonConvert.SerializeObject(jsonMap);
            return jsonString;
        }
        
        public void Load(string json)
        {
            var jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            humanMap = JsonConvert.DeserializeObject<Dictionary<long, Human>>(jsonMap["humanMap"]);
        }
    }
}