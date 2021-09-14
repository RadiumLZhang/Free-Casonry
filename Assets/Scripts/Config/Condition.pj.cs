using System.Collections.Generic;
using Config;
namespace Condition
{
    using pb = Google.Protobuf;
    public class ConditionLoader : BaseConfigLoader<Condition>
    {
        public override string ConfigName => "ConfigAssets/PbJson/Condition.pb.json";
        private ConditionLoader() {}
        private static readonly ConditionLoader s_instance = new ConditionLoader();
        public static ConditionLoader Instance
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
        public IReadOnlyList<Condition.Types.ConditionItem> ConditionConfig => Table.ConditionConfig;
        public IReadOnlyDictionary<long, Condition.Types.ConditionItem> ConditionItemDic => Table.ConditionItemDic;
        public Condition.Types.ConditionItem FindConditionItem(long key)
        {
            ConditionItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class Condition : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, Condition.Types.ConditionItem> ConditionItemDic = new Dictionary<long, Condition.Types.ConditionItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in ConditionConfig)
            {
                ConditionItemDic[item.ConditionId] = item;
            }
        }
    }
}