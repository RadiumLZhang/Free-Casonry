using System;
using System.Collections.Generic;
using BaseEffect;
using Effect;
using Language;
using Manager;
using UnityEngine;

using ResourceType = Logic.PlayerModel.ResourceType;

namespace Logic.Effect
{
    public class EffectUtils
    {
        /// <summary>
        /// 获取描述
        /// </summary>
        public static string GetDescription(int id)
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
        public static void ActivateEffect(long id)
        {
            var config = EffectLoader.Instance.FindEffectItem(id);
            if (config == null)
            {
                Debug.LogError($"Invalid EffectId:{id}");
                return;
            }
            
            var parasCount = config.Paras.Count;
            var list = new object[parasCount];
            for (int i = 0; i < parasCount; i++)
            {
                list[i] = config.Paras[i];
            }
            
            ActivateBaseEffect(config.BaseEffectId, list);
        }
        
        private static void ActivateBaseEffect(long id, params object[] args)
        {
            switch (id)
            {
                //资源类变更
                case 5000:
                    SetGlobalValue(ResourceType.Money, Operate.Add, args);
                    break;
                case 5001:
                    SetGlobalValue(ResourceType.Money,Operate.Minus, args);
                    break;
                case 5002:
                    SetGlobalValue(ResourceType.Money, Operate.Set, args);
                    break;
                case 5003:
                    SetGlobalValue(ResourceType.Influence, Operate.Add, args);
                    break;
                case 5004:
                    SetGlobalValue(ResourceType.Influence, Operate.Minus, args);
                    break;
                case 5005:
                    SetGlobalValue(ResourceType.Influence, Operate.Set, args);
                    break;
                case 5006:
                    SetGlobalValue(ResourceType.Hidency, Operate.Add, args);
                    break;
                case 5007:
                    SetGlobalValue(ResourceType.Hidency, Operate.Minus, args);
                    break;
                case 5008:
                    SetGlobalValue(ResourceType.Hidency, Operate.Set, args);
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
                    RecordChange(true, args);
                    break;
                case 5014:
                    RecordChange(false, args);
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
                // case 5019:
                //     ButtonCatManageStateChange(false);
                //     break;
                // case 5020:
                //     ButtonCatManageStateChange(true);
                //     break;
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
                    HumanPropertyChange(Human.Human.PropertyType.Visibility, Operate.Add, args);
                    break;
                case 5103:
                    HumanPropertyChange(Human.Human.PropertyType.Visibility, Operate.Minus, args);
                    break;
                case 5104:
                    HumanPropertyChange(Human.Human.PropertyType.Visibility, Operate.Set, args);
                    break;
                case 5105:
                    HumanPropertyLock(Human.Human.PropertyType.Visibility, true, args);
                    break;
                case 5106:
                    HumanPropertyLock(Human.Human.PropertyType.Visibility, false, args);
                    break;
                case 5107:
                    HumanPropertyChange(Human.Human.PropertyType.Defence, Operate.Add, args);
                    break;
                case 5108:
                    HumanPropertyChange(Human.Human.PropertyType.Defence, Operate.Minus, args);
                    break;
                case 5109:
                    HumanPropertyChange(Human.Human.PropertyType.Defence, Operate.Set, args);
                    break;
                case 5110:
                    HumanPropertyLock(Human.Human.PropertyType.Defence, true, args);
                    break;
                case 5111:
                    HumanPropertyLock(Human.Human.PropertyType.Defence, false, args);
                    break;
                case 5117:
                    HumanWashHeadStatus(true, args);
                    break;
                case 5118:
                    HumanWashHeadStatus(false, args);
                    break;
                case 5121:
                    HumanEventRoll(false, args);
                    break;
                case 5128:
                    HumanEventRoll(true, args);
                    break;
                case 5122:
                    HumanDeath(args);
                    break;
                case 5123:
                    HumanGetCat(args);
                    break;
                case 5124:
                    HumanLoseCat(args);
                    break;
                case 5127:
                    HumanImageChange(args);
                    break;
                // 猫咪相关
                case 5200:
                    CatPropertyChange(Cat.CatPropertyType.Conspiracy, Operate.Add, args);
                    break;
                case 5201:
                    CatPropertyChange(Cat.CatPropertyType.Conspiracy, Operate.Minus, args);
                    break;
                case 5202:
                    CatPropertyChange(Cat.CatPropertyType.Conspiracy, Operate.Set, args);
                    break;
                case 5203:
                    CatPropertyChange(Cat.CatPropertyType.Scout, Operate.Add, args);
                    break;
                case 5204:
                    CatPropertyChange(Cat.CatPropertyType.Scout, Operate.Minus, args);
                    break;
                case 5205:
                    CatPropertyChange(Cat.CatPropertyType.Scout, Operate.Set, args);
                    break;
                case 5206:
                    CatPropertyChange(Cat.CatPropertyType.Communication, Operate.Add, args);
                    break;
                case 5207:
                    CatPropertyChange(Cat.CatPropertyType.Communication, Operate.Minus, args);
                    break;
                case 5208:
                    CatPropertyChange(Cat.CatPropertyType.Communication, Operate.Set, args);
                    break;
                case 5209:
                    CatStatusChange(true, args);
                    break;
                case 5210:
                    CatStatusChange(false, args);
                    break;
                case 5211:
                    CatImageChange(args);
                    break;
                //人际关系图操作
                case 5300:
                    ShowHuman(args);
                    break;
                case 5301:
                    ShowRelationLine(args);
                    break;
                // case 5302:
                //     ShowAll(args);
                //     break;
                case 5303:
                    SetRelation(args);
                    break;
                case 5500:
                    SetStoryProcess(args);
                    break;
                case 5501:
                    SetGlobalValue(ResourceType.ArmyDifference, Operate.Add, args);
                    break;
                case 5502:
                    SetGlobalValue(ResourceType.ArmyDifference, Operate.Minus, args);
                    break;
                case 5503:
                    SetGlobalValue(ResourceType.ArmyDifference, Operate.Set, args);
                    break;
                case 5507:
                    SetGlobalValue(ResourceType.DiseaseSurvey, Operate.Add, args);
                    break;
                case 5508:
                    SetGlobalValue(ResourceType.DiseaseSurvey, Operate.Minus, args);
                    break;
                case 5509:
                    SetGlobalValue(ResourceType.DiseaseSurvey, Operate.Set, args);
                    break;
                default:
                    break;
            }
        }

