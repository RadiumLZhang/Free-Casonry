using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Event;
using EventHandler;
using Logic;
using Newtonsoft.Json;

namespace Manager
{


    public class EventHandlerManager : BaseModel<EventHandlerManager>, ISaveObject
    {
        private List<DesignedEventHandler> handlerList = new List<DesignedEventHandler>();
        private List<CatColumnHandler> monoList = new List<CatColumnHandler>();

        public int CurSelectIndex { get; set; }
        
        public EventHandlerManager()
        {
            
            handlerList.Add(new DesignedEventHandler(CatManager.Instance.GetCat(61001)));
            handlerList.Add(new DesignedEventHandler(CatManager.Instance.GetCat(61002)));
            handlerList.Add(new DesignedEventHandler(CatManager.Instance.GetCat(61003)));
            handlerList.Add(new DesignedEventHandler(CatManager.Instance.GetCat(61004)));
        }
        
        public void InitMono(Transform panelEventExe)
        {
            if (monoList.Count == 4)
            {
                return;
            }
            monoList.Add(panelEventExe.Find("EventSlot0").GetComponent<CatColumnHandler>());
            monoList.Add(panelEventExe.Find("EventSlot1").GetComponent<CatColumnHandler>());
            monoList.Add(panelEventExe.Find("EventSlot2").GetComponent<CatColumnHandler>());
            monoList.Add(panelEventExe.Find("EventSlot3").GetComponent<CatColumnHandler>());
            for(int i = 0; i < 4; i++)
            {
                handlerList[i].SetMonoHandler(monoList[i]);
                handlerList[i].SetIndex(i);
            }
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

        public Cat GetCatByIndex(int index)
        {
            return handlerList[index].GetCat();
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

        public string Save()
        {
            var jsonList = new Dictionary<int, string>();
            for (int i = 0; i < 4; i++)
            {
                jsonList[i] = handlerList[i].Save();
            }
            var jsonString = JsonConvert.SerializeObject(jsonList);
            return jsonString;
        }

        public void Load(string json)
        {
            var jsonList = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);
            for (int i = 0; i < 4; i++)
            {
                var map = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonList[i]);
                var mono = monoList[i];
                var designedEventHandler = handlerList[i];
                TimeTickerManager.Instance.AddNowWaitingEvent(
                    -1,
                    () =>
                    {
                        return mono.isInit;
                    },
                    () =>
                    {
                        designedEventHandler.Load(
                        long.Parse(map["emergencyId"]),
                        bool.Parse(map["emergencyResolved"]),
                        long.Parse(map["eventID"]),
                        long.Parse(map["cacheTime"]),
                        uint.Parse(map["emergencyTime"]),
                        bool.Parse(map["valid"]),
                        int.Parse(map["index"])
                        );
                    },
                    10,
                    () =>
                    {
                        Debug.LogError("Event Handler Manager:Load False!");
                    }
                );
                
                
                
            }
        }

        public void RefreshColumnImage()
        {
            for (int i = 0; i < handlerList.Count; i++)
            {
                monoList[i].HighLight(!handlerList[i].HasEvent());
            }
        }

        public void ResetColumnImage()
        {
            for (int i = 0; i < handlerList.Count; i++)
            {
                monoList[i].HighLight(false);
            }
        }
    }
}