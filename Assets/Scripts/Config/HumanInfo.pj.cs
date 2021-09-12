using System.Collections.Generic;
using Config;
namespace HumanInfo
{
    using pb = Google.Protobuf;
    public class HumanInfoLoader : BaseConfigLoader<HumanInfo>
    {
        public override string ConfigName => "ConfigAssets/PbJson/HumanInfo.pb.json";
        private HumanInfoLoader() {}
        private static readonly HumanInfoLoader s_instance = new HumanInfoLoader();
        public static HumanInfoLoader Instance
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
        public IReadOnlyList<HumanInfo.Types.person> People => Table.People;
        public IReadOnlyDictionary<long, HumanInfo.Types.person> personDic => Table.personDic;
        public HumanInfo.Types.person Findperson(long key)
        {
            personDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class HumanInfo : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<long, HumanInfo.Types.person> personDic = new Dictionary<long, HumanInfo.Types.person>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in People)
            {
                personDic[item.HumanId] = item;
            }
        }
    }
}