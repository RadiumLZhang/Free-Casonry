using System;
using BaseCondition;
using Condition;
using EventHandler;
using Google.Protobuf.Collections;
using Logic.Event;
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
            
            var parasCount = config.Paras.Count;
            var list = new object[parasCount];
            for (int i = 0; i < parasCount; i++)
            {
                list[i] = config.Paras[i];
            }
            
            // confition id 是基础id，parameter是参数
            return Check(config.BaseConditionId, list);
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
                case 4105:// 目标人物X，心防至少为Y
                    return CheckHumanDefenceGreaterEqual(args);
                case 4106: // 目标人物X，心防为Y
                    return CheckHumanDefenceEqual(args);
                case 4107: // 目标人物X，心防低于Y
                    return CheckHumanDefenceGreaterEqual(args) == false;
                    // case 4108: // 目标人物X，亲密度至少为Y
                    //     return CheckHumanIntimacyGreaterEqual(args);
                    // case 4109: //目标人物X，亲密度为Y
                    //     return CheckHumanIntimacyEqual(args);
                
                case 4111:  //  目标人物X，拥有洗脑状态
                    return CheckHumanIsWashHead((int) args[0]);
                case 4112:  //  目标人物X，未拥有洗脑状态
                    return CheckHumanIsWashHead((int) args[0]) == false;
                case 4113:  //  目标人物X，拥有任意猫咪
                    return CheckHumanOwnCat((int) args[0]);
                case 4114:  //  目标人物X，拥有猫咪Y
                    return CheckHumanOwnCat((int) args[0], (int) args[1]);
                case 4115:  //  目标人物X，未拥有猫咪
                    return CheckHumanOwnCat((int) args[0]) == false;
                case 4116:  //  目标人物X，允许养猫
                    return CheckHumanCanOwnCat((int) args[0]);
                case 4117:  //  目标人物X，禁止养猫
                    return CheckHumanCanOwnCat((int) args[0]) == false;
                    
                case 4200:  //  目标猫咪X，密谋等级至少为Y
                    return CheckCatConspiracyGreaterEqual((int) args[0], (int) args[1]);
                case 4201:  //  目标猫咪X，密谋等级等于Y
                    return CheckCatConspiracyEqual((int) args[0], (int) args[1]);
                case 4202:  //  目标猫咪X，密谋等级低于Y
                    return CheckCatConspiracyGreaterEqual((int) args[0], (int) args[1]) == false;
                case 4203:  //  目标猫咪X，刺探等级至少为Y
                    return CheckCatScoutGreaterEqual((int) args[0], (int) args[1]);
                case 4204:  //  目标猫咪X，刺探等级等于Y
                    return CheckCatScoutEqual((int) args[0], (int) args[1]);
                case 4205:  //  目标猫咪X，刺探等级低于Y
                    return CheckCatScoutGreaterEqual((int) args[0], (int) args[1]) == false;
                case 4206:  //  目标猫咪X，交流等级至少为Y
                    return CheckCatCommunicationGreaterEqual((int) args[0], (int) args[1]);
                case 4207:  //  目标猫咪X，交流等级等于Y
                    return CheckCatCommunicationEqual((int) args[0], (int) args[1]);
                case 4208:  //  目标猫咪X，交流等级低于Y
                    return CheckCatCommunicationGreaterEqual((int) args[0], (int) args[1]) == false;
                  
                case 4300:  //  人物X已出现在关系网中
                    return CheckNPCDisplay((int) args[0]);
                case 4301:  //  人物X未出现在关系网中
                    return CheckNPCDisplay((int) args[0]) == false;
                case 4302:  //  人物关系线X已出现在关系网中
                    return CheckVineIsDisplay((int) args[0]);
                case 4303:  //  人物关系线X未出现在关系网中
                    return CheckVineIsDisplay((int) args[0]) == false;
                
                case 4400: // 拥有事件记录X
                    return CheckHasFlag((int) args[0]);
                case 4401: // 拥有事件记录X或Y
                    return CheckHasFlag((int) args[0]) || CheckHasFlag((int) args[1]);
                case 4402: // 拥有事件记录X和Y
                    return CheckHasFlag((int) args[0]) && CheckHasFlag((int) args[1]);
                case 4403:  // 该事件的突发事件当中选择了第X项
                    return CheckEmergencyChoice(args);
                
                
                case 4500:  //  故事进度为X
                    return CheckStoryProgressGreaterEqual((int) args[0]);
                case 4501:  //  故事进度大于等于X
                    return CheckStoryProgressEqual((int) args[0]);
                case 4502:  //  故事进度小于等于X
                    return CheckStoryProgressLessEqual((int) args[0]);
                case 4503:  // 拥有至多X点人类货币
                    return CheckPlayerPropertyLessOrEqual((int) args[0], PlayerModel.ResourceType.Money);
                case 4504:  //  拥有至多X点猫咪影响力
                    return CheckPlayerPropertyLessOrEqual((int) args[0], PlayerModel.ResourceType.Influence);
                case 4505:  //  拥有至多X点猫咪隐匿度
                    return CheckPlayerPropertyLessOrEqual((int) args[0], PlayerModel.ResourceType.Hidency);
                case 4506:  //  执行任务的猫咪编号为X
                    return CheckCatId((int) args[0]);
            }

            return false;
        }

        private static bool CheckMoneyGreaterEqual(params object[] args)
        {
            return (int)args[0] <= PlayerModel.Instance.Money;
        }

        private static bool CheckCatInfluenceGreaterEqual(params object[] args)
        {
            return (int) args[0] <= PlayerModel.Instance.Influence;
        }

        private static bool CheckCatHidencyGreaterEqual(params object[] args)
        {
            return (int) args[0] <= PlayerModel.Instance.Hidency;
        }

        private static bool CheckTimeReach(params object[] args)
        {
            //TODO 缺时间接口
            var list = args;
            return TimeManager.Instance.GetTimeStamp() >=
                   TimeManager.Instance.TimeToStamp((int) list[0], (int) list[1], (int) list[2], (int) list[3]);
        }

        private static bool CheckEventHandlerCountGreaterEqual(params object[] args)
        {
            Debug.Log("基础条件4005：" + (int) args[0] + "  " + EventHandlerManager.Instance.GetHandlerCount());
            return (int)args[0] <= EventHandlerManager.Instance.GetHandlerCount();
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
             return HumanManager.Instance.GetHuman((int) args[0]).GetTags().Exists(l => l == (String) args[1]);
         }
         
         private static bool CheckHumanVisibilityGreaterEqual(params object[] args)
         {
             return HumanManager.Instance.GetHuman((int) args[0]).Visibility >= (int) args[1];
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

         private static bool CheckHasFlag(int flagId)
         {
             return PlayerModel.Instance.CheckRecord(flagId);
         }

         private static bool CheckEmergencyChoice(params object[] args)
         {
             Emergency tempEmergency = EmergencyManager.Instance.GetEmergencyByID((int)args[0]);
             return tempEmergency.GetChoice() == (int)args[1];
         }
         
         
         /*补充********************************************************************************************/

         private static bool CheckHumanDefenceGreaterEqual(params object[] args)
         {
             Human.Human human = HumanManager.Instance.GetHuman((int) args[0]);
             return human.Defence >= (int) args[1];
         }

         private static bool CheckHumanDefenceEqual(params object[] args)
         {
             Human.Human human = HumanManager.Instance.GetHuman((int) args[0]);
             return human.Defence == (int) args[1];
         }

         private static bool CheckPlayerPropertyLessOrEqual(int value, PlayerModel.ResourceType type)
         {
             return PlayerModel.Instance.GetResource(type) <= value;
         }

         private static bool CheckHumanIsWashHead(int humanId)
         {
             Human.Human human = HumanManager.Instance.GetHuman(humanId);
             return human.IsWashHead;
         }
         
         private static bool CheckHumanOwnCat(int humanId)
         {
             Human.Human human = HumanManager.Instance.GetHuman(humanId);
             return human.cat != null;
         }
         
         private static bool CheckHumanOwnCat(int humanId, int catId)
         {
             Human.Human human = HumanManager.Instance.GetHuman(humanId);
             Cat cat = CatManager.Instance.GetCat(catId);

             return cat != null && cat == human.cat;
         }

         private static bool CheckHumanCanOwnCat(int humanId)
         {
             Human.Human human = HumanManager.Instance.GetHuman(humanId);
             return human.CanRaiseCat();
         }

         private static bool CheckCatConspiracyGreaterEqual(int catId, int conspiracy)
         {
             Cat cat = CatManager.Instance.GetCat(catId);
             return cat.Conspiracy >= conspiracy;
         }
         
         private static bool CheckCatConspiracyEqual(int catId, int conspiracy)
         {
             Cat cat = CatManager.Instance.GetCat(catId);
             return cat.Conspiracy == conspiracy;
         }

         private static bool CheckCatScoutGreaterEqual(int catId, int scout)
         {
             Cat cat = CatManager.Instance.GetCat(catId);
             return cat.ScoutValue >= scout;
         }
         
         private static bool CheckCatScoutEqual(int catId, int scout)
         {
             Cat cat = CatManager.Instance.GetCat(catId);
             return cat.ScoutValue == scout;
         }

         private static bool CheckCatCommunicationGreaterEqual(int catId, int communication)
         {
             Cat cat = CatManager.Instance.GetCat(catId);
             return cat.Communication >= communication;
         }
         
         private static bool CheckCatCommunicationEqual(int catId, int communication)
         {
             Cat cat = CatManager.Instance.GetCat(catId);
             return cat.Communication == communication;
         }

         private static bool CheckStoryProgressGreaterEqual(int storyProgress)
         {
             return PlayerModel.Instance.StoryProgress >= storyProgress;
         }
         private static bool CheckStoryProgressEqual(int storyProgress)
         {
             return PlayerModel.Instance.StoryProgress == storyProgress;
         }
         private static bool CheckStoryProgressLessEqual(int storyProgress)
         {
             return PlayerModel.Instance.StoryProgress <= storyProgress;
         }

         private static bool CheckVineIsDisplay(int vineId)
         {
             return VineManager.CheckVine(vineId);
         }

         private static bool CheckNPCDisplay(int npcId)
         {
             // todo
             return false;
         }

         private static bool CheckCatId(long id)
         {
             var eventHandlerManager = EventHandlerManager.Instance;
             var curSelectCat = eventHandlerManager.GetCatByIndex(eventHandlerManager.CurSelectIndex);

             return curSelectCat.ID == id;
         }
    }
    
}