using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public enum Operate
    {
        Minus = -1,
        Set = 0,
        Add = 1
    }
    
    public class PlayerModel : BaseModel<PlayerModel>
    {
        private int[] m_resource = new int[3];
        public enum ResourceType
        {
            Money = 0,
            Influence = 1,
            Cohesion = 2,
            ArmyDifference = 3,
            DiseaseSurvey = 4
        }

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

        public void SetResource(ResourceType type, int value)
        {
            m_resource[(int) type] = value;
            GameObject.Find("Canvas").GetComponent<GameView>().UpdatePanelResources();
        }

        public int GetResource(ResourceType type)
        {
            return m_resource[(int) type];
        }

        /// <summary>
        /// 已完成的事件
        /// </summary>
        public readonly List<EventResult> EventResultList;
        
        public class EventResult
        {
            public long eventId;
            public long result;
        }
        
        
    }
}