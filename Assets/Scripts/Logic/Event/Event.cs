using Event;
using UnityEngine;

namespace Logic.Event
{
    public class Event
    {
        public long ID { get; private set; }

        public EventInfoConfig.Types.EventItemConfig Config { get; private set; }

        public string Name => Config?.Name;

        public int Type => Config?.Type ?? 0;

        public string Image => Config?.Image;

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

        public Event(long id)
        {
            ID = id;
            Config = EventInfoConfigLoader.Instance.FindEventItemConfig(id);
            if (Config == null)
            {
                Debug.LogError($"Invalid EventID:{id}");
            }
        }


        public bool CanExecute()
        {
            //todo 判断前提条件
            //Config.Preconditions;
            //判断执行次数
            if (ExecuteCount >= Config.RepeatTime)
            {
                return false;
            }
            
            return true;
        }

        public bool CanGenerate()
        {
            //todo 判断生成条件组
            //Config.GenerateConditions;
            return true;
        }

        public bool IsDestroyed()
        {
            //todo 判断销毁条件组
            // Config.DestroyConditions;
            return true;
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
    }
}