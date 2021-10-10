using System;
using System.Collections.Generic;
using HumanInfo;
using Language;
using Manager;
using UnityEngine;

namespace Logic.Human
{
    [Serializable]
    public class Human
    {
        public enum PropertyType
        {
            Visibility = 0,
            Defence = 1
        }
        
        public long ID { get; set; }

        public HumanInfo.HumanInfo.Types.person Config;

        public string Name { get; set; }
        
        public string Title { get; set; }

        public string Image { get; set; }
        
        public int Sex { get; set; }

        public bool IsImport { get; set; } = false;

        public bool IsWashHead { get; set; }

        public bool IsEventRollActive { get; set; }

        public bool IsAlive { get; set; } = true;
        
        public List<String> Tags { get; set; }

        public int[] m_properties = new int[2];
        public bool[] m_locks = new bool[2];

        /// <summary>
        /// 能见度
        /// </summary>
        public int Visibility => m_properties[0];
        public bool VisibilityLock => m_locks[0];

        /// <summary>
        /// 心防
        /// </summary>
        public int Defence => m_properties[1];
        public bool DefenceLock => m_locks[1];

        public bool IsShow { get; set; }

        /// <summary>
        /// 养的猫
        /// </summary>
        public Cat cat;

        public long CatId { get; set; }


        public Human(long mID)
        {
            ID = mID;
            Config = HumanInfoLoader.Instance.Findperson(mID);
            if (Config == null)
            {
                Debug.LogError($"Invalid human id {mID}!");
                return;
            }

            Name = Config.Name;
            Title = Config.Title;
            Image = Config.Image;
            Sex = Config.Sex;
            IsImport = Config.IsImportant == 1;
            IsWashHead = false;

            m_properties[(int) PropertyType.Visibility] = Config.Visibility;
            m_properties[(int) PropertyType.Defence] = Config.Defense;

            Tags = new List<String>(Config.Tags);
        }

        /**
         * 不带参数的构造函数仅用于存档的反序列化
         */
        public Human()
        {
            
        }
        
        public bool CanRaiseCat()
        {
            if (Config == null)
            {
                return false;
            }

            return Config.CanRaiseCat == 1;
        }

        public bool CanWashHead()
        {
            if (Config == null)
            {
                return false;
            }

            return Config.CanWashHead == 1;
        }

        public List<String> GetTags()
        {
            return Tags;
        }

        public void SetProperty(PropertyType type, int value)
        {
            if (m_locks[(int) type])
            {
                return;
            }
            m_properties[(int) type] = value;
        }

        public int GetProperty(PropertyType type)
        {
            return m_properties[(int) type];
        }

        public void SetLock(PropertyType type, bool isLock)
        {
            m_locks[(int) type] = isLock;
        }

        public void SetWashHead(bool status)
        {
            IsWashHead = status;
            NPCManager.SetNpcWashHead(ID);
        }

        public void SetEventRoll(bool status)
        {
            IsEventRollActive = status;
            //todo
        }

        public void Death()
        {
            IsAlive = false;
            if (!NPCManager.NPCs.TryGetValue(ID, out var mono))
            {
                return;
            }

            mono.Death();
            NPCManager.SetEventCycleActive(ID, false);
        }

        public void SetCat(long id)
        {
            if (id == 0)
            {
                cat = null;
            }
            else
            {
                cat = CatManager.Instance.GetCat(id);
            }
            
            NPCManager.SetNpcCat(ID);
        }

        public void SetImage(string image)
        {
            Image = image;
        }

        public void Show()
        {
            IsShow = true;
            
            if(!NPCManager.NPCs.TryGetValue(ID, out var npcMono))
            {
                return;
            }

            npcMono.Show();
            NPCManager.SetNpcCat(ID);
            NPCManager.SetNpcWashHead(ID);
        }

        public void Restore()
        {
            Config = HumanInfoLoader.Instance.Findperson(ID);
            if (CatId != 0)
            {
                cat = CatManager.Instance.GetCat(CatId);
            }
        }
    }
}