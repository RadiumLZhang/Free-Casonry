using System.Collections.Generic;
using EmergencyInfo;
using Logic.Effect;
using UnityEngine;

namespace Logic.Event
{
    public class Emergency
    {
        public long ID { get; private set; }
        
        public EmergencyInfoConfig.Types.EmergencyItem Config { get; private set; }

        public string Name => Config?.Name;

        public string Description => Config?.Description;

        //可以考虑挪到主事件里面去
        public uint TimeOffset => Config?.TimeOffset ?? 0;
        
        //好像缺了个持续时间？

        public int DefaultOption => Config?.DefaultOption ?? 0;
        
        public List<EmergencyInfoConfig.Types.Option> options;

        private int choice;

        public Emergency(long id)
        {
            choice = -1;
            ID = id;
            Config = EmergencyInfoConfigLoader.Instance.FindEmergencyItem(id);
            if (Config == null)
            {
                Debug.LogError($"Invalid EmergencyId:{id}");
                return;
            }

            options = new List<EmergencyInfoConfig.Types.Option>(Config?.Options);
        }

        public List<EmergencyInfoConfig.Types.Option> GetOptions()
        {
            if (options != null)
            {
                return options;
            }
            
            if (Config?.Options == null)
            {
                return null;
            }
            options = new List<EmergencyInfoConfig.Types.Option>(Config?.Options);

            return options;
        }

        public uint GetTimeOffset()
        {
            return TimeOffset;
        }
        public bool OptionCanChoose(int option)
        {
            //todo 判断条件
            // Config?.Options[0].Conditions;
            return true;
        }

        public void Choose(int option)
        {
            choice = option;
            foreach (var effect in options[option-1].Effects)
            {
                EffectUtils.ActivateEffect(effect);
            }
            
        }

        public void ChosseDefaultOption()
        {
            choice = DefaultOption;
            Choose(DefaultOption);
        }

        public int GetChoice()
        {
            if (choice == -1)
            {
                return DefaultOption;
            }
            return choice;
        }

        public void SetChoice(int c)
        {
            choice = c;
        }
    }
}