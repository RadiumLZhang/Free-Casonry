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
         
        /// <summary>
        /// yingy
        /// </summary>
        /// <param name="id">应用id</param>
        /// <returns></returns>
        public static bool CheckCondition(long id)
        {
            var config = ConditionLoader.Instance.FindConditionItem(id);
            if (config == null)
            {
                Debug.LogError($"Invalid ConditionId:{id}");
                return false;
            }
            
            // confition id 是基础id，parameter是参数
            return Check(config.BaseConditionId, config.Paras);
        }
        
        // 基础条件id， 和传入参数
        private static bool Check(long id, params object[] args)
        {
            switch (id)
            {
                case 4000: // 拥有至少X点“人类货币”
                    return CheckMoneyGreaterEqual(args);
                    break;
                case 4001: // 拥有至少X点“猫咪影响力”
                    return CheckCatInfluenceGreaterEqual(args);
                    break;
                /*case 4002: // 拥有至少X点“隐匿度”
                    return CheckCatHidency(args); 
                    break;
                case 4003: // 当前时间已到达X
                    return CheckTimeReach(args) == true;
                    break;
                case 4004: // 当前时间未到达X
                    return CheckTimeReach(args) == false;
                    break;
                case 4005: // 议程槽数量至少为X
                    return CheckEventHandlerCountGreaterEqual(args);
                    break;
                case 4006: // 议程槽数量为X
                    return CheckEventHandlerCountEqual(agrs);
                    break;
                case 4007: // 议程槽数量少于X
                    return CheckEventHandlerCountGreaterEqual(agrs) == false;
                    break;
                case 4008: // 猫咪阴谋目标X已完成
                    return CheckCatPurpose(args);
                    break;
                case 4100: // 目标人物X，拥有性格标签Y
                    return CheckHumanHasTag(args);
                case 4101: // 目标人物X，没有性格标签Y
                    return CheckHumanHasTag(args) == false;
                case 4102: //目标人物X，能见度至少为Y
                    return CheckHumanVisibilityGreaterEqual(args);
                    break;
                case 4103: // 目标人物X，能见度为Y
                    return CheckHumanVisibilityEqual(args);
                    break;
                case 4104: // 目标人物X，能见度低于Y
                    return CheckHumanVisibilityGreaterEqual(args) == false;
                    break;
                case 4105:// 目标人物X，养猫意愿至少为Y
                    return CheckHumanRaiseCatFavorGreaterEqual(args);
                    break;
                case 4106: // 目标人物X，养猫意愿为Y
                    return CheckHumanRaiseCatFavorEqual(args);
                    break;
                case 4107: // 目标人物X，养猫意愿低于Y
                    return CheckHumanVisibilityGreaterEqual(args) == false;
                    break;
                case 4108: // 目标人物X，亲密度至少为Y
                    return CheckHumanIntimacyGreaterEqual(args);
                    break;
                case 4109: //目标人物X，亲密度为Y
                    CheckHumanIntimacyEqual(args);
                    break;*/
            }

            return false;
        }

        private static bool CheckMoneyGreaterEqual(params object[] args)
        {
            return (int)args[0] >= PlayerModel.Instance.Money;
        }

        private static bool CheckCatInfluenceGreaterEqual(params object[] args)
        {
            return (int) args[0] > PlayerModel.Instance.Influence;
        }
    }
}