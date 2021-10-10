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

            //直接将国王设置成显示
            humanMap[60000].IsShow = true;
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

        public void ShowAll()
        {
            foreach (var item in humanMap)
            {
                var human = item.Value;
                human.Show();
            }
        }

        public string Save()
        {
            var savDic = new Dictionary<long, string>();
            foreach (var item in humanMap)
            {
                var human = item.Value;
                savDic[item.Key] = JsonConvert.SerializeObject(human);
            }
            
            var jsonString = JsonConvert.SerializeObject(savDic);
            return jsonString;
        }
        
        public void Load(string json)
        {
            var savDic = JsonConvert.DeserializeObject<Dictionary<long, string>>(json);
            humanMap = new Dictionary<long, Human>();
            foreach (var kv in savDic)
            {
                var human = JsonConvert.DeserializeObject<Human>(kv.Value);
                human.Restore();
                humanMap[kv.Key] = human;
            }

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
                        var human = kv.Value;

                        if (human.IsShow)
                        {
                            human.Show();
                        }

                        if (!human.IsAlive)
                        {
                            human.Death();
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