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
        private int step = 20;

        private long timeStamp; 
        
        public void Init()
        {
            // 初始化时间戳
            System.DateTime configTime =
                TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(ConstValue.YEAR, ConstValue.MONTH,
                    ConstValue.DAY));
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
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
            System.DateTime time = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return time.AddSeconds(timeStamp);
        }
    }
}