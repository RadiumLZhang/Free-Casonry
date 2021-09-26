using BaseEffect;
using Effect;
using EventHandler;
using Manager;
using UnityEngine;
using UnityEngine.Assertions.Must;

using ResourceOperate = Logic.PlayerModel.ResourceOperate;
using ResourceType = Logic.PlayerModel.ResourceType;

namespace Logic.Effect
{
    public class EffectUtils
    {
        /// <summary>
        /// 获取描述
        /// </summary>
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
        
        /// <summary>
        /// 激活应用效果
        /// </summary>
        public static void ActiveEffect(long id)
        {
            var config = EffectLoader.Instance.FindEffectItem(id);
            if (config == null)
            {
                Debug.LogError($"Invalid EffectId:{id}");
                return;
            }
            
            ActiveBaseEffect(config.BaseEffectId, config.Paras);
        }
        
        private static void ActiveBaseEffect(long id, params object[] args)
        {
            switch (id)
            {
                //资源类变更
                case 5000:
                    SetResource(ResourceType.Money, ResourceOperate.Add, args);
                    break;
                case 5001:
                    SetResource(ResourceType.Money,ResourceOperate.Minus, args);
                    break;
                case 5002:
                    SetResource(ResourceType.Money, ResourceOperate.Set, args);
                    break;
                case 5003:
                    SetResource(ResourceType.Influence, ResourceOperate.Add, args);
                    break;
                case 5004:
                    SetResource(ResourceType.Influence, ResourceOperate.Minus, args);
                    break;
                case 5005:
                    SetResource(ResourceType.Influence, ResourceOperate.Set, args);
                    break;
                case 5006:
                    SetResource(ResourceType.Cohesion, ResourceOperate.Add, args);
                    break;
                case 5007:
                    SetResource(ResourceType.Cohesion, ResourceOperate.Minus, args);
                    break;
                case 5008:
                    SetResource(ResourceType.Cohesion, ResourceOperate.Set, args);
                    break;
                //议程槽变更
                case 5009:
                    HandlerChange(true, args);
                    break;
                case 5010:
                    HandlerChange(false, args);
                    break;
                case 5011://完成目标
                    break;
                case 5012: //设置时间
                    SetTime();
                    break;
                //事件记录变更
                case 5013: 
                    RecordChange(true);
                    break;
                case 5014:
                    RecordChange(false);
                    break;
                //生成事件(建议两个合并)
                case 5015:
                    EventGenerate(args);
                    break;
                case 5016:
                    EventGenerate(args);
                    break;
                //按钮变化
                case 5017:
                    UIButtonChange(args);
                    break;
                default:
                    break;
            }
        }

        private static void SetResource(PlayerModel.ResourceType type, ResourceOperate sign, params object[] args)
        {
            if (args == null || args.Length != 1)
            {
                return;
            }

            if (!(args[0] is int value))
            {
                return;
            }

            int result;
            if (sign == 0)
            {
                result = value;
            }
            else
            {
                result = PlayerModel.Instance.GetResource(type);
                result += (int)sign * value;
            }
            
            PlayerModel.Instance.SetResource(type, result);
        }

        private static void HandlerChange(bool active, params object[] args)
        {
            if (args == null || args.Length != 1)
            {
                return;
            }

            if (!(args[0] is int index))
            {
                return;
            }

            EventHandlerManager.Instance.EnableHandler(index);
        }

        private static void SetTime(params object[] args)
        {
            // TimeManager.Instance
        }

        private static void RecordChange(bool isAdd, params object[] args)
        {
            //todo
        }

        private static void EventGenerate(params object[] args)
        {
            if (args == null || args.Length != 2)
            {
                return;
            }
            
            
        }

        private static void UIButtonChange(params object[] args)
        {
            
        }
    }
}