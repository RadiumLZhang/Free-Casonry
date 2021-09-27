using System.Collections.Generic;
using Event;
using Logic;
using Logic.Event;

namespace Manager
{
    public class EventManager : BaseModel<EventManager>
    {
        /********************************* 接口 ***********************************************/

        /*
         * 获取下方的事件列表
         */
        public List<Logic.Event.CatEvent> GetCommonEventList()
        {
            return GetEffectiveEventList(0);
        }

        /*
         * 获取人物周围的事件列表
         */
        public List<Logic.Event.CatEvent> GetRoleEventList(long id)
        {
            return GetEffectiveEventList(id);
        }
        
        
        /********************************* 实现 ***********************************************/

        // 
        private Dictionary<long, List<Logic.Event.CatEvent>> eventMap = new Dictionary<long, List<Logic.Event.CatEvent>>();

        public void Init()
        {
            foreach (var config in EventInfoConfigLoader.Instance.EventConfig)
            {
                AddEvent(new Logic.Event.CatEvent(config.EventId));
            }
        }
        
        private void AddEvent(Logic.Event.CatEvent e)
        {
            if (!eventMap.TryGetValue(e.HumanId, out var eventList))
            {
                eventList = new List<Logic.Event.CatEvent>();
                eventMap[e.HumanId] = eventList;
            }
            eventList.Add(e);
        }

        private List<Logic.Event.CatEvent> GetEffectiveEventList(long id)
        {
            List<Logic.Event.CatEvent> returnList = new List<Logic.Event.CatEvent>();
            foreach (var e in eventMap[id])
            {
                if (!e.IsDestroyed() && e.CanGenerate())
                {
                    if (e.Status == EventStatus.Init)
                    {
                        e.Status = EventStatus.Generated;
                    }
                    if (e.Status == EventStatus.Generated)
                    {
                        returnList.Add(e);
                    }
                }
            }
            return returnList;
        }
        
        
    }
}