using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NPCMono : MonoBehaviour
{
    public GameObject eventCycle;
    public long id;
    private RectTransform selfRect;
    private RectTransform contentRect;
    private RectTransform viewRect;
    private ScrollRect scrollRect;
    private NPCManager manager;
    private EventTrigger m_trigger;
    private Animation m_animation;
    private GameObject npcRedPoint;
    private Animation m_redPointAnimation;

    private const string ImportantPointIn = "ImportantPointIn";
    
    public List<NPCEventMono> eventcols = new List<NPCEventMono>();
    
    private void Start()
    {
        eventCycle = transform.Find("EventCycle/root").gameObject;
        selfRect = transform.GetComponent<RectTransform>();
        contentRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        viewRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").GetComponent<RectTransform>();
        scrollRect = GameObject.Find("ScrollRelationship").GetComponent<ScrollRect>();
        manager = GameObject.Find("ScrollRelationship").GetComponent<NPCManager>();
        
        npcRedPoint = transform.Find("animationRoot/RedPoint").gameObject;
        m_redPointAnimation = npcRedPoint.GetComponent<Animation>();
        
        m_trigger = transform.GetComponent<EventTrigger>();
        m_animation = transform.Find("EventCycle").GetComponent<Animation>();
        m_animation.Stop();
        
        for (int i = 0; i < 5; i++)
        {
            eventcols.Add(transform.Find("EventCycle/root/NPCEvent" + i).GetComponent<NPCEventMono>());
        }
    }

    public void NPC_OnClick()
    {
        UIManager.Instance.SwitchNPCInfo(HumanManager.Instance.GetHuman(id));
        if (!eventCycle.activeSelf)
        {
            if (manager.currentOpenedNPC)
            {
                manager.currentOpenedNPC.GetComponent<NPCMono>().CloseEventCycle();
            }
            
            float contentX = contentRect.sizeDelta.x - viewRect.sizeDelta.x;
            float contentY = contentRect.sizeDelta.y - viewRect.sizeDelta.y;
            manager.StartNPCLerp((selfRect.anchoredPosition.x + contentX / 2.0f) / contentX,(selfRect.anchoredPosition.y + contentY / 2.0f) / contentY - 0.03f);
            manager.SetOpenedNPC(gameObject);
        }
        
        var inAni = m_animation["EventCycleShow"];
        if (eventCycle.activeSelf)
        {
            CloseEventCycle();
        }
        else
        {
            OpenEventCycle();
        }
        m_animation.Play("EventCycleShow");
        
        // eventCycle.SetActive(!eventCycle.activeSelf);
    }

    public void OpenEventCycle()
    {
        var inAni = m_animation["EventCycleShow"];
        inAni.speed = 1;
        inAni.normalizedTime = 0;
        
        inAni.enabled = false;
        m_animation.Sample();
        m_animation.Play("EventCycleShow");
    }
    
    public void CloseEventCycle()
    {
        var inAni = m_animation["EventCycleShow"];
        inAni.speed = -1;
        inAni.normalizedTime = 1;
        
        inAni.enabled = false;
        m_animation.Sample();
        m_animation.Play("EventCycleShow");
    }

    public void ClostBtn_OnClick()
    {
        CloseEventCycle();
    }

    public void SetEventTriggerActive(bool active)
    {
        m_trigger.enabled = active;
    }

    public void SwitchNPCRedPoint(bool bIsSwitchToShown)
    {
        if (npcRedPoint.activeSelf == bIsSwitchToShown)
        {
            return;
        }
        
        npcRedPoint.SetActive(bIsSwitchToShown);
        var inAni = m_redPointAnimation[ImportantPointIn];
        if (bIsSwitchToShown)
        {
            inAni.speed = 1;
            inAni.normalizedTime = 0;
        }
        else
        {
            inAni.speed = -1;
            inAni.normalizedTime = 1;
        }
        
        inAni.enabled = false;
        m_redPointAnimation.Sample();
        m_redPointAnimation.Play(ImportantPointIn);
    }

    public void RefreshEventCycle()
    {
        // by default
        SwitchNPCRedPoint(false);
        var list = EventManager.Instance.GetRoleEventList(id);
        int i = 0;
        
        for (; i < list.Count; i++)
        {
            var tempEvent = list[i];
            if (tempEvent.IsImportant)
            {
                SwitchNPCRedPoint(true);
            }
            eventcols[i].InitWithID(list[i].ID);
        }

        for (; i < 5; i++)
        {
            // todo 五个环中没有事件 @takiding
        }
    }
}
