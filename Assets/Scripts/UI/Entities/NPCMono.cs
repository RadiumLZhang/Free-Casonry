using System;
using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
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
    public GameObject imageWashHead;
    private Animation m_redPointAnimation;
    private Transform backgroundButton;
    public Image imageCat;
    private UITransitionEffect effect;
    private Image imageHuman;

    private const string ImportantPointIn = "ImportantPointIn";
    
    public List<NPCEventMono> eventcols = new List<NPCEventMono>();
    
    private void Start()
    {
        imageHuman = transform.GetComponent<Image>();
        eventCycle = transform.Find("EventCycle/root").gameObject;
        selfRect = transform.GetComponent<RectTransform>();
        contentRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        viewRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").GetComponent<RectTransform>();
        scrollRect = GameObject.Find("ScrollRelationship").GetComponent<ScrollRect>();
        manager = GameObject.Find("ScrollRelationship").GetComponent<NPCManager>();
        backgroundButton = transform.Find("EventCycle/root/Button");
        npcRedPoint = transform.Find("animationRoot/RedPoint").gameObject;
        imageWashHead = transform.Find("ImageWashHead").gameObject;
        m_redPointAnimation = transform.Find("animationRoot").GetComponent<Animation>();
        imageCat = transform.Find("ImageCat").GetComponent<Image>();

        effect = transform.GetComponent<UITransitionEffect>();
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
            //manager.StartNPCLerp((selfRect.anchoredPosition.x + contentX / 2.0f) / contentX,(selfRect.anchoredPosition.y + contentY / 2.0f) / contentY - 0.03f);
            UIManager.Instance.gameView.GetComponent<GameView>().oldRelationshipPos = new Vector2(
                (selfRect.anchoredPosition.x + contentX / 2.0f) / contentX,
                (selfRect.anchoredPosition.y + contentY / 2.0f) / contentY - 0.07f);
            UIManager.Instance.gameView.GetComponent<GameView>().ButtonCloseRelationship_OnClick();
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
        backgroundButton.gameObject.SetActive(true);
        var inAni = m_animation["EventCycleShow"];
        inAni.speed = 1;
        inAni.normalizedTime = 0;
        
        inAni.enabled = false;
        m_animation.Sample();
        m_animation.Play("EventCycleShow");
        
        UIManager.Instance.panelNPCInfo.OpenNpcInfo();
    }
    
    public void CloseEventCycle()
    {
        backgroundButton.gameObject.SetActive(false);
        var inAni = m_animation["EventCycleShow"];
        inAni.speed = -1;
        inAni.normalizedTime = 1;
        
        inAni.enabled = false;
        m_animation.Sample();
        m_animation.Play("EventCycleShow");
        
        UIManager.Instance.panelNPCInfo.CloseNpcInfo();
    }

    public void ClostBtn_OnClick()
    {
        CloseEventCycle();
        UIManager.Instance.panelNPCInfo.CloseNpcInfo();
        manager.currentOpenedNPC = null;
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
        if (!bIsSwitchToShown)
        {
            return;
        }
        var inAni = m_redPointAnimation[ImportantPointIn];
        inAni.speed = 1;
        inAni.normalizedTime = 0;

        inAni.enabled = false;
        m_redPointAnimation.Sample();
        m_redPointAnimation.Play(inAni.name);
    }

    public void RefreshEventCycle()
    {
        // by default
        
        var list = EventManager.Instance.GetRoleEventList(id);
        int i = 0;

        var flag = false;
        for (; i < 5; i++)
        {
            eventcols[i].EmptyCol();
        }

        var n = Math.Min(list.Count, 5);
        
        for (i = 0; i < list.Count; i++)
        {
            var tempEvent = list[i];
            eventcols[i].InitWithID(list[i].ID);
            flag |= tempEvent.IsImportant;
        }
        
        SwitchNPCRedPoint(flag);
    }

    public void Show()
    {
        
        gameObject.SetActive(true);
        effect.Show();
    }

    public void Hide()
    {
        effect.Hide();
    }

    public void Death()
    {
        imageHuman.sprite = Resources.Load<Sprite>("Sprites/Main/NPCs/女仆死亡");
    }
}
