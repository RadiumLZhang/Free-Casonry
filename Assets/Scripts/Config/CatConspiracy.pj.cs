using System.Collections.Generic;
using Config;
namespace CatConspiracyInfo
{
    using pb = Google.Protobuf;
    public class CatConspiracyInfoLoader : BaseConfigLoader<CatConspiracyInfo>
    {
        public override string ConfigName => "ConfigAssets/PbJson/CatConspiracyInfo.pb.json";
        private CatConspiracyInfoLoader() {}
        private static readonly CatConspiracyInfoLoader s_instance = new CatConspiracyInfoLoader();
        public static CatConspiracyInfoLoader Instance
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
        public IReadOnlyList<CatConspiracyInfo.Types.CatConspiracyItem> ResultEventConfig => Table.ResultEventConfig;
        public IReadOnlyDictionary<long, CatConspiracyInfo.Types.CatConspiracyItem> CatConspiracyItemDic => Table.CatConspiracyItemDic;
        public CatConspiracyInfo.Types.CatConspiracyItem FindCatConspiracyItem(long key)
        {
            CatConspiracyItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class CatConspiracyInfo : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, CatConspiracyInfo.Types.CatConspiracyItem> CatConspiracyItemDic = new Dictionary<long, CatConspiracyInfo.Types.CatConspiracyItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in ResultEventConfig)
            {
                CatConspiracyItemDic[item.ConspiracyId] = item;
            }
        }
    }
}