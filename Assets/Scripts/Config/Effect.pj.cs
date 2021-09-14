using System.Collections.Generic;
using Config;
namespace Effect
{
    using pb = Google.Protobuf;
    public class EffectLoader : BaseConfigLoader<Effect>
    {
        public override string ConfigName => "ConfigAssets/PbJson/Effect.pb.json";
        private EffectLoader() {}
        private static readonly EffectLoader s_instance = new EffectLoader();
        public static EffectLoader Instance
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
        public IReadOnlyList<Effect.Types.EffectItem> EffectConfig => Table.EffectConfig;
        public IReadOnlyDictionary<long, Effect.Types.EffectItem> EffectItemDic => Table.EffectItemDic;
        public Effect.Types.EffectItem FindEffectItem(long key)
        {
            EffectItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class Effect : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, Effect.Types.EffectItem> EffectItemDic = new Dictionary<long, Effect.Types.EffectItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in EffectConfig)
            {
                EffectItemDic[item.EffectId] = item;
            }
        }
    }
}