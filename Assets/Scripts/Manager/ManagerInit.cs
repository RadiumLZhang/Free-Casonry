using System;
using UnityEngine;

namespace Manager
{
    public class ManagerInit : MonoBehaviour
    {
        public void Awake()
        {
            TimeTickerManager.Instance.Awake();
        }
    }
}