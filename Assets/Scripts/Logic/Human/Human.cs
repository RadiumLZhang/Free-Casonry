using System.Collections.Generic;
using HumanInfo;
using Language;
using UnityEngine;

namespace Logic.Human
{
    public class Human
    {
        public long ID { get; private set; }

        private readonly HumanInfo.HumanInfo.Types.person Config;

        public string Name { get; private set; }
        
        public string Title { get; private set; }

        public string Image { get; private set; }
        
        public int Sex { get; private set; }

        public bool IsImport { get; private set; } = false;

        public bool IsWashHead { get; set; }
        
        public List<long> Tags { get; private set; }
        
        /// <summary>
        /// 能见度
        /// </summary>
        public int Visibility { get; private set; }
        
        /// <summary>
        /// 养猫意向
        /// </summary>
        public int TendToRaiseCat { get; private set; }
        
        /// <summary>
        /// 亲密度
        /// </summary>
        public int FavorValue { get; private set; }

        /// <summary>
        /// todo 养的猫
        /// </summary>
        /// <returns></returns>



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

            Visibility = Config.Visibility;
            TendToRaiseCat = TendToRaiseCat;
            FavorValue = FavorValue;
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

        public List<long> GetTags()
        {
            if (Tags != null)
            {
                return Tags;
            }
            
            if (Config?.Tags == null)
            {
                return null;
            }
            
            Tags = new List<long>(Config.Tags);

            return Tags;
        }
    }
}