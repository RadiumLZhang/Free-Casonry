using System;
using UnityEngine;

namespace Manager
{
    public class ManagerInit : MonoBehaviour
    {
        public void Start()
        {
            TimeTickerManager.Instance.Init();
            TimeManager.Instance.Init();
            EventManager.Instance.Init();
            
            //AddTicker();
        }

        public void OnDestroy()
        {
            TimeManager.Destroy();
            TimeTickerManager.Destroy();
        }

        // 初始化挂载的Ticker
        private void AddTicker()
        {
            TimeTickerManager.Instance.AddLastingEventByStep(
                transform.GetComponent<GameView>().RefreshScrollSpecialEvent,
                1,
                10,
                0,
                null);
        }
    }
}