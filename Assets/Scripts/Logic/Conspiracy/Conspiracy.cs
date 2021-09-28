using CatConspiracyInfo;
using UnityEngine;

namespace Logic.Conspiracy
{
    public class Conspiracy
    {
        private long ID { get; set; }
        private string Desc { get; set; }
        private long Conditions { get; set; }
        public CatConspiracyInfo.CatConspiracyInfo.Types.CatConspiracyItem Config { get; private set; }
        
        public Conspiracy(long id)
        {
            ID = id;
            Config = CatConspiracyInfoLoader.Instance.FindCatConspiracyItem(id);
            if (Config == null)
            {
                Debug.LogError($"Invalid Cat Id:{id}");
                return;
            }

            Desc = Config.Description;
            Conditions = Config.Condition;
        }
    }
}