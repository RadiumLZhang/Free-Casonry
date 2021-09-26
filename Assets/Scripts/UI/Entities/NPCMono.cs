using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMono : MonoBehaviour
{
    private GameObject eventCycle;
    private RectTransform selfRect;
    private RectTransform contentRect;
    private RectTransform viewRect;
    private ScrollRect scrollRect;
    private NPCManager manager;
    private void Start()
    {
        eventCycle = transform.Find("EventCycle").gameObject;
        selfRect = transform.GetComponent<RectTransform>();
        contentRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        viewRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").GetComponent<RectTransform>();
        scrollRect = GameObject.Find("ScrollRelationship").GetComponent<ScrollRect>();
        manager = GameObject.Find("ScrollRelationship").GetComponent<NPCManager>();
    }

    public void NPC_OnClick()
    {
        if (!eventCycle.activeSelf)
        {
            if (manager.currentOpenedNPC)
                manager.currentOpenedNPC.transform.Find("EventCycle").gameObject.SetActive(false);
            float contentX = contentRect.sizeDelta.x - viewRect.sizeDelta.x;
            float contentY = contentRect.sizeDelta.y - viewRect.sizeDelta.y;
            manager.StartNPCLerp((selfRect.anchoredPosition.x + contentX / 2.0f) / contentX,(selfRect.anchoredPosition.y + contentY / 2.0f) / contentY - 0.03f);
            manager.SetOpenedNPC(gameObject);
        }
        eventCycle.SetActive(!eventCycle.activeSelf);
    }

    public void ClostBtn_OnClick()
    {
        eventCycle.SetActive(false);
    }
}
