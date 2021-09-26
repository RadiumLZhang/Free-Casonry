using System;
using System.Collections.Generic;
using System.Timers;
using Logic;
using UnityEngine;
using Object = System.Object;


public enum TICKER_SPEED_ENUM : int
{
    STOP = 0,
    NORMAL = 1,
    FAST = 2
}
public class TimeTickerManager : BaseModel<TimeTickerManager>
{
    public delegate void CallBack();

    public delegate bool Condition();
    
    /********************************* 接口 ***********************************************/

    /*
     * 暂停
     */
    public void StopTick()
    {
        speed = (int) TICKER_SPEED_ENUM.STOP;
    }

    /*
     * 开始
     */
    public void StartTick()
    {
        speed = (int) TICKER_SPEED_ENUM.NORMAL;
    }

    /*
     * 自定义速度
     * @param: speedEnum        速度枚举值
     */
    public void StartTickWithSpeed(TICKER_SPEED_ENUM speedEnum)
    {
        speed = (int) speedEnum;
    }

    /**
     * 获取当前速度
     */
    public TICKER_SPEED_ENUM GetSpeed()
    {
        return (TICKER_SPEED_ENUM)speed;
    }

    /*
     * 添加事件
     * @param: callBack     回调函数
     * @param: delaySecond  经过x秒后触发
     */
    public int AddEvent(CallBack callBack, int delaySecond)
    {
        int frame = delaySecond * stepPerSecond + frameIndex;
        EventItem eventItem = new EventItem(callBack, frame);
        insertEvent(eventList, eventItem);
        return 0;
    }

    /*
     * 添加等待事件
     * @param: condition        生效判定，需要返回布尔值(会在每次tick时候调用判定)
     * @param: callBack         生效回调函数
     * @param: beginWaitSecond  经过x秒后开始等待
     * @param: destroySecond    经过x秒后不再等待，传入负数表示不会销毁
     * @param: destroyCallBack  未生效回调
     */
    public int AddWaitingEvent(Condition condition, CallBack callBack, int beginWaitSecond,
        int destroySecond, CallBack destroyCallBack)
    {
        int frame = beginWaitSecond * stepPerSecond + frameIndex;
        int destroyFrame = destroySecond * stepPerSecond + frameIndex;
        WaitingEventItem waitingEventItem = new WaitingEventItem(callBack, frame, condition, destroyFrame, destroyCallBack);
        insertEvent(waitingList, waitingEventItem);
        return 0;
    }

    /*
     * 添加持续性事件
     * @param: callBack         持续事件回调函数
     * @param: delaySecond      经过x秒后开始执行
     * @param: intervalSecond   执行间隔x秒，要是非负数，起码都会间隔一帧
     * @param: finishSecond     经过x秒后不再持续，配置0或负数表示会一直执行不会结束
     * @param: finishCallBack   结束回调
     */
    public int AddLastingEvent(CallBack callBack, int delaySecond, int intervalSecond,
        int finishSecond, CallBack finishCallBack)
    {
        int frame = delaySecond * stepPerSecond + frameIndex;
        int step = intervalSecond * stepPerSecond;
        int finishFrame = finishSecond > 0 ? finishSecond * stepPerSecond + frameIndex : 0;
        LastingEventItem lastingEventItem = new LastingEventItem(callBack, frame, step, finishCallBack, finishFrame);
        insertEvent(lastingList, lastingEventItem);
        return 0;
    }

    /*
     * 每一帧都会执行的事件
     * @param: callBack         持续事件回调函数
     * @param: delaySecond      经过x秒后开始执行
     * @param: finishSecond     经过x秒后不再持续
     * @param: finishCallBack   结束回调
     */
    public int onUpdate(CallBack callBack, int delaySecond,
    int finishSecond, CallBack finishCallBack)
    {
        return AddLastingEvent(callBack, delaySecond, 0, finishSecond, finishCallBack);
    }
    
    
    
    /************************************** 实现 ***************************************************/
    
    // 事件类
    class EventItem
    {
        public CallBack callBack;
        public int frame;

        public EventItem(CallBack callBack, int frame)
        {
            this.callBack = callBack;
            this.frame = frame;
        }
    }

    // 延时触发的事件类
    class WaitingEventItem : EventItem
    {
        public int destroyFrame;
        public Condition condition;
        public CallBack destroyCallBack;
        public WaitingEventItem(CallBack callBack, int frame,
            Condition condition, int destroyFrame, CallBack destroyCallBack) : base(callBack, frame)
        {
            this.destroyFrame = destroyFrame;
            this.condition = condition;
            this.destroyCallBack = destroyCallBack;
        }
    }
    
