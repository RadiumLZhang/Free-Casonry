using UnityEngine;
using System;
using Logic;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Security;
using Manager;
using UnityEngine.UIElements;
using Event = Logic.Event.Event;

namespace EventHandler
{
    public class DesignedEventHandler
    {

        private Cat catInfo = null;
        private Logic.Event.Event eventInfo = null;
        private int eventID = 0;
        private int cacheTime = 0;
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
            eventInfo = new Logic.Event.Event(eventID);
            cacheTime = (int)eventInfo.ConsumeTime;
            TimeTickerManager.Instance.AddLastingEvent(UpdateCacheTime, 1, 1, (int)eventInfo.ConsumeTime, SetEffect);
        }

        public Logic.Event.Event GetEventInfo()
        {
            return eventInfo;
        }
        public void UpdateCacheTime()
        {
            cacheTime = cacheTime - 1;
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
            var money = PlayerModel.Instance.Money + 100;
            PlayerModel.Instance.SetResource(PlayerModel.ResourceType.Money, money);
        }
    }
}