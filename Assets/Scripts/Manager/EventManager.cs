using System.Collections.Generic;
using Event;
using Logic;

namespace Manager
{
    public class EventManager : BaseModel<EventManager>
    {
        /********************************* 接口 ***********************************************/

        /*
         * 获取下方的事件列表
         */
        public List<Logic.Event.Event> GetCommonEventList()
        {
            return GetEffectiveEventList(0);
        }

        /*
         * 获取人物周围的事件列表
         */
        public List<Logic.Event.Event> GetRoleEventList(long id)
        {
            return GetEffectiveEventList(id);
        }
        
        
        /********************************* 实现 ***********************************************/

        // 
        private Dictionary<long, List<Logic.Event.Event>> eventMap = new Dictionary<long, List<Logic.Event.Event>>();

        public void Init()
        {
            // TODO 把所有事件读进来
        }
        
        private void AddEvent(Logic.Event.Event e)
        {
            List<Logic.Event.Event> eventList = eventMap[e.HumanId];
            if (eventList == null)
            {
                eventList = new List<Logic.Event.Event>();
                eventMap[e.HumanId] = eventList;
            }
            eventList.Add(e);
        }

        private List<Logic.Event.Event> GetEffectiveEventList(long id)
        {
            List<Logic.Event.Event> returnList = new List<Logic.Event.Event>();
            foreach (var e in eventMap[id])
            {
                if (!e.IsDestroyed() && e.CanGenerate())
                {
                    returnList.Add(e);
                }
            }
            return returnList;
        }
        
        
    }
}