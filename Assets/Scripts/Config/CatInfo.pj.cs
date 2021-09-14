using System.Collections.Generic;
using Config;
namespace CatInfo
{
    using pb = Google.Protobuf;
    public class CatInfoLoader : BaseConfigLoader<CatInfo>
    {
        public override string ConfigName => "ConfigAssets/PbJson/CatInfo.pb.json";
        private CatInfoLoader() {}
        private static readonly CatInfoLoader s_instance = new CatInfoLoader();
        public static CatInfoLoader Instance
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
        public IReadOnlyList<CatInfo.Types.CatItemConfig> CatConfig => Table.CatConfig;
        public IReadOnlyDictionary<long, CatInfo.Types.CatItemConfig> CatItemConfigDic => Table.CatItemConfigDic;
        public CatInfo.Types.CatItemConfig FindCatItemConfig(long key)
        {
            CatItemConfigDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class CatInfo : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, CatInfo.Types.CatItemConfig> CatItemConfigDic = new Dictionary<long, CatInfo.Types.CatItemConfig>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in CatConfig)
            {
                CatItemConfigDic[item.Id] = item;
            }
        }
    }
}