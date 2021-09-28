using UnityEngine;
using System;
using Logic;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Security;
using Logic.Event;
using Logic.Effect;
using Manager;
using UnityEngine.UIElements;

namespace EventHandler
{
    public class DesignedEventHandler
    {

        private Cat catInfo = null;
        private Logic.Event.CatEvent m_catEventInfo = null;
        private Emergency emergency = null;
        private int emergencyId = 0;
        private bool emergencyResolved = false;
        private int eventID = 0;
        private int cacheTime = 0;
        private uint emergencyTime = 0;
        private bool valid = true;

        // constructor
        public DesignedEventHandler(Cat cat)
        {
            catInfo = cat;
            valid = true;
        }

        public void SetEventInfo(int newEventID)
        {
            eventID = newEventID;
            m_catEventInfo = new Logic.Event.CatEvent(eventID);
            emergencyId = (int)m_catEventInfo.GetEmergencyId();
            
            // 如果有紧急事件
            if (emergencyId != 0)
            {
                emergency = new Emergency(m_catEventInfo.GetEmergencyId());
                emergencyResolved = false;
                emergencyTime = emergency.GetTimeOffset();
            }
            
            cacheTime = (int)m_catEventInfo.ConsumeTime;
            TimeTickerManager.Instance.AddLastingEvent(UpdateCacheTime, 1, 1, (int)m_catEventInfo.ConsumeTime, SetEffect);
        }

        public Logic.Event.CatEvent GetEventInfo()
        {
            return m_catEventInfo;
        }
        public void UpdateCacheTime()
        {

            // 更新倒计时
            cacheTime = cacheTime - 1;
            
            
            if (emergencyId != 0 && emergencyTime != 0) // 如果有紧急事件 且 还未到触发时间
            {
                emergencyTime = emergencyTime - 1;
            }
            else if(emergencyId != 0 && emergencyTime == 0) // 如果有紧急事件 且 到触发时间
            {
                // TODO 紧急显示接口红点
                // todo 点击后暂停TimeTickerManager.Instance.StopTick();
                // todo 解决后返回
                // todo 
                
            } // 没有紧急事件
            else
            {
                // 无事发生
            }
            
        }
    
        public int GetTimeRemain()
        {
            return cacheTime;
        }

        public void SetValid(bool newValid)
        {
            valid = newValid;
        }
        
        private void SetEffect()
        {
            UpdateCacheTime();
            m_catEventInfo.Status = EventStatus.Finished;   
            m_catEventInfo.Finish(); //执行事件结算
            if (emergencyResolved == false)
            {
                emergency.ChosseDefaultOption();
            }
        }
    }
}