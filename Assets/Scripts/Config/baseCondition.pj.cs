using System.Collections.Generic;
using Config;
namespace BaseCondition
{
    using pb = Google.Protobuf;
    public class BaseConditionLoader : BaseConfigLoader<BaseCondition>
    {
        public override string ConfigName => "ConfigAssets/PbJson/BaseCondition.pb.json";
        private BaseConditionLoader() {}
        private static readonly BaseConditionLoader s_instance = new BaseConditionLoader();
        public static BaseConditionLoader Instance
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
        public IReadOnlyList<BaseCondition.Types.BaseConditionItem> BaseConditionConfig => Table.BaseConditionConfig;
        public IReadOnlyDictionary<string, BaseCondition.Types.BaseConditionItem> BaseConditionItemDic => Table.BaseConditionItemDic;
        public BaseCondition.Types.BaseConditionItem FindBaseConditionItem(string key)
        {
            BaseConditionItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class BaseCondition : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<string, BaseCondition.Types.BaseConditionItem> BaseConditionItemDic = new Dictionary<string, BaseCondition.Types.BaseConditionItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in BaseConditionConfig)
            {
                BaseConditionItemDic[item.Key] = item;
            }
        }
    }
}