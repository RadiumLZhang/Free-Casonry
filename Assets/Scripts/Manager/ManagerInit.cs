using System;
using Logic.Condition;
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
            ConditionUtils.Init();
            HumanManager.Instance.Init();
            SaveManager.Instance.Init();

            StartCoroutine(TimeTickerManager.Instance.Loop());

            AddTicker();
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
                -1,
                transform.GetComponent<GameView>().RefreshScrollSpecialEvent,
                0,
                10,
                0,
                null);
        }
    }
}