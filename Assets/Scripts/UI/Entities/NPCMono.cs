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
    private UITransitionEffect m_imgWashHeadEffect;
    
    private Animation m_redPointAnimation;
    private Transform backgroundButton;
    
    public Image imageCat;
    private UITransitionEffect m_imgCatEffect;
        
    private UITransitionEffect effect;
    private Image imageHuman;
    public AudioSource adplayer;
    
    private const string ImportantPointIn = "ImportantPointIn";
    
    
    
    public List<NPCEventMono> eventcols = new List<NPCEventMono>();
    
    private void Awake()
    {
        adplayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        imageHuman = transform.GetComponent<Image>();
        eventCycle = transform.Find("EventCycle/root").gameObject;
        selfRect = transform.GetComponent<RectTransform>();
        contentRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        viewRect = GameObject.Find("ScrollRelationship").transform.Find("Viewport").GetComponent<RectTransform>();
        scrollRect = GameObject.Find("ScrollRelationship").GetComponent<ScrollRect>();
        manager = GameObject.Find("ScrollRelationship").GetComponent<NPCManager>();
        backgroundButton = transform.Find("EventCycle/root/Button");
        
        npcRedPoint = transform.Find("animationRoot/RedPoint").gameObject;
        m_redPointAnimation = transform.Find("animationRoot").GetComponent<Animation>();
        
        imageWashHead = transform.Find("ImageWashHead").gameObject;
        m_imgWashHeadEffect = imageWashHead.GetComponent<UITransitionEffect>();

        imageCat = transform.Find("ImageCat").GetComponent<Image>();
        m_imgCatEffect = imageCat.GetComponent<UITransitionEffect>();

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
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "人物信息");
        adplayer.clip = m_clip;
        adplayer.Play();
        
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
            UIManager.Instance.gameView.GetComponent<GameView>().ButtonCloseRelationship_OnClickWithoutAudio();
            manager.SetOpenedNPC(gameObject);
        }
        
        if (eventCycle.activeSelf)
        {
            CloseEventCycle();
        }
        else
        {
            OpenEventCycle();
        }
        
        // eventCycle.SetActive(!eventCycle.activeSelf);
        
    }

    public void OpenEventCycle()
    {
        //用button的状态当做是否打开的判断
        if (backgroundButton.gameObject.activeSelf)
        {
            return;
        }
        backgroundButton.gameObject.SetActive(true);
        var inAni = m_animation["EventCycleShow"];
        if (m_animation.IsPlaying(inAni.name) && inAni.speed == 1)
        {
            return;
        }
        
        inAni.speed = 1;
        inAni.normalizedTime = 0;
        
        inAni.enabled = false;
        m_animation.Sample();
        m_animation.Play("EventCycleShow");
        
        UIManager.Instance.panelNPCInfo.OpenNpcInfo();
    }
    
    public void CloseEventCycle()
    {
        //用button的状态当做是否打开的判断
        if (!backgroundButton.gameObject.activeSelf)
        {
            return;
        }
        
        backgroundButton.gameObject.SetActive(false);
        var inAni = m_animation["EventCycleShow"];
        if (m_animation.IsPlaying(inAni.name) && inAni.speed == -1)
        {
            return;
        }
        
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

    private string m_catName;
    
    public void SetCatImage(string name)
    {
        if (m_catName == name && imageCat.gameObject.activeSelf)
        {
            return;
        }

        m_catName = name;
        
        imageCat.sprite = Resources.Load<Sprite>("Sprites/Portraits/" + name);
        imageCat.gameObject.SetActive(true);
        m_imgCatEffect.Show();
        imageCat.SetAllDirty();
    }

    public void RemoveCatImage()
    {
        imageCat.gameObject.SetActive(false);
    }

    public void SetWashHead(bool isWashHead)
    {
        if (isWashHead == imageWashHead.activeSelf)
        {
            return;
        }
        
        imageWashHead.SetActive(isWashHead);
        if (isWashHead)
        {
            m_imgWashHeadEffect.Show();
        }
    }

    public void RefreshEventCycle()
    {
        // by default
        if (eventcols.Count == 0)
        {
            return;
        }
        
        var list = EventManager.Instance.GetRoleEventList(id);
        int i = 0;

        var flag = false;
        for (; i < eventcols.Count; i++)
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
        if (gameObject.activeSelf)
        {
            return;
        }
        
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
