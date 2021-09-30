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
        private List<ISaveObject> nodeList;
        private string savePath;

        public void Init()
        {
            savePath = "Assets/Resources/Save";
            nodeList = new List<ISaveObject>();
            
            // 需要存档的对象在这里注册
            nodeList.Add(TimeTickerManager.Instance);
            nodeList.Add(TimeManager.Instance);
            nodeList.Add(EventManager.Instance);
            nodeList.Add(PlayerModel.Instance);
            nodeList.Add(HumanManager.Instance);
            nodeList.Add(EmergencyManager.Instance);
            nodeList.Add(UIManager.Instance);
            nodeList.Add(CatManager.Instance);
            nodeList.Add(EventHandlerManager.Instance);

            string userName = PlayerPrefs.GetString("saveName");
            if (userName != null && !"".Equals(userName))
            {
                LoadData(userName);
            }
        }

        public void SaveData(string name)
        {
            string filePath = savePath + "/" + name + ".txt";
            var list = new List<string>();
            foreach (var node in nodeList)
            {
                list.Add(node.Save());
            }
            string jsonString = JsonConvert.SerializeObject(list);

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

            //var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
            var asset = Resources.Load<TextAsset>(filePath);
            var json = asset.text;
            var list = JsonConvert.DeserializeObject<List<string>>(json);
            for (int i = 0; i < nodeList.Count; i++)
            {
                nodeList[i].Load(list[i]);
            }

            return true;
        }
    }
}
