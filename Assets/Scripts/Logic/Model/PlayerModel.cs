using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class PlayerModel : BaseModel<PlayerModel>
    {
        private int m_money;
        /// <summary>
        /// 人类货币
        /// </summary>
        public int Money
        {
            get => m_money;
            set
            {
                m_money = value;
                GameObject.Find("Canvas").GetComponent<GameView>().UpdatePanelResources();
            }
        }

        private int m_influence;
        /// <summary>
        /// 猫咪影响力
        /// </summary>
        public int Influence
        {
            get => m_influence;
            set
            {
                m_influence = value;
                GameObject.Find("Canvas").GetComponent<GameView>().UpdatePanelResources();
            }
        }

        private int m_cohension;
        /// <summary>
        /// 猫咪集群
        /// </summary>
        public int Cohesion
        {
            get => m_cohension;
            set
            {
                m_cohension = value;
                GameObject.Find("Canvas").GetComponent<GameView>().UpdatePanelResources();
            }
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