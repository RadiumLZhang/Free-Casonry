using System.Collections.Generic;
using Config;
namespace Event
{
    using pb = Google.Protobuf;
    public class EventInfoConfigLoader : BaseConfigLoader<EventInfoConfig>
    {
        public override string ConfigName => "ConfigAssets/PbJson/EventInfoConfig.pb.json";
        private EventInfoConfigLoader() {}
        private static readonly EventInfoConfigLoader s_instance = new EventInfoConfigLoader();
        public static EventInfoConfigLoader Instance
        {
            get
            {
                if (s_instance.Table == null)
                {
                    s_instance.Load();
                }
                return s_instance;
            }
        }
        public IReadOnlyList<EventInfoConfig.Types.EventItemConfig> EventConfig => Table.EventConfig;
        public IReadOnlyDictionary<long, EventInfoConfig.Types.EventItemConfig> EventItemConfigDic => Table.EventItemConfigDic;
        public EventInfoConfig.Types.EventItemConfig FindEventItemConfig(long key)
        {
            EventItemConfigDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class EventInfoConfig : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, EventInfoConfig.Types.EventItemConfig> EventItemConfigDic = new Dictionary<long, EventInfoConfig.Types.EventItemConfig>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in EventConfig)
            {
                EventItemConfigDic[item.EventId] = item;
            }
        }
    }
}