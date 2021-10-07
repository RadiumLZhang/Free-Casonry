using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using Logic.Event;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatColumnHandler : MonoBehaviour, IDropHandler
{
    private Logic.Event.CatEvent m_myCatEventInfo;
    private DragHandlerSpecialEvent droppedSpecialEvent;
    private DragHandlerNPCEvent droppedNPCEvent;
    private GameView gameView;
    private GameObject pointerDragCache;
    
    private Transform m_emergencyFlag;
    private Animation m_emergencyFlagAnimation;

    private Transform m_finishFlag;
    private Animation m_finishFlagAnimation;

    private Image m_eventImage;
    private RectTransform m_mask;


    private const string FlagAnimation = "FlagIn";
    
    public int index;
    private long myID = -1;

    //remainingTime维护剩余时间
    private long remainingTime;

    private Transform imageRemainingTime;
    private Text textRemainingTime;
    
    void Start()
    {
        gameView = GameObject.Find("Canvas").GetComponent<GameView>();
        imageRemainingTime = transform.Find("CatPortrait/ImageRemainingTime");
        textRemainingTime = imageRemainingTime.Find("TextRemainingTime").GetComponent<Text>();
        m_emergencyFlag = transform.Find("ImageEmergencyFlag");
        m_emergencyFlagAnimation = m_emergencyFlag.GetComponent<Animation>();

        m_finishFlag = transform.Find("ImageFinishFlag");
        m_finishFlagAnimation = m_finishFlag.GetComponent<Animation>();
        
        m_eventImage = transform.Find("ImageEvent").GetComponent<Image>();
        m_mask = m_eventImage.transform.Find("Mask").GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        pointerDragCache = eventData.pointerDrag;
        if ((droppedSpecialEvent = pointerDragCache.GetComponent<DragHandlerSpecialEvent>()) != null)
        {
            if (droppedSpecialEvent.bIsExtracting)
            {
                myID = droppedSpecialEvent.GetEventID();
                DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByIndex(index);
                
                // on preinit
                droppedSpecialEvent.gameObject.SetActive(false);
                gameView.DroppedImage = droppedSpecialEvent.gameObject;
                eventHandler.OnInit(myID);
                gameView.currentDialogEventID = myID;
                m_myCatEventInfo = eventHandler.GetEventInfo();
                m_eventImage.sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
                m_eventImage.enabled = true;
            }
        }
        else if ((droppedNPCEvent = pointerDragCache.GetComponent<DragHandlerNPCEvent>()) != null)
        {
            myID = droppedNPCEvent.GetEventID();
            DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByIndex(index);
            
            // on preinit
            eventHandler.OnInit(myID);
            gameView.currentDialogEventID = myID;
            m_myCatEventInfo = eventHandler.GetEventInfo();
            m_eventImage.sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
            m_eventImage.enabled = true;
        }

        InitHandler();
        m_mask.gameObject.SetActive(true);
    }

    public void InitHandler()
    {
        DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByIndex(index);
        // transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
        // transform.Find("ImageEvent").GetComponent<Image>().enabled = true;
        
        remainingTime = eventHandler.GetTimeRemain();
        textRemainingTime.text = Convert.ToString((remainingTime) * 10);
        imageRemainingTime.gameObject.SetActive(true);
        
        if ((droppedSpecialEvent = pointerDragCache.GetComponent<DragHandlerSpecialEvent>()) != null)
        {
            droppedSpecialEvent.EndDrag();
        }
        else if ((droppedNPCEvent = pointerDragCache.GetComponent<DragHandlerNPCEvent>()) != null)
        {
            droppedNPCEvent.EndDrag();
        }
        
    }

    public void DestroyEvent()
    {
        if ((droppedSpecialEvent = pointerDragCache.GetComponent<DragHandlerSpecialEvent>()) != null)
        {
            Destroy(droppedSpecialEvent.transform.parent.gameObject);
            transform.GetComponent<Image>().raycastTarget = false;
        }
        else if ((droppedNPCEvent = pointerDragCache.GetComponent<DragHandlerNPCEvent>()) != null)
        {
            //TODO:删除该NPC池子中的事件
            droppedNPCEvent.GetComponent<Image>().sprite = null;
            droppedNPCEvent.gameObject.SetActive(false);
            transform.GetComponent<Image>().raycastTarget = false;
        }
    }
    
    public static void SendEventDestroyEvent(long id)
    {
        return;
    }

    void Update()
    {
        
        DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByIndex(index);
        remainingTime = eventHandler.GetTimeRemain();
        //只要栏位中有事件(即ID不是默认的-1)就用remainingTime刷新UI显示
        var tempEvent = eventHandler.GetEventInfo();
        if (tempEvent != null)
        {
            myID = tempEvent.ID;
        }
        if (tempEvent != null && myID != -1)
        {
            textRemainingTime.text = Convert.ToString((remainingTime) * 10);
            var percent = (float)remainingTime / tempEvent.ConsumeTime;
            m_mask.localScale = new Vector3(1, percent, 1);
        }
        
        if (tempEvent == null)
            OnFinish();
    }

    //TODO:事件完成时调这个方法清空栏位，可以是(0 == remainingTime)的时候？
    public void OnFinish()
    {
        m_eventImage.enabled = false;
        m_eventImage.sprite = null;
        imageRemainingTime.gameObject.SetActive(false);
        myID = -1;
        transform.GetComponent<Image>().raycastTarget = true;
        droppedSpecialEvent = null;
        droppedNPCEvent = null;
        
        m_mask.gameObject.SetActive(false);
    }
    
    public void ButtonFinishFlag_OnClick()
    {
        EventHandlerManager.Instance.GetHandlerByIndex(index).OnFinish();
        gameView.currentDialogEventID = myID;
    }
    
    public void ButtonEmergencyFlag_OnClick()
    {
        EventHandlerManager.Instance.GetHandlerByIndex(index).OnEmergency();
        gameView.currentDialogEventID = myID;
    }

    public void SetEmergencyFlag(bool active)
    {
        if (active == m_emergencyFlag.gameObject.activeSelf)
        {
            return;
        }
        
        m_emergencyFlag.gameObject.SetActive(active);

        var inAni = m_emergencyFlagAnimation[FlagAnimation];
        if (active)
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
        m_emergencyFlagAnimation.Sample();
        m_emergencyFlagAnimation.Play(FlagAnimation);
    }

    public void SetFinishFlag(bool active)
    {
        if (active == m_finishFlag.gameObject.activeSelf)
        {
            return;
        }
        
        m_finishFlag.gameObject.SetActive(active);
        
        var inAni = m_finishFlagAnimation[FlagAnimation];
        if (active)
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
        m_finishFlagAnimation.Sample();
        m_finishFlagAnimation.Play(FlagAnimation);
    }
}
    
