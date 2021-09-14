using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Event;
using Logic;

namespace EventHandler{
    public class EventManager
    {

        private static EventManager instance = null;
        List<DesignedEventHandler> EventHandlerList; //控制长度为4
        List<Cat> CatList;
        
        // constructor
        private EventManager()
        {
            CatList.Add(1, new Cat(1));
            
        }

        public static void Start()
        {
            
        }
        
        //accessor
        public static EventManager GetEventManager()
        {
            if (instance == null)
            {
                instance = new EventManager();
            }

            return instance;
        }

        private void Start(){

        }
        
        // 猫咪作为handler的一个属性
        public int InitalizeNewEvent(Logic.Event.Event event, Cat cat)
        {
            
            EventHandlerList.Add(new EventHandler(cat, event));
        }
        

        public int GetHandlerID(CatInfo cat){
            
        }

        public int RefreshUIPerSecond()
        {
                        
        }
}