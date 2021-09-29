using System;
using System.Collections.Generic;
using HumanInfo;
using Language;
using Manager;
using UnityEngine;

namespace Logic.Human
{
    public class Human
    {
        public enum PropertyType
        {
            Visibility = 0,
            Defence = 1
        }
        
        public long ID { get; private set; }

        public readonly HumanInfo.HumanInfo.Types.person Config;

        public string Name { get; private set; }
        
        public string Title { get; private set; }

        public string Image { get; private set; }
        
        public int Sex { get; private set; }

        public bool IsImport { get; private set; } = false;

        public bool IsWashHead { get; private set; }

        public bool IsEventRollActive { get; private set; }
        
        public bool IsAlive { get; private set; }
        
        public List<String> Tags { get; private set; }

        private int[] m_properties = new int[2];
        private bool[] m_locks = new bool[2];

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
        
        

        /// <summary>
        /// 养的猫
        /// </summary>
        public Cat cat;


        public Human(long mID)
        {
            ID = mID;
            Config = HumanInfoLoader.Instance.Findperson(mID);
            if (Config == null)
            {
                Debug.LogError("Invalid human id!");
                return;
            }

            Name = Config.Name;
            Title = Config.Title;
            Image = Config.Image;
            Sex = Config.Sex;
            IsImport = Config.IsImportant == 1;
            IsWashHead = false;

            m_properties[(int) PropertyType.Visibility] = Config.Visibility;
            m_properties[(int) PropertyType.Defence] = 0;

            Tags = new List<String>(Config.Tags);
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
        }

        public void SetEventRoll(bool status)
        {
            IsEventRollActive = status;
            //todo
        }

        public void Death()
        {
            IsAlive = false;
            //todo 处理一些必要的逻辑
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
        }

        public void SetImage(string image)
        {
            Image = image;
            //todo
        }
    }
}