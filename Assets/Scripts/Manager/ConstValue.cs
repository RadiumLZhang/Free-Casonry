namespace Manager
{
    public class ConstValue
    {
        // 游戏开始年份
        public const int YEAR = 2021;
        
        // 游戏开始月份
        public const int MONTH = 7;
        
        // 游戏开始日期
        public const int DAY = 13;
        
        // 现实中的一秒对应游戏中的x秒, TimeManager当中使用
        public const int TIME_STEP = 600;
        
        // TimeTickerManager每秒执行多少次
        public const int TIME_TICKER_STEP = 20;

        // 默认成功的条件ID
        public const int CONDITION_TRUE_ID = 0;
        // 默认失败的条件ID
        public const int CONDITION_FALSE_ID = -1;
    }
}