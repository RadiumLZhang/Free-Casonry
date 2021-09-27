using System;
using Logic;
using UnityEngine;

namespace Manager
{
    // 全局时间管理（左上角那个年月日时分秒
    public class TimeManager : BaseModel<TimeManager>
    {
        /********************************* 接口 ***********************************************/
        
        /*
         * 获取当前时间戳
         */
        public long GetTimeStamp()
        {
            return timeStamp;
        }

        /*
         * 根据月-日-时-分转换为时间戳
         * 为输入的参数一律按1月1日0：0处理
         */
        public long TimeToStamp(int? month, int? day, int? hour, int? minute)
        {
            System.DateTime input = new System.DateTime(ConstValue.YEAR, month ?? 1, day ?? 1, hour ?? 0, minute ?? 0, 0);
            return (long)(input - startTime).TotalSeconds;
        }

        /*
         * 根据月-日-时-分设置当前时间
         * 为输入的参数一律按1月1日0：0处理
         * 时间只允许向后跳
         */
        public void SetTime(int? month, int? day, int? hour, int? minute)
        {
            System.DateTime input = new System.DateTime(ConstValue.YEAR, month ?? 1, day ?? 1, hour ?? 0, minute ?? 0, 0);
            long newTimeStamp = (long)(input - startTime).TotalSeconds;
            if (newTimeStamp >= timeStamp)
            {
                TimeTickerManager.Instance.AddTime((int) (newTimeStamp - timeStamp));
                timeStamp = newTimeStamp;
            }
        }

        /**
         * 获取当前时间
         */
        public System.DateTime GetTime()
        {
            return Build();
        }
        
        /*
         * 获取当前时间年份
         */
        public int GetYear()
        {
            return Build().Year;
        }
        
        /*
         * 获取当前时间月份
         */
        public int GetMonth()
        {
            return Build().Month;
        }
        
        /*
         * 获取当前时间日期
         */
        public int GetDay()
        {
            return Build().Day;
        }
        
        /*
         * 获取当前时间小时
         */
        public int GetHour()
        {
            return Build().Hour;
        }
        
        /*
         * 获取当前时间分钟
         */
        public int GetMinute()
        {
            return Build().Minute;
        }
        
        /*
         * 获取当前时间秒
         */
        public int GetSecond()
        {
            return Build().Second;
        }
        
        /************************************** 实现 ******************************************/

        // 现实中的一秒对应游戏中的x秒
        private int step = ConstValue.TIME_STEP;

        private long timeStamp; 
        
        private System.DateTime startTime = new System.DateTime(1970, 1, 1);
        
        public void Init()
        {
            // 初始化时间戳
            System.DateTime configTime =new System.DateTime(ConstValue.YEAR, ConstValue.MONTH,
                    ConstValue.DAY);
            timeStamp = (long)(configTime - startTime).TotalSeconds;
            
            // 添加ticker
            TimeTickerManager.Instance.AddLastingEvent(
                () =>
                {
                    timeStamp += step;
                },
                0,
                1,
                0,
                null
            );
            
            // TEST
            
            /*TimeTickerManager.Instance.AddLastingEvent(
                () =>
                {
                    Debug.Log($"Year: {GetYear()}, Month: { GetMonth()}, Day: {GetDay()}, Hour: {GetHour()}, Minute: {GetMinute()}, Second: {GetSecond()}");
                },
                0,
                1,
                0,
                null
            );*/
        }

        private System.DateTime Build()
        {
            System.DateTime time = startTime.AddSeconds(timeStamp);
            return time;
        }
    }
}