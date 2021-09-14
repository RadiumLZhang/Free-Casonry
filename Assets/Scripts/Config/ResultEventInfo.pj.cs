using System.Collections.Generic;
using Config;
namespace ResultEventInfo
{
    using pb = Google.Protobuf;
    public class ResultEventInfoLoader : BaseConfigLoader<ResultEventInfo>
    {
        public override string ConfigName => "ConfigAssets/PbJson/ResultEventInfo.pb.json";
        private ResultEventInfoLoader() {}
        private static readonly ResultEventInfoLoader s_instance = new ResultEventInfoLoader();
        public static ResultEventInfoLoader Instance
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
        public IReadOnlyList<ResultEventInfo.Types.ResultEventItem> ResultEventConfig => Table.ResultEventConfig;
        public IReadOnlyDictionary<long, ResultEventInfo.Types.ResultEventItem> ResultEventItemDic => Table.ResultEventItemDic;
        public ResultEventInfo.Types.ResultEventItem FindResultEventItem(long key)
        {
            ResultEventItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class ResultEventInfo : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, ResultEventInfo.Types.ResultEventItem> ResultEventItemDic = new Dictionary<long, ResultEventInfo.Types.ResultEventItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in ResultEventConfig)
            {
                ResultEventItemDic[item.ResultId] = item;
            }
        }
    }
}