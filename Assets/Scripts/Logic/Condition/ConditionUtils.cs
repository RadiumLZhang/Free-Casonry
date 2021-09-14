using BaseCondition;
using Condition;
using UnityEngine;

namespace Logic.Condition
{
    public class ConditionUtils
    {
        public static string GetDescription(long id)
        {
            var config = ConditionLoader.Instance.FindConditionItem(id);
            if (config == null)
            {
                Debug.LogError($"Invalid ConditionId:{id}");
                return "";
            }

            var description = BaseConditionLoader.Instance.FindBaseConditionItem(config.BaseConditionId)?.Value;

            return description ?? "";
        }
         
        public static bool CheckCondition(long id)
        {
            var config = ConditionLoader.Instance.FindConditionItem(id);
            if (config == null)
            {
                Debug.LogError($"Invalid ConditionId:{id}");
                return false;
            }
            
            return Check(config.ConditionId, config.Paras);
        }
        
        private static bool Check(long id, params object[] args)
        {
            switch (id)
            {
                case 1000:
                    CheckMoney(args);
                    break;
            }

            return false;
        }

        private static void CheckMoney(params object[] args)
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