using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Security;

namespace Event
{
    public class DesignedEventHandler:IComparable<DesignedEventHandler>
    {
        public catInfo catInfo = null;
        public EventInfo eventInfo = null;
        public int cacheTime = 0;
        
        // constructor
        private DesignedEventHandler(CatInfo cat)
        {
            catInfo = cat;
        }
        
        private DesignedEventHandler(CatInfo catIns, EventInfo eventIns)
        {
            catInfo = catIns;
            eventInfo = eventIns;
        }

        public void callback()
        {
            
        }
        
        
        
    }
}