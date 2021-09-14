using System.Collections.Generic;
using Config;
namespace BaseEffect
{
    using pb = Google.Protobuf;
    public class BaseEffectLoader : BaseConfigLoader<BaseEffect>
    {
        public override string ConfigName => "ConfigAssets/PbJson/BaseEffect.pb.json";
        private BaseEffectLoader() {}
        private static readonly BaseEffectLoader s_instance = new BaseEffectLoader();
        public static BaseEffectLoader Instance
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
        public IReadOnlyList<BaseEffect.Types.BaseEffectItem> BaseEffectConfig => Table.BaseEffectConfig;
        public IReadOnlyDictionary<long, BaseEffect.Types.BaseEffectItem> BaseEffectItemDic => Table.BaseEffectItemDic;
        public BaseEffect.Types.BaseEffectItem FindBaseEffectItem(long key)
        {
            BaseEffectItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class BaseEffect : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, BaseEffect.Types.BaseEffectItem> BaseEffectItemDic = new Dictionary<long, BaseEffect.Types.BaseEffectItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in BaseEffectConfig)
            {
                BaseEffectItemDic[item.Key] = item;
            }
        }
    }
}