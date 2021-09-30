using UnityEngine;
using System;
using Logic;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Security;
using Logic.Event;
using Logic.Effect;
using Manager;
using Newtonsoft.Json;
using ResultEventInfo;
using UI.Animation;
using UnityEngine.UIElements;

namespace EventHandler
{
    public class DesignedEventHandler
    {

        private Cat catInfo = null;
        private Logic.Event.CatEvent m_catEventInfo = null;
        private Emergency emergency = null;
        private long emergencyId = 0;
        private bool emergencyResolved = false;
        private long eventID = 0;
        private long cacheTime = 0;
        private uint emergencyTime = 0;
        private bool valid = true; // 议程槽是否被封印
        private CatColumnHandler monoHandler;
        private int index = 0;

        // constructor
        public DesignedEventHandler(Cat cat)
        {
            catInfo = cat;
            valid = true;
        }

        public void SetMonoHandler(CatColumnHandler handler)
        {
            monoHandler = handler;
        }

        public void SetIndex(int ind)
        {
            index = ind;
        }

        public void SetValid(bool newValid)
        {
            valid = newValid;
        }
        
        public Logic.Event.CatEvent GetEventInfo()
        {
            return m_catEventInfo;
        }
        
        public long GetTimeRemain()
        {
            return cacheTime;
        }
        
        // 拖动事件到议程槽触发
        public void OnInit(long newEventID)
        {
            eventID = newEventID;
            m_catEventInfo = EventManager.Instance.GetCatEventByID((long)eventID);
            emergencyId = m_catEventInfo.GetEmergencyId();
            cacheTime = m_catEventInfo.ConsumeTime;
            TimeTickerManager.Instance.StopTick(); // 暂停时间
            UIManager.Instance.EventPopAnimation.GetComponent<EventPopAnimation>().Play(
                () =>
                {
                    UIManager.Instance.InitStartEventDialog(m_catEventInfo);
                });
        }

        // 点击确认开始事件触发
        public void OnPostInit(long newEventID)
        {
            
            m_catEventInfo = EventManager.Instance.GetCatEventByID((long)eventID);
            
            // 设置倒计时逻辑
            // 如果有紧急事件, 生成紧急事件
            if (emergencyId != 0)
            {
                emergency = new Emergency(m_catEventInfo.GetEmergencyId());
                emergencyId = emergency.ID;
                emergencyResolved = false;
                emergencyTime = emergency.GetTimeOffset();
            }
            m_catEventInfo.Status = EventStatus.OnProcess; 
            TimeTickerManager.Instance.AddLastingEvent(newEventID,UpdateTime, 1, 1, (int)m_catEventInfo.ConsumeTime, OnPreFinish);
            TimeTickerManager.Instance.Restore(); //恢复时间
            m_catEventInfo = EventManager.Instance.GetCatEventByID((long)eventID);
            m_catEventInfo.ExecuteEffect(); //执行事件代价
        }
        
        // 事件倒计时完成，弹出红点
        public void OnPreFinish()
        {
            UpdateTime();
            // 紧急任务默认结算
            if (emergencyId != 0 && emergencyResolved == false)
            {
                emergency.ChosseDefaultOption();
                emergencyResolved = true;
            }
            UIManager.Instance.SwitchFinishFlag(index, true);
        }

        // 点击结算红点
        public void OnFinish()
        {
            long resultId = m_catEventInfo.GetResultId();
            ResultEventInfo.ResultEventInfo.Types.ResultEventItem item = ResultEventInfoLoader.Instance.FindResultEventItem(resultId);
            TimeTickerManager.Instance.StopTick();
            // 跳出弹窗
            UIManager.Instance.EventPopAnimation.GetComponent<EventPopAnimation>().Play(
                () =>
                {
                    UIManager.Instance.InitFinishEventDialog(item);
                }
                );
        }

        //点击结算，触发结果事件效果
        public void OnPostFinish()
        {
            m_catEventInfo.Status = EventStatus.Finished;
            long resultId = m_catEventInfo.GetResultId();
            m_catEventInfo.FinishEffect(resultId);
            OnDestroyEvent();
            UIManager.Instance.SwitchFinishFlag(index, false);
        }
        
        // 取消事件进行 or 完成事件后调用
        public void OnDestroyEvent()
        {
            catInfo = null;
            m_catEventInfo = null;
            
            emergency = null;
            emergencyId = 0;
            emergencyResolved = false;
            eventID = 0;
            cacheTime = 0;
            emergencyTime = 0;
            TimeTickerManager.Instance.Restore(); //恢复时间
        }

        // 点击紧急事件红点
        public void OnEmergency()
        {
            UIManager.Instance.EventPopAnimation.GetComponent<EventPopAnimation>().Play(
                () =>
                {
                    UIManager.Instance.InitEmergencyDialog(emergency, cacheTime);
                }
            );
            TimeTickerManager.Instance.StopTick(); // 暂停时间
        }

        // 点击紧急事件结算
        public void OnPostEmergency(int choiceIndex)
        {
            emergency.Choose(choiceIndex);
            emergencyResolved = true;
            TimeTickerManager.Instance.Restore(); // 恢复时间
        }
        
        // 紧急事件红点显示
        public void UpdateEmergencyEvent()
        {
            if (emergencyId != 0 && emergencyTime > 0) // 如果有紧急事件 且 还未到触发时间
            {
                emergencyTime = emergencyTime - 1;
            }
            else if(emergencyId != 0 && emergencyTime <= 0) // 如果有紧急事件 且 到触发时间
            {

                if (emergencyResolved == false)
                {
                    UIManager.Instance.SwitchEmergencyFlag(index, true);
                }
                else
                {
                    UIManager.Instance.SwitchEmergencyFlag(index, false);
                }
            }
            else
            {
                
            }
            // 如果倒计时结束，紧急任务标隐藏
            if (cacheTime <= 0)
            {
                UIManager.Instance.SwitchEmergencyFlag(index, false);
            }
        }

        public void UpdateTime()
        {
            // 更新倒计时
            cacheTime = cacheTime - 1;
            UpdateEmergencyEvent();
        }

        public string Save()
        {
            var map = new Dictionary<string, string>();
            map["emergencyId"] = Convert.ToString(emergencyId);
            map["emergencyResolved"] = Convert.ToString(emergencyResolved);
            map["eventID"] = Convert.ToString(eventID);
            map["cacheTime"] = Convert.ToString(cacheTime);
            map["emergencyTime"] = Convert.ToString(emergencyTime);
            map["valid"] = Convert.ToString(valid);
            map["index"] = Convert.ToString(index);
            var jsonString = JsonConvert.SerializeObject(map);
            return jsonString;
        }
        
        public void Load(long emergencyIdReload, bool emergencyResolvedReload, long eventIDReload, long cacheTimeReload, uint emergencyTimeReload, bool validReload, int indexReload)
        {
            
        }
    }
}