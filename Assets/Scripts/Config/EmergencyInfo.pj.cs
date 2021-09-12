using System.Collections.Generic;
using Config;
namespace EmergencyInfo
{
    using pb = Google.Protobuf;
    public class EmergencyInfoConfigLoader : BaseConfigLoader<EmergencyInfoConfig>
    {
        public override string ConfigName => "ConfigAssets/PbJson/TriggerEvent.pb.json";
        private EmergencyInfoConfigLoader() {}
        private static readonly EmergencyInfoConfigLoader s_instance = new EmergencyInfoConfigLoader();
        public static EmergencyInfoConfigLoader Instance
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
        public IReadOnlyList<EmergencyInfoConfig.Types.EmergencyItem> EmergencyItemConfig => Table.EmergencyItemConfig;
        public IReadOnlyDictionary<long, EmergencyInfoConfig.Types.EmergencyItem> EmergencyItemDic => Table.EmergencyItemDic;
        public EmergencyInfoConfig.Types.EmergencyItem FindEmergencyItem(long key)
        {
            EmergencyItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class EmergencyInfoConfig : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, EmergencyInfoConfig.Types.EmergencyItem> EmergencyItemDic = new Dictionary<long, EmergencyInfoConfig.Types.EmergencyItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in EmergencyItemConfig)
            {
                EmergencyItemDic[item.EmergencyId] = item;
            }
        }
    }
}