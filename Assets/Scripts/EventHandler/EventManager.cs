using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Event;
using Logic;

namespace EventHandler
{
    
    
public class EventManager
{
        private List<DesignedEventHandler> handlerList = new List<DesignedEventHandler>();
        public static EventManager Instance;

        private EventManager()
        {
            handlerList.Add(new DesignedEventHandler(new Cat(1001)));
            handlerList.Add(new DesignedEventHandler(new Cat(1001)));
            handlerList.Add(new DesignedEventHandler(new Cat(1001)));
            handlerList.Add(new DesignedEventHandler(new Cat(1001)));
        }

        public static EventManager GetInstance()
        {
            //如果进行调用时instance为null则进行初始化
            if (Instance == null) 
            {
                TimeTickerManager.Awake();
                Instance = new EventManager();
            }
            return Instance;
        }

         public DesignedEventHandler GetHandlerByID(int id)
        {
            //Debug.Log(ID);
            return handlerList[id];
        }
    }
}