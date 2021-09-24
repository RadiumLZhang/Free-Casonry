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
        }

        public void OnDestroy()
        {
            TimeManager.Destroy();
            TimeTickerManager.Destroy();
        }
    }
}