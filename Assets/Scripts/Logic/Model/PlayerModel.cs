using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class PlayerModel : BaseModel<PlayerModel>
    {
        private int[] m_resource = new int[3];
        public enum ResourceType
        {
            Money = 0,
            Influence = 1,
            Cohesion = 2
        }
        
        public enum ResourceOperate
        {
            Minus = -1,
            Set = 0,
            Add = 1
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
        /// 猫咪集群
        /// </summary>
        public int Cohesion => m_resource[2];

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