using Google.Protobuf;
using Pbjson;
using UnityEngine;

namespace Config
{
    public abstract class BaseConfigLoader<T> where T : IRepeatedFieldConvert, IMessage<T>, new()
    {
        public abstract string ConfigName { get; }
        
        private static JsonParser s_defaultParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        
        public  T Table => m_table;
        private T m_table;

        public void Load()
        {
            var configName = ConfigName.Substring(0, ConfigName.Length - 5); 
            var txt = Resources.Load<TextAsset>(configName);

            if (txt == null || string.IsNullOrEmpty(txt.ToString()))
            {
                Debug.Log($"Read {ConfigName} Failed");
                return;
            }
            
            m_table = s_defaultParser.Parse<T>(txt.ToString());
            
            m_table.RepeatedFieldToDictionary();
        }
    }
}