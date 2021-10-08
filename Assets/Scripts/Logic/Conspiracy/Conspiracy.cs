using CatConspiracyInfo;
using Logic.Condition;
using UnityEngine;

namespace Logic.Conspiracy
{
    public class Conspiracy
    {
        public long ID { get; set; }
        public string Desc { get; set; }
        public long Conditions { get; set; }
        public long GroupId { get; set; }
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
            GroupId = Config.GroupId;
        }

        public bool Check()
        {
            return ConditionUtils.CheckCondition(Conditions);
        }
    }
}