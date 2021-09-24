using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Event;
using Logic;
using UnityEditor.Experimental.GraphView;

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
                Instance = new EventManager();
            }
            return Instance;
        }

         public DesignedEventHandler GetHandlerByID(int index)
        {
            //Debug.Log(ID);
            return handlerList[index];
        }
         public bool BanHandler(int index)
         {
             if (handlerList.Count <= index)
                return false;
             else
             {
                 handlerList[index].SetValid(false);
                 return true;
             }
         }
}
}