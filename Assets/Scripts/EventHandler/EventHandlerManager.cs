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
                Instance = new EventManager();
            }
            return Instance;
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