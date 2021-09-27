using BaseEffect;
using Effect;
using Manager;
using UnityEngine;

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
                    ButtonCouncilStateChange(false);
                    break;
                case 5018:
                    ButtonCouncilStateChange(true);
                    break;
                case 5019:
                    ButtonCatManageStateChange(false);
                    break;
                case 5020:
                    ButtonCatManageStateChange(true);
                    break;
                //游戏结局
                case 5021:
                case 5022:
                case 5023:
                case 5024:
                case 5025:
                    GameEnd(args);
                    break;
                //播放动效？
                case 5026:
                    break;
                
                //人物相关
                case 5100:
                    TagChange(true);
                    break;
                case 5101:
                    TagChange(false);
                    break;
                case 5102:
                    HumanPropertyChange(Human.Human.PropertyType.Visibility, Human.Human.PropertyOperate.Add, args);
                    break;
                case 5103:
                    HumanPropertyChange(Human.Human.PropertyType.Visibility, Human.Human.PropertyOperate.Minus, args);
                    break;
                case 5104:
                    HumanPropertyChange(Human.Human.PropertyType.Visibility, Human.Human.PropertyOperate.Set, args);
                    break;
                case 5105:
                    HumanPropertyLock(Human.Human.PropertyType.Visibility, true, args);
                    break;
                case 5106:
                    HumanPropertyLock(Human.Human.PropertyType.Visibility, false, args);
                    break;
                case 5107:
                    HumanPropertyChange(Human.Human.PropertyType.Defence, Human.Human.PropertyOperate.Add, args);
                    break;
                case 5108:
                    HumanPropertyChange(Human.Human.PropertyType.Defence, Human.Human.PropertyOperate.Minus, args);
                    break;
                case 5109:
                    HumanPropertyChange(Human.Human.PropertyType.Defence, Human.Human.PropertyOperate.Set, args);
                    break;
                case 5110:
                    HumanPropertyLock(Human.Human.PropertyType.Defence, true, args);
                    break;
                case 5111:
                    HumanPropertyLock(Human.Human.PropertyType.Defence, false, args);
                    break;
                default:
                    break;
            }
        }

        private static void SetResource(PlayerModel.ResourceType type, ResourceOperate sign, params object[] args)
        {
            if (args == null || args.Length <= 1)
            {
                return;
            }

            if (!(args[0] is int value))
            {
                return;
            }

            var result = PlayerModel.Instance.GetResource(type);
            if (sign != ResourceOperate.Set)
            {
                result += (int)sign * value;
            }
            
            PlayerModel.Instance.SetResource(type, result);
        }

        private static void HandlerChange(bool active, params object[] args)
        {
            if (args == null || args.Length <= 1)
            {
                return;
            }

            if (!(args[0] is int index))
            {
                return;
            }

            if (active)
            {
                EventHandlerManager.Instance.EnableHandler(index);
            }
            else
            {
                EventHandlerManager.Instance.DisableHandler(index);
            }
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
            if (args == null || args.Length <= 2)
            {
                return;
            }
            
            //todo
        }

        private static void ButtonCouncilStateChange(bool active)
        {
            UIManager.Instance.buttonCouncil.SetActive(active);
        }

        private static void ButtonCatManageStateChange(bool active)
        {
            UIManager.Instance.buttonCouncilCatManage.SetActive(active);
        }

        private static void GameEnd(params object[] args)
        {
            //todo
        }

        private static void TagChange(bool isAdd, params object[] args)
        {
            if (args == null || args.Length <= 2)
            {
                return;
            }

            if (!(args[0] is long humanId) || !(args[1] is long tagId))
            {
                return;
            }
            var human = HumanManager.Instance.GetHuman(humanId);
            if (human == null)
            {
                return;
            }
            
            if (isAdd)
            {
                human.Tags.Add(tagId);
            }
            else
            {
                human.Tags.Remove(tagId);
            }
        }

        private static void HumanPropertyChange(Human.Human.PropertyType type, Human.Human.PropertyOperate operate, params object[] args)
        {
            if (args == null || args.Length <= 2)
            {
                return;
            }

            if (!(args[0] is long humanId) || !(args[1] is int value))
            {
                return;
            }

            var human = HumanManager.Instance.GetHuman(humanId);
            if (human == null)
            {
                return;
            }

            var result = human.GetProperty(type);
            if (operate != Human.Human.PropertyOperate.Set)
            {
                result += value * (int) operate;
            }
            
            human.SetProperty(type, result);
        }

        private static void HumanPropertyLock(Human.Human.PropertyType type, bool isLock, params object[] args)
        {
            if (args == null || args.Length <= 1)
            {
                return;
            }
            
            if (!(args[0] is long humanId))
            {
                return;
            }
            
            var human = HumanManager.Instance.GetHuman(humanId);

            human?.SetLock(type, isLock);
        }
    }
}