    // 持续性事件
    class LastingEventItem : EventItem
    {
        public int step;
        public CallBack finishCallBack;
        public int finishFrame;

        public LastingEventItem(CallBack callBack, int frame,
            int step, CallBack finishCallBack, int finishFrame) : base(callBack, frame)
        {
            this.step = step;
            this.finishCallBack = finishCallBack;
            this.finishFrame = finishFrame;
        }
    }
    

    private int speed;                           //	当前速率
    private int stepPerSecond;                   //	每秒步长
    private int frameIndex;                      //	当前帧
    private List<EventItem> eventList;
    private List<WaitingEventItem> waitingList;
    private List<LastingEventItem> lastingList;
    
    private System.Timers.Timer timer;
    

    // 初始化
    public void Init()
    {
        speed = (int) TICKER_SPEED_ENUM.NORMAL;
        stepPerSecond = 20;
        frameIndex = 0;
        eventList = new List<EventItem>();
        waitingList = new List<WaitingEventItem>();
        lastingList = new List<LastingEventItem>();
        timer = new Timer(1000 / stepPerSecond);
        timer.Elapsed += Ontick;
        timer.AutoReset = true;
        timer.Enabled = true;
        //test();   --用来测试的test函数，可以打开看看控制台效果
    }
    
    private void Ontick(Object source, ElapsedEventArgs e)
    {
        for (int i = 0; i < speed; i++)
        {
            frameIndex++;
            eventHandle();
            waitingHandle();
            lastingHandle();
        }
    }

    // 以生效帧升序插入
    private void insertEvent<T>(List<T> eventList, T eventItem) where T : EventItem
    {
        if (eventList.Count == 0)
        {
            eventList.Add(eventItem);
        }
        else
        {
            int index = 0;
            for (int i = 0; i < eventList.Count; i++)
            {
                if (eventList[i].frame > eventItem.frame)
                {
                    index = i;
                    break;
                }
            }
            eventList.Insert(index, eventItem);
        }
    }

    private void eventHandle()
    {
        if (eventList.Count == 0)
        {
            return;
        }
        while (eventList.Count > 0)
        {
            if (eventList[0].frame <= frameIndex)
            {
                if (eventList[0].callBack != null)
                {
                    eventList[0].callBack.Invoke();
                }
                eventList.RemoveAt(0);
            }
            else
            {
                break;
            }
        }
    }

    private void waitingHandle()
    {
        if (waitingList.Count == 0)
        {
            return;
        }
        int index = 0;
        while (waitingList.Count > 0)
        {
            WaitingEventItem waitingEventItem = waitingList[index];
            if (waitingEventItem.frame <= frameIndex)
            {
                if (waitingEventItem.condition.Invoke())
                {
                    waitingList.RemoveAt(index);
                    if (waitingEventItem.callBack != null)
                    {
                        waitingEventItem.callBack.Invoke();
                    }
                    continue;
                }

                if (waitingEventItem.destroyFrame >= 0 && waitingEventItem.destroyFrame <= frameIndex)
                {
                    waitingList.RemoveAt(index);
                    if (waitingEventItem.destroyCallBack != null)
                    {
                        waitingEventItem.destroyCallBack.Invoke();
                    }
                }
                index++;
            }
            else
            {
                break;
            }
        }

    }

    private void lastingHandle()
    {
        if (lastingList.Count == 0)
        {
            return;
        }

        int index = 0;
        while (index < lastingList.Count)
        {
            LastingEventItem lastingEventItem = lastingList[index];
            // 使用非正数的结束帧表示不会停止
            if (lastingEventItem.finishFrame <= 0 || lastingEventItem.finishFrame > frameIndex)
            {
                if (lastingEventItem.frame <= frameIndex)
                {
                    lastingEventItem.callBack.Invoke();
                    lastingEventItem.frame += lastingEventItem.step;
                }
                index++;
            }
            else
            {
                lastingList.RemoveAt(index);
                if (lastingEventItem.finishCallBack != null)
                {
                    lastingEventItem.finishCallBack.Invoke();
                }
            }
        }
    }

    /************************************ 自己测试用的 **************************************************/
    public int test()
    {
        AddEvent(() =>
        {
            Console.WriteLine("TimeTickerManager Awake");
        }, 0);
        int id = 1;
        AddLastingEvent(
            () =>
            {
                Console.WriteLine("id = " + id);
                id++;
            }, 0, 1, 10,
            () =>
            {
                Console.WriteLine("Finish Exist");
            });
        Console.ReadLine();
        return 0;
    }
}
