using UnityEngine;
using System;
using Logic;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Security;
using UnityEngine.UIElements;
using UI.Components;
using Event = Logic.Event.Event;

namespace EventHandler
{
    public class DesignedEventHandler
    {

        private Cat catInfo = null;
        private Logic.Event.Event eventInfo = null;
        private int eventID = 0;
        private int cacheTime = 0;

        // constructor
        private DesignedEventHandler(Cat cat)
        {
            catInfo = cat;
        }

        private DesignedEventHandler(Cat catIns, int eventID)
        {
            catInfo = catIns;
            eventInfo = new Logic.Event.Event(eventID);
        }

        public int GetTimeRemain()
        {
            return cacheTime;
        }

        private void SetEventInfo(int eventID)
        {
            eventInfo = new Logic.Event.Event(eventID);
            TimeTickerManager.AddLastingEvent(UpdateUI, 1 , 1, eventInfo.ConsumeTime, SetEffect)
        }
        public void UpdateUI()
        {
            
        }

        public void SetEffect()
        {
            
        }
    }
}