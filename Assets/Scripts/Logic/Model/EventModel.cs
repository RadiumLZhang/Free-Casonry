using System.Collections.Generic;

namespace Logic
{
    public class EventModel : BaseModel<EventModel>
    {
        //准备列表,判断是否生成
        public Dictionary<long, Event.CatEvent> PrepareDic { get; } = new Dictionary<long, Event.CatEvent>();
        //备选(已生成)
        public Dictionary<long, Event.CatEvent> ActiveDic { get; } = new Dictionary<long, Event.CatEvent>();
        //执行中
        public Dictionary<long, Event.CatEvent> ExecuteDic { get; } = new Dictionary<long, Event.CatEvent>();
        //已过期
        public Dictionary<long, Event.CatEvent> ExpireDic { get; } = new Dictionary<long, Event.CatEvent>();
        //已销毁
        public Dictionary<long, Event.CatEvent> DestroyDic { get; } = new Dictionary<long, Event.CatEvent>();
        //已完成
        public Dictionary<long, Event.CatEvent> FinishDic { get; } = new Dictionary<long, Event.CatEvent>();
    }
}