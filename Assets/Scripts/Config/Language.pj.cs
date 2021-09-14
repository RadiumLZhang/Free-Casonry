using System.Collections.Generic;
using Config;
namespace Language
{
    using pb = Google.Protobuf;
    public class LanguageLoader : BaseConfigLoader<Language>
    {
        public override string ConfigName => "ConfigAssets/PbJson/Language.pb.json";
        private LanguageLoader() {}
        private static readonly LanguageLoader s_instance = new LanguageLoader();
        public static LanguageLoader Instance
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
        public IReadOnlyList<Language.Types.LanguageItem> LanguageConfig => Table.LanguageConfig;
        public IReadOnlyDictionary<string, Language.Types.LanguageItem> LanguageItemDic => Table.LanguageItemDic;
        public Language.Types.LanguageItem FindLanguageItem(string key)
        {
            LanguageItemDic.TryGetValue(key, out var value);
            return value;
        }
    }
    public partial class Language : Pbjson.IRepeatedFieldConvert
    {
        public readonly Dictionary<string, Language.Types.LanguageItem> LanguageItemDic = new Dictionary<string, Language.Types.LanguageItem>();
        public void RepeatedFieldToDictionary()
        {
            foreach (var item in LanguageConfig)
            {
                LanguageItemDic[item.Key] = item;
            }
        }
    }
}