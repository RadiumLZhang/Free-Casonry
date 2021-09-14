using System.Collections.Generic;
using Config;
namespace BuffInfo
{
    using pb = Google.Protobuf;
    public class BuffInfoLoader : BaseConfigLoader<BuffInfo>
    {
        public override string ConfigName => "ConfigAssets/PbJson/BuffInfo.pb.json";
        private BuffInfoLoader() {}
        private static readonly BuffInfoLoader s_instance = new BuffInfoLoader();
        public static BuffInfoLoader Instance
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
        public IReadOnlyList<BuffInfo.Types.BuffItem> BuffConfigs => Table.BuffConfigs;
        public IReadOnlyDictionary<long, BuffInfo.Types.BuffItem> BuffItemDic => Table.BuffItemDic;
        public BuffInfo.Types.BuffItem FindBuffItem(long key)
        {
            BuffItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class BuffInfo : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, BuffInfo.Types.BuffItem> BuffItemDic = new Dictionary<long, BuffInfo.Types.BuffItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in BuffConfigs)
            {
                BuffItemDic[item.BuffId] = item;
            }
        }
    }
}