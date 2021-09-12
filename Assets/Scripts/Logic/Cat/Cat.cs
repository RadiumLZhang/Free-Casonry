using System.Collections.Generic;
using CatInfo;
using UnityEngine;

namespace Logic
{
    public class Cat
    {
        public long ID { get; private set; }
        
        public CatInfo.CatInfo.Types.CatItemConfig Config { get; private set; }

        public string Name { get; private set; }

        public string Type { get; private set; }

        public string Image { get; private set; }
        
        public List<int> Properties { get; private set; }

        /// <summary>
        /// 刺探
        /// </summary>
        public int ScoutValue => Properties[0];
        /// <summary>
        /// 密谋
        /// </summary>
        public int Conspiracy => Properties[1];
        /// <summary>
        /// 交流
        /// </summary>
        public int Communicate => Properties[2];

        public long skillId { get; set; }
        
        public string Description { get; private set; }

        public Cat(long id)
        {
            ID = id;
            Config = CatInfoLoader.Instance.FindCatItemConfig(id);
            if (Config == null)
            {
                Debug.LogError($"Invalid Cat Id:{id}");
                return;
            }

            Name = Config.Name;
            Type = Config.Type;
            Image = Config.Image;

            Properties = new List<int>(Config.Property);
        }
        
        public enum CatStatus
        {
            None,
            Normal
        }
    }
}