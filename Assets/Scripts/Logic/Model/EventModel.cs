using System.Collections.Generic;

namespace Logic
{
    public class EventModel : BaseModel<EventModel>
    {
        //准备列表,判断是否生成
        public Dictionary<long, Event.Event> PrepareDic { get; } = new Dictionary<long, Event.Event>();
        //备选(已生成)
        public Dictionary<long, Event.Event> ActiveDic { get; } = new Dictionary<long, Event.Event>();
        //执行中
        public Dictionary<long, Event.Event> ExecuteDic { get; } = new Dictionary<long, Event.Event>();
        //已过期
        public Dictionary<long, Event.Event> ExpireDic { get; } = new Dictionary<long, Event.Event>();
        //已销毁
        public Dictionary<long, Event.Event> DestroyDic { get; } = new Dictionary<long, Event.Event>();
        //已完成
        public Dictionary<long, Event.Event> FinishDic { get; } = new Dictionary<long, Event.Event>();
    }
}