        private static void SetGlobalValue(PlayerModel.ResourceType type, Operate sign, params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }

            if (!(args[0] is int value))
            {
                Debug.LogError($"参数不为int");
                return;
            }

            var result = value;
            if (sign != Operate.Set)
            {
                result = PlayerModel.Instance.GetResource(type) + (int)sign * value;
            }
            
            PlayerModel.Instance.SetResource(type, result);
        }

        private static void HandlerChange(bool active, params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }

            if (!(args[0] is int index))
            {
                Debug.LogError($"参数不为int");
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
            var record = (int) args[0];
            if (isAdd)
            {
                PlayerModel.Instance.AddRecord(record);
            }
            else
            {
                PlayerModel.Instance.RemoveRecord(record);
            }
            
        }

        private static void EventGenerate(params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }
            
            //todo
        }

        private static void ButtonCouncilStateChange(bool active)
        {
            UIManager.Instance.SetButtonCouncil(active);
        }

        // private static void ButtonCatManageStateChange(bool active)
        // {
        //     UIManager.Instance.buttonCouncilCatManage.SetActive(active);
        // }

        private static void GameEnd(params object[] args)
        {
            //todo
        }

        private static void TagChange(bool isAdd, params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }

            if (!(args[0] is int humanId) || !(args[1] is String tag))
            {
                Debug.LogError($"参数不为int");
                return;
            }
            var human = HumanManager.Instance.GetHuman(humanId);
            if (human == null)
            {
                return;
            }
            
            if (isAdd)
            {
                human.Tags.Add(tag);
            }
            else
            {
                human.Tags.Remove(tag);
            }
        }

        private static void HumanPropertyChange(Human.Human.PropertyType type, Operate operate, params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }

            if (!(args[0] is int humanId) || !(args[1] is int value))
            {
                Debug.LogError($"参数不为int");
                return;
            }

            var human = HumanManager.Instance.GetHuman(humanId);
            if (human == null)
            {
                Debug.LogError($"无人物 {humanId}");
                return;
            }

            var result = value;
            if (operate != Operate.Set)
            {
                result = human.GetProperty(type) + value * (int) operate;
            }
            
            human.SetProperty(type, result);
            
            UIManager.Instance.panelNPCInfo.GetComponent<NPCInfoMono>().SwitchNpcInfo(human);
        }

        private static void HumanPropertyLock(Human.Human.PropertyType type, bool isLock, params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }
            
            if (!(args[0] is int humanId))
            {
                
                return;
            }
            
            var human = HumanManager.Instance.GetHuman(humanId);

            if (human == null)
            {
                Debug.LogError($"无人物 {humanId}");
                return;
            }
            
            human.SetLock(type, isLock);
        }
        
        private static void HumanWashHeadStatus(bool isWashHead, params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }

            if (!(args[0] is int humanId))
            {
                Debug.LogError($"参数不为int");
                return;
            }

            var human = HumanManager.Instance.GetHuman(humanId);

            if (human == null)
            {
                Debug.LogError($"无人物 {humanId}");
                return;
            }
            
            human.SetWashHead(isWashHead);
        }

        private static void HumanEventRoll(bool isActive, params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }

            if (!(args[0] is int humanId))
            {
                Debug.LogError($"参数不为int");
                return;
            }

            NPCManager.SetEventCycleActive(humanId, isActive);
        }

        private static void HumanDeath(params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }

            if (!(args[0] is int humanId))
            {
                Debug.LogError($"参数不为int");
                return;
            }

            var human = HumanManager.Instance.GetHuman(humanId);
            if (human == null)
            {
                Debug.LogError($"无人物 {humanId}");
                return;
            }
            
            human.Death();
        }

        private static void HumanGetCat(params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }
            var human = GetHuman(args[0]);
            if (human == null)
            {
                Debug.LogError($"无人物 {(int) args[0]}");
                return;
            }
            
            if (!(args[1] is int catId))
            {
                Debug.LogError($"参数不为int");
                return;
            }
            
            human.SetCat(catId);
        }

        private static void HumanLoseCat(params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }

            var human = GetHuman(args[0]);
            human?.SetCat(0);
        }

        private static void HumanImageChange(params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }

            var human = GetHuman(args[0]);
            if (human == null || !(args[1] is string image))
            {
                return;
            }

            human.SetImage(image);
        }

        private static bool CheckArgs(int count, params object[] args)
        {
            if (args == null || args.Length < count)
            {
                Debug.LogError($"参数长度异常！！！");
                return false;
            }

            return true;
        }

        private static Human.Human GetHuman(object obj)
        {
            if (!(obj is int humanId))
            {
                Debug.LogError($"参数不为int");
                return null;
            }

            return HumanManager.Instance.GetHuman(humanId);
        }

        private static void CatPropertyChange(Cat.CatPropertyType type, Operate operate, params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }

            var cat = GetCat(args[0]);
            if (cat == null || !(args[1] is int value))
            {
                return;
            }

            var result = value;
            if (operate != Operate.Set)
            {
                result = cat.GetProperty(type) + (int) operate * value;
            }
            
            cat.SetProperty(type, result);
        }

        private static void CatStatusChange(bool isAdd, params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }

            var cat = GetCat(args[0]);
            if (cat == null || !(args[1] is int textId))
            {
                return;
            }

            var text = LanguageLoader.Instance.FindLanguageItem(textId.ToString());
            if (isAdd)
            {
                cat.AddTag(text.Value);
            }
            else
            {
                cat.RemoveTag(text.Value);
            }
        }

        private static void CatImageChange(params object[] args)
        {
            if (!CheckArgs(2, args))
            {
                return;
            }

            var cat = GetCat(args[0]);
            if (cat == null || !(args[1] is int image))
            {
                return;
            }
            
            cat.SetImage(image.ToString());
        }

        private static Cat GetCat(object obj)
        {
            if (!(obj is int catId))
            {
                Debug.LogError($"参数不为int");
                return null;
            }

            return CatManager.Instance.GetCat(catId);
        }

        private static void SetStoryProcess(params object[] args)
        {
            if (!CheckArgs(1, args))
            {
                return;
            }

            if (args[0] is int value)
            {
                PlayerModel.Instance.StoryProgress = value;
            }
        }

        private static void ShowHuman(params object[] args)
        {
            var humanId = (int) args[0];
            if(!NPCManager.NPCs.TryGetValue(humanId, out var npcMono))
            {
                return;
            }
            
            npcMono.Show();
        }

        private static void ShowRelationLine(params object[] args)
        {
            var lineId = (int) args[0];
            var vineMono = VineManager.GetVineFromID(lineId);
            if (vineMono == null)
            {
                return;
            }

            vineMono.Show();
        }

        private static void SetRelation(params object[] args)
        {
            var lineId = (int) args[0];
            var vineMono = VineManager.GetVineFromID(lineId);
            if (vineMono == null)
            {
                return;
            }
            
            vineMono.SetText((int) args[1]);
        }
    }
}