using System;
using System.Collections.Generic;
using Manager;
using Newtonsoft.Json;
using UnityEngine;

namespace Logic
{
    public enum Operate
    {
        Minus = -1,
        Set = 0,
        Add = 1
    }
    
    public class PlayerModel : BaseModel<PlayerModel>, ISaveObject
    {
        private int[] m_resource = new int[5]{0, 10, 50, 0, 0};
        public enum ResourceType
        {
            Money = 0,
            Influence = 1,
            Hidency = 2,
            ArmyDifference = 3,
            DiseaseSurvey = 4
        }

        public bool NeedUpdate { get; set; } = true;

        /// <summary>
        /// 人类货币
        /// </summary>
        public int Money => m_resource[0];

        /// <summary>
        /// 猫咪影响力
        /// </summary>
        public int Influence => m_resource[1];

        /// <summary>
        /// 隐匿度
        /// </summary>
        public int Hidency => m_resource[2];

        /// <summary>
        /// 军队实力差
        /// </summary>
        public int ArmyDifference => m_resource[3];

        /// <summary>
        /// 传染病调查进度
        /// </summary>
        public int DiseaseSurvey => m_resource[4];


        public int StoryProgress { get; set; }

        private HashSet<long> m_records = new HashSet<long>();

        public void SetResource(ResourceType type, int value)
        {
            //资源类型无负值
            m_resource[(int) type] = Math.Max(0, value);
            m_resource[(int) type] = value;
            NeedUpdate = true;
        }

        public int GetResource(ResourceType type)
        {
            return m_resource[(int) type];
        }

        public class EventResult
        {
            public long eventId;
            public long result;
        }

        public void AddRecord(long id)
        {
            m_records.Add(id);
        }

        public void RemoveRecord(long id)
        {
            m_records.Remove(id);
        }

        public bool CheckRecord(long id)
        {
            return m_records.Contains(id);
        }

        public string Save()
        {
            var jsonMap = new Dictionary<string, string>();
            jsonMap["m_resource"] = JsonConvert.SerializeObject(m_resource);
            jsonMap["StoryProgress"] = Convert.ToString(StoryProgress);
            jsonMap["m_records"] = JsonConvert.SerializeObject(m_records);
            var jsonString = JsonConvert.SerializeObject(jsonMap);
            return jsonString;
        }

        public void Load(string json)
        {
            var jsonMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            m_resource = JsonConvert.DeserializeObject<int[]>(jsonMap["m_resource"]);
            StoryProgress = int.Parse(jsonMap["StoryProgress"]);
            m_records = JsonConvert.DeserializeObject<HashSet<long>>(jsonMap["m_records"]);
            GameObject.Find("Canvas").GetComponent<GameView>().UpdatePanelResources();
        }
    }
}