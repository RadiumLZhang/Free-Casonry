using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Event;
using EventHandler;
using Logic;

namespace Manager
{


    public class EventHandlerManager : BaseModel<EventHandlerManager>
    {
        private List<DesignedEventHandler> handlerList = new List<DesignedEventHandler>();
        private List<CatColumnHandler> monoList = new List<CatColumnHandler>();

        
        public EventHandlerManager()
        {
            handlerList.Add(new DesignedEventHandler(new Cat(61001)));
            handlerList.Add(new DesignedEventHandler(new Cat(61002)));
            handlerList.Add(new DesignedEventHandler(new Cat(61003)));
            handlerList.Add(new DesignedEventHandler(new Cat(61004)));
        }
        
        public void InitMono(Transform panelEventExe)
        {
            Debug.LogError("init 叻吗");
            monoList.Add(panelEventExe.Find("EventSlot").GetComponent<CatColumnHandler>());
            monoList.Add(panelEventExe.Find("EventSlot1").GetComponent<CatColumnHandler>());
            monoList.Add(panelEventExe.Find("EventSlot2").GetComponent<CatColumnHandler>());
            monoList.Add(panelEventExe.Find("EventSlot3").GetComponent<CatColumnHandler>());
            for(int i = 0; i < 4; i++)
            {
                handlerList[i].SetMonoHandler(monoList[i]);
            }
            Debug.LogError("init 叻吗");
        }

        // 获取议程槽的数量
        public int GetHandlerCount()
        {
            return handlerList.Count;
        }

        // 获取议程槽的事件
        public DesignedEventHandler GetHandlerByIndex(int index)
        {
            //Debug.Log(ID);
            return handlerList[index];
        }

        // 获取ui的mono
        public CatColumnHandler GetMonoByIndex(int index)
        {
            return monoList[index];
        }
        
        public DesignedEventHandler GetHandlerByEventID(long newID)
        {
            foreach (var handler in handlerList)
            {
                var curEvent = handler.GetEventInfo();
                
                if (curEvent!= null && curEvent.ID == newID)
                {
                    return handler;
                }
            }
            return null;
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