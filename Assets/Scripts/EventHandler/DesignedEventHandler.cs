using UnityEngine;
using System;
using Logic;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Security;
using Logic.Event;
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
            if (emergencyId != 0)
            {
                emergencyTime = emergencyTime - 1;
            }
            else
            {
                TimeTickerManager.Instance.StopTick();
                // todo 紧急显示接口红点
                
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
            
            // Todo: add set effet function here
            var money = PlayerModel.Instance.Money + 100;
            PlayerModel.Instance.SetResource(PlayerModel.ResourceType.Money, money);
        }
    }
}