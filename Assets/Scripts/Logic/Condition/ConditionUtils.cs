using BaseCondition;
using Condition;
using EventHandler;
using Manager;
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
            if (id == ConstValue.CONDITION_TRUE_ID)
            {
                return true;
            }

            if (id == ConstValue.CONDITION_FALSE_ID)
            {
                return false;
            }
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
                case 4001: // 拥有至少X点“猫咪影响力”
                    return CheckCatInfluenceGreaterEqual(args);
                case 4002: // 拥有至少X点“隐匿度”
                    return CheckCatHidencyGreaterEqual(args);
                case 4003: // 当前时间已到达X
                    return CheckTimeReach(args) == true;
                case 4004: // 当前时间未到达X
                    return CheckTimeReach(args) == false;
                case 4005: // 议程槽数量至少为X
                    return CheckEventHandlerCountGreaterEqual(args);
                case 4006: // 议程槽数量为X
                    return CheckEventHandlerCountEqual(args);
                case 4007: // 议程槽数量少于X
                    return CheckEventHandlerCountGreaterEqual(args) == false;
                case 4008: // 猫咪阴谋目标X已完成
                    return CheckCatPurpose(args);
                case 4100: // 目标人物X，拥有性格标签Y
                    return CheckHumanHasTag(args);
                case 4101: // 目标人物X，没有性格标签Y
                    return CheckHumanHasTag(args) == false;
                case 4102: //目标人物X，能见度至少为Y
                    return CheckHumanVisibilityGreaterEqual(args);
                case 4103: // 目标人物X，能见度为Y
                    return CheckHumanVisibilityEqual(args);
                case 4104: // 目标人物X，能见度低于Y
                    return CheckHumanVisibilityGreaterEqual(args) == false;
                    // case 4105:// 目标人物X，养猫意愿至少为Y
                    //     return CheckHumanRaiseCatFavorGreaterEqual(args);
                    // case 4106: // 目标人物X，养猫意愿为Y
                    //     return CheckHumanRaiseCatFavorEqual(args);
                    // case 4107: // 目标人物X，养猫意愿低于Y
                    //     return CheckHumanRaiseCatFavorGreaterEqual(args) == false;
                    // case 4108: // 目标人物X，亲密度至少为Y
                    //     return CheckHumanIntimacyGreaterEqual(args);
                    // case 4109: //目标人物X，亲密度为Y
                    //     return CheckHumanIntimacyEqual(args);
            }

            return false;
        }

        private static bool CheckMoneyGreaterEqual(params object[] args)
        {
            return (int)args[0] >= PlayerModel.Instance.Money;
        }

        private static bool CheckCatInfluenceGreaterEqual(params object[] args)
        {
            return (int) args[0] >= PlayerModel.Instance.Influence;
        }

        private static bool CheckCatHidencyGreaterEqual(params object[] args)
        {
            return (int) args[0] >= PlayerModel.Instance.Hidency;
        }

        private static bool CheckTimeReach(params object[] args)
        {
            // TODO 缺时间接口
            //return TimeManager.GetTimeStamp() >= 
            return true;
        }

        private static bool CheckEventHandlerCountGreaterEqual(params object[] args)
        {
            return (int)args[0] >= EventHandlerManager.Instance.GetHandlerCount();
        }
        
        private static bool CheckEventHandlerCountEqual(params object[] args)
        {
            return (int)args[0] == EventHandlerManager.Instance.GetHandlerCount();
        }
        
         private static bool CheckCatPurpose(params object[] args)
        {
            // TODO 猫咪目标
            return true;
        }

         private static bool CheckHumanHasTag(params object[] args)
         {
             return HumanManager.Instance.GetHuman((int) args[0]).GetTags().Exists(l => l == (long) args[1]);
         }
         
         private static bool CheckHumanVisibilityGreaterEqual(params object[] args)
         {
             return (int) args[1] >= HumanManager.Instance.GetHuman((int) args[0]).Visibility;
         }
         
         private static bool CheckHumanVisibilityEqual(params object[] args)
         {
             
             return (int) args[1] == HumanManager.Instance.GetHuman((int) args[0]).Visibility;
         }
         
         // private static bool CheckHumanRaiseCatFavorGreaterEqual(params object[] args)
         // {
         //     return (int) args[1] >= HumanManager.Instance.GetHuman((int) args[0]).GetTendToRaiseCat();
         // }
         //
         // private static bool CheckHumanRaiseCatFavorEqual(params object[] args)
         // {
         //     return (int) args[1] == HumanManager.Instance.GetHuman((int) args[0]).GetTendToRaiseCat();
         // }
         //
         // private static bool CheckHumanIntimacyGreaterEqual(params object[] args)
         // {
         //     return (int) args[1] >= HumanManager.Instance.GetHuman((int) args[0]).GetFavorValue();
         // }
         //
         // private static bool CheckHumanIntimacyEqual(params object[] args)
         // {
         //     return (int) args[1] == HumanManager.Instance.GetHuman((int) args[0]).GetFavorValue();
         // }
    }
}