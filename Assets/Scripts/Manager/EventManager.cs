using System.Collections.Generic;
using System.Linq;
using Event;
using Logic;
using Logic.Event;
using Newtonsoft.Json;

namespace Manager
{
    public class EventManager : BaseModel<EventManager>, ISaveObject
    {
        /********************************* 接口 ***********************************************/

        /*
         * 获取下方的事件列表
         */
        public List<CatEvent> GetCommonEventList()
        {
            return GetEffectiveEventList(0);
        }

        /*
         * 获取人物周围的事件列表
         */
        public List<CatEvent> GetRoleEventList(long id)
        {
            return GetEffectiveEventList(id);
        }

        public CatEvent GetCatEventByID(long id)
        {
            return id2Event[id];
        }
        
        
        /********************************* 实现 ***********************************************/

        // 
        private Dictionary<long, List<CatEvent>> eventMap;
        private Dictionary<long, CatEvent> id2Event;

        public void Init()
        {
            if (eventMap == null)
            {
                eventMap = new Dictionary<long, List<Logic.Event.CatEvent>>();
                id2Event = new Dictionary<long, CatEvent>();
                foreach (var config in EventInfoConfigLoader.Instance.EventConfig)
                {
                    AddEvent(new Logic.Event.CatEvent(config.EventId));
                }
            }
        }
        
        private void AddEvent(CatEvent e)
        {
            if (!eventMap.TryGetValue(e.HumanId, out var eventList))
            {
                eventList = new List<Logic.Event.CatEvent>();
                eventMap[e.HumanId] = eventList;
            }
            eventList.Add(e);
            id2Event[e.ID] = e;
        }

        private List<CatEvent> GetEffectiveEventList(long id)
        {
            var returnList = new List<Logic.Event.CatEvent>();
            if (!eventMap.TryGetValue(id, out var list))
            {
                return returnList;
            }
            
            foreach (var e in list)
            {
                // finished
                if (e.Status == EventStatus.Finished)
                {
                    if (e.ExecuteCount >= e.Config.RepeatTime)
                    {
                        e.Status = EventStatus.Destroyed;
                    }
                    else
                    {
                        e.Status = EventStatus.Init;
                    }
                }
                // init
                if (e.Status == EventStatus.Init)
                {
                    if (e.IsDestroyed())
                    {
                        e.Status = EventStatus.Destroyed;
                    }
                    else if (e.CanGenerate())
                    {
                        e.Generate();
                    }
                }
                // generated
                if (e.Status == EventStatus.Generated)
                {
                    if (!e.HasTicker && e.ExpireTime != 0)
                    {
                        e.AddTicker();
                    }
                    returnList.Add(e);
                }
            }
            return returnList;
        }

        public string Save()
        {
            string jsonString = JsonConvert.SerializeObject(eventMap);
            return jsonString;
        }

        public void Load(string json)
        {
            eventMap = JsonConvert.DeserializeObject<Dictionary<long, List<CatEvent>>>(json);
            id2Event = new Dictionary<long, CatEvent>();
            foreach (var list in eventMap)
            {
                foreach (var e in list.Value)
                {
                    id2Event[e.ID] = e;
                    if (e.Status == EventStatus.Generated && e.HasTicker)
                    {
                        e.AddTicker();
                    }
                }
            }
        }
    }
}