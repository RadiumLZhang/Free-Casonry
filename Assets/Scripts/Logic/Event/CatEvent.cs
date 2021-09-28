using Event;
using Google.Protobuf.Collections;
using Logic.Condition;
using Logic.Effect;
using ResultEventInfo;
using UnityEngine;

namespace Logic.Event
{

    public enum EventStatus : int
    {
        Init = 0,
        Generated = 1,
        OnProcess = 2,
        Finished = 3,
        Destroyed = 4
    }
    public class CatEvent
    {
        public long ID { get; private set; }

        public EventInfoConfig.Types.EventItemConfig Config { get; private set; }

        public string Name => Config?.Name;

        public int Type => Config?.Type ?? 0;

        public string Imageout => Config?.Imageout;

        public long ConsumeTime => Config?.ConsumeTime ?? 0;

        public long ExpireTime => Config?.ExpireTime ?? 0;
        /// <summary>
        /// 人物id，非专有事件为0
        /// </summary>
        public long HumanId => Config?.ExpireTime ?? 0;

        /// <summary>
        /// 已执行次数
        /// </summary>
        public int ExecuteCount { get; private set; }

        /// <summary>
        /// 绑定的突发事件id, 0代表没有
        /// </summary>
        public long EmergencyId => Config?.EmergencyId ?? 0;

        public bool IsImportant => Config?.IsImportant == 1;

        public EventStatus Status { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority => Config?.Priority ?? 0;

        public CatEvent(long id)
        {
            ID = id;
            Config = EventInfoConfigLoader.Instance.FindEventItemConfig(id);
            Status = EventStatus.Init;
            if (Config == null)
            {
                Debug.LogError($"Invalid EventID:{id}");
            }
        }

        public long GetEmergencyId()
        {
            return EmergencyId;
        }

        public bool CanExecute()
        {
            foreach (var id in Config.Preconditions)
            {
                if (ConditionUtils.CheckCondition(id))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanGenerate()
        {
            //判断生成次数
            if (ExecuteCount >= Config.RepeatTime)
            {
                return false;
            }
            
            return CheckConditionGroup(Config.GenerateConditions);
        }

        public void Generate()
        {
            ExecuteCount++;
            Status = EventStatus.Generated;
        }

        public bool IsDestroyed()
        {
            return CheckConditionGroup(Config.DestroyConditions);
        }

        public void Execute()
        {
            //todo 执行效果、代价
            // Config.Effects;
            if (!CanExecute())
            {
                return;
            }
            ExecuteCount++;

            foreach (var VARIABLE in Config.Effects)
            {
                
            }
        }

        public void Finish()
        {
            //todo 结算结果
            // Config.Result;
            var resultId = 0L;
            foreach (var resultEvent in Config.Result)
            {
                var success = true;
                foreach (var condition in resultEvent.Conditions)
                {
                    if (!ConditionUtils.CheckCondition(condition))
                    {
                        success = false;
                        break;
                    }
                }

                if (success)
                {
                    resultId = resultEvent.EventId;
                    break;
                }
            }
            
            Status = EventStatus.Finished;

            if (resultId == 0)
            {
                Debug.Log("There is no suitable result");
                return;
            }

            var item = ResultEventInfoLoader.Instance.FindResultEventItem(resultId);
            foreach (var effect in item.Effects)
            {
                EffectUtils.ActivateEffect(effect);
            }
        }

        public void OutOfTimeResult()
        {
            //todo 结算超时效果
            // Config.ExpireEffect;
            //todo 超时record 不知道干嘛用
            // Config.OutOfTimeRecordId;
        }

        private bool CheckConditionGroup(RepeatedField<EventInfoConfig.Types.ConditionGroup> conditionGroup)
        {
            foreach (var condition in conditionGroup)
            {
                bool success = true;
                foreach (var id in condition.Conditions)
                {
                    if (!ConditionUtils.CheckCondition(id))
                    {
                        success = false;
                        break;
                    }
                }

                if (success)
                {
                    return true;
                }
            }
            return false;
        }
    }
}