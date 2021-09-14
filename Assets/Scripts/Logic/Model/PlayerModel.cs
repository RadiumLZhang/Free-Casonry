using System;
using System.Collections.Generic;

namespace Logic
{
    public class PlayerModel : BaseModel<PlayerModel>
    {
        /// <summary>
        /// 人类货币
        /// </summary>
        public int money;
        
        /// <summary>
        /// 猫咪影响力
        /// </summary>
        public int influence;
        /// <summary>
        /// 猫咪集群
        /// </summary>
        public int cohesion;

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