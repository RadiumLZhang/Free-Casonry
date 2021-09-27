using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Event;
using EventHandler;
using Logic;

namespace Manager
{


    public class EventHandlerManager : BaseModel<EventHandlerManager>
    {
        private List<DesignedEventHandler> handlerList = new List<DesignedEventHandler>();

        public EventHandlerManager()
        {
            handlerList.Add(new DesignedEventHandler(new Cat(61001)));
            handlerList.Add(new DesignedEventHandler(new Cat(61002)));
            handlerList.Add(new DesignedEventHandler(new Cat(61003)));
            handlerList.Add(new DesignedEventHandler(new Cat(61004)));
        }

        // 获取议程槽的数量
        public int GetHandlerCount()
        {
            return handlerList.Count;
        }

        // 获取议程槽的事件
        public DesignedEventHandler GetHandlerByID(int index)
        {
            //Debug.Log(ID);
            return handlerList[index];
        }

        public bool AddNewHandler(Cat catInfo)
        {
            if (handlerList.Count >= 4)
                return false;
            else
            {
                handlerList.Add(new DesignedEventHandler(catInfo));
                return true;
            }
        }

        public bool DisableHandler(int index)
        {
            if (handlerList.Count <= index)
            {
                return false;
            }

            handlerList[index].SetValid(true);
            return true;
        }

        public bool EnableHandler(int index)
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