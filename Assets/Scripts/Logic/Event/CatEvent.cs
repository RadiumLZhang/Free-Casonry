using Event;
using Google.Protobuf.Collections;
using Logic.Condition;
using UnityEngine;

namespace Logic.Event
{
    public class CatEvent
    {
        public long ID { get; private set; }

        public EventInfoConfig.Types.EventItemConfig Config { get; private set; }

        public string Name => Config?.Name;

        public int Type => Config?.Type ?? 0;

        public string Image => Config?.Imageout;

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

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority => Config?.Priority ?? 0;

        public CatEvent(long id)
        {
            ID = id;
            Config = EventInfoConfigLoader.Instance.FindEventItemConfig(id);
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
            //判断执行次数
            if (ExecuteCount >= Config.RepeatTime)
            {
                return false;
            }
            
            return true;
        }

        public bool CanGenerate()
        {
            return CheckConditionGroup(Config.GenerateConditions);
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
        }

        public void Finish()
        {
            //todo 结算结果
            // Config.Result;
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
            foreach (var condition in Config.DestroyConditions)
            {
                bool success = true;
                foreach (var id in condition.Conditions)
                {
                    if (ConditionUtils.CheckCondition(id))
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