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
        private List<ISaveObject> m_nodeList;
        private string m_savePath;
        private string m_loadPath;

        public void Init()
        {
            m_savePath = "Assets/Resources/Save";
            m_loadPath = "Save";
            m_nodeList = new List<ISaveObject>();
            
            // 需要存档的对象在这里注册
            m_nodeList.Add(TimeTickerManager.Instance);
            m_nodeList.Add(TimeManager.Instance);
            m_nodeList.Add(EventManager.Instance);
            m_nodeList.Add(PlayerModel.Instance);
            m_nodeList.Add(HumanManager.Instance);
            m_nodeList.Add(EmergencyManager.Instance);
            m_nodeList.Add(UIManager.Instance);
            m_nodeList.Add(CatManager.Instance);
            m_nodeList.Add(EventHandlerManager.Instance);

            string userName = PlayerPrefs.GetString("saveName");
            if (userName != null && !"".Equals(userName))
            {
                LoadData(userName);
            }
        }

        public void SaveData(string name)
        {
            string filePath = m_savePath + "/" + name + ".txt";
            var list = new List<string>();
            foreach (var node in m_nodeList)
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
            string filePath = $"{m_loadPath}/{name}";
            // if (!File.Exists(filePath))
            // {
            //     return false;
            // }

            //var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(filePath);
            var asset = Resources.Load<TextAsset>(filePath);
            var json = asset.text;
            var list = JsonConvert.DeserializeObject<List<string>>(json);
            for (int i = 0; i < m_nodeList.Count; i++)
            {
                m_nodeList[i].Load(list[i]);
            }

            return true;
        }
    }
}
