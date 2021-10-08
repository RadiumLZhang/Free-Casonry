using System;
using System.Collections.Generic;
using HumanInfo;
using Logic;
using Logic.Human;
using Newtonsoft.Json;
using UnityEngine;

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
            var jsonString = JsonConvert.SerializeObject(humanMap);
            return jsonString;
        }
        
        public void Load(string json)
        {
            humanMap = JsonConvert.DeserializeObject<Dictionary<long, Human>>(json);
            TimeTickerManager.Instance.AddNowWaitingEvent(
                -1,
                () =>
                {
                    return NPCManager.IsInit;
                },
                () =>
                {
                    foreach (var kv in humanMap)
                    {
                        NPCManager.NPCs[kv.Key].gameObject.SetActive(kv.Value.IsShow);
                        if (!kv.Value.IsAlive)
                        {
                            kv.Value.Death();
                        }
                    }
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