using System.Collections.Generic;
using Config;
namespace TotalConspiracy
{
    using pb = Google.Protobuf;
    public class TotalConspiracyLoader : BaseConfigLoader<TotalConspiracy>
    {
        public override string ConfigName => "ConfigAssets/PbJson/TotalConspiracy.pb.json";
        private TotalConspiracyLoader() {}
        private static readonly TotalConspiracyLoader s_instance = new TotalConspiracyLoader();
        public static TotalConspiracyLoader Instance
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
        public IReadOnlyList<TotalConspiracy.Types.TotalConspiracyItem> ResultEventConfig => Table.ResultEventConfig;
        public IReadOnlyDictionary<long, TotalConspiracy.Types.TotalConspiracyItem> TotalConspiracyItemDic => Table.TotalConspiracyItemDic;
        public TotalConspiracy.Types.TotalConspiracyItem FindTotalConspiracyItem(long key)
        {
            TotalConspiracyItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class TotalConspiracy : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, TotalConspiracy.Types.TotalConspiracyItem> TotalConspiracyItemDic = new Dictionary<long, TotalConspiracy.Types.TotalConspiracyItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in ResultEventConfig)
            {
                TotalConspiracyItemDic[item.GroupId] = item;
            }
        }
    }
}