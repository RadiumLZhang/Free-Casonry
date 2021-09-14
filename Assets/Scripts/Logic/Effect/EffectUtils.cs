using BaseEffect;
using Effect;
using UnityEngine;

namespace Logic.Effect
{
    public class EffectUtils
    {
        public static string GetDescription(long id)
        {
            var config = EffectLoader.Instance.FindEffectItem(id);
            if (config == null)
            {
                Debug.LogError($"Invalid ConditionId:{id}");
                return "";
            }

            var description = BaseEffectLoader.Instance.FindBaseEffectItem(config.BaseEffectId)?.Value;

            return description ?? "";
        }
        
        public static void ActiveEffect(long id)
        {
            var config = EffectLoader.Instance.FindEffectItem(id);
            if (config == null)
            {
                Debug.LogError($"Invalid EffectId:{id}");
                return;
            }
            
            Trigger(config.BaseEffectId, config.Paras);
        }
        
        private static void Trigger(long id, params object[] args)
        {
            switch (id)
            {
                case 1000:
                    GetMoney(args);
                    break;
            }
        }

        private static void GetMoney(params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }

            if (args[0] is int value)
            {
                //todo
            }
        }
    }
}