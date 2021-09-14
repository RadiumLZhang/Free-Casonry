using UnityEngine;
using System;
using Logic;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Security;
using UnityEngine.UIElements;

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

        private DesignedEventHandler(Cat catIns, Logic.Event.Event eventIns)
        {
            catInfo = catIns;
            eventInfo = eventIns;
        }
        public void SetEventID(int newEventID)
        {
            eventID = newEventID;
        }
        public void callback()
        {

        }
    }
}