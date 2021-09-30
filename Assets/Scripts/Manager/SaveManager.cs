using System;
using System.Collections.Generic;
using Logic;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEditor;

namespace Manager
{
    /*
     * 使用存档的对象需要实现以下接口，然后在SaveManager的Init当中注册
     */
    public interface ISaveObject
    {
        public string Save();

        public void Load(string json);
    }
    
    public class SaveManager : BaseModel<SaveManager>
    {
        private Dictionary<string, ISaveObject> saveMap;
        private string savePath;
        
        public void Init()
        {
            savePath = "Assets/Resources/Save";
            saveMap = new Dictionary<string, ISaveObject>();
            
            // 需要存档的对象在这里注册
            saveMap["EventManager"] = EventManager.Instance;
            saveMap["TimeTickerManager"] = TimeTickerManager.Instance;
            saveMap["TimeManager"] = TimeManager.Instance;
            saveMap["PlayerModel"] = PlayerModel.Instance;
            saveMap["EmergencyManager"] = EmergencyManager.Instance;

            string userName = PlayerPrefs.GetString("userName");
            if (userName != null && !"".Equals(userName))
            {
                LoadData(userName);
            }
        }

        public void SaveData(string name)
        {
            string filePath = savePath + "/" + name + ".txt";
            var map = new Dictionary<string, string>();
            foreach (var kv in saveMap)
            {
                map[kv.Key] = kv.Value.Save();
            }
            string jsonString = JsonConvert.SerializeObject(map);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            FileInfo fileInfo = new FileInfo(filePath);
            StreamWriter streamWriter = fileInfo.CreateText();
            streamWriter.Write(jsonString);
            streamWriter.Close();
        }

        public bool LoadData(string name)
        {
            string filePath = savePath + "/" + name + ".txt";
            if (!File.Exists(filePath))
            {
                return false;
            }

            var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
            var json = asset.text;
            var map = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            foreach (var kv in map)
            {
                saveMap[kv.Key].Load(kv.Value);
            }

            return true;
        }
    }
}