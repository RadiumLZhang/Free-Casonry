using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using Logic;
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

    private Transform bg;
    private Transform highlightBg;


    private const string FlagAnimation = "FlagIn";
    
    public int index;
    private long myID = -1;

    //remainingTime维护剩余时间
    private long remainingTime;
    
    public AudioSource adplayer;
    
    private Transform imageRemainingTime;
    private Text textRemainingTime;
    public bool isInit = false;

    private DesignedEventHandler eventHandler;
    void Awake()
    {
        adplayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        gameView = GameObject.Find("Canvas").GetComponent<GameView>();
        imageRemainingTime = transform.Find("CatPortrait/ImageRemainingTime");
        textRemainingTime = imageRemainingTime.Find("TextRemainingTime").GetComponent<Text>();
        m_emergencyFlag = transform.Find("ImageEmergencyFlag");
        m_emergencyFlagAnimation = m_emergencyFlag.GetComponent<Animation>();

        m_finishFlag = transform.Find("ImageFinishFlag");
        m_finishFlagAnimation = m_finishFlag.GetComponent<Animation>();
        
        m_eventImage = transform.Find("ImageEvent").GetComponent<Image>();
        m_mask = m_eventImage.transform.Find("Mask").GetComponent<RectTransform>();
        isInit = true;
        Debug.Log("cat column is started");

        bg = transform.Find("ImageEventBG");
        highlightBg = transform.Find("ImageEventBGHighlighted");
        
        eventHandler = EventHandlerManager.Instance.GetHandlerByIndex(index);
        //UI初始化
        Cat cat = EventHandlerManager.Instance.GetCatByIndex(index);
        transform.Find("CatPortrait/ImageCat").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Portraits/" + cat.Image);
        transform.Find("CatName").GetComponent<Text>().text = cat.Name;
        transform.Find("Image1/Text").GetComponent<Text>().text = cat.ScoutValue.ToString();
        transform.Find("Image2/Text").GetComponent<Text>().text = cat.Conspiracy.ToString();
        transform.Find("Image3/Text").GetComponent<Text>().text = cat.Communication.ToString();

    }
    public void OnDrop(PointerEventData eventData)
    {
        pointerDragCache = eventData.pointerDrag;
        if (pointerDragCache.name != "ImageEvent")
        {
            return;
        }
        print(pointerDragCache);
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "事件放入音效");
        adplayer.clip = m_clip;
        adplayer.Play();
        var tempEvent = eventHandler.GetEventInfo();
        
        if (tempEvent == null)
        {
            Input.multiTouchEnabled = false;
            if ((droppedSpecialEvent = pointerDragCache.GetComponent<DragHandlerSpecialEvent>()) != null)
            {
                if (droppedSpecialEvent.bIsExtracting)
                {
                    myID = droppedSpecialEvent.GetEventID();
                    EventHandlerManager.Instance.CurSelectIndex = index;
                
                    // on preinit
                    droppedSpecialEvent.gameObject.SetActive(false);
                    gameView.DroppedImage = droppedSpecialEvent.gameObject;
                    eventHandler.OnInit(myID);
                
                    gameView.currentDialogEventID = myID;
                    m_myCatEventInfo = eventHandler.GetEventInfo();
                    switch (m_myCatEventInfo.Type)
                    {
                        case 0:
                            m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/特殊事件玻璃");
                            break;
                        case 1:
                            m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/探查事件玻璃");
                            break;
                        case 2:
                            m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/密谋事件玻璃");
                            break;
                        case 3:
                            m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/交际事件玻璃");
                            break;
                    }
        
                    m_eventImage.transform.Find("ImageEventIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Events/" + m_myCatEventInfo.Imageout);
                    m_eventImage.enabled = true;
                    m_eventImage.transform.Find("ImageEventIcon").GetComponent<Image>().enabled = true;
                }
            }
            else if ((droppedNPCEvent = pointerDragCache.GetComponent<DragHandlerNPCEvent>()) != null)
            {
                myID = droppedNPCEvent.GetEventID();
                EventHandlerManager.Instance.CurSelectIndex = index;
            

                // on preinit
                eventHandler.OnInit(myID);
                gameView.currentDialogEventID = myID;
                m_myCatEventInfo = eventHandler.GetEventInfo();
                switch (m_myCatEventInfo.Type)
                {
                    case 0:
                        m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/特殊事件玻璃");
                        break;
                    case 1:
                        m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/探查事件玻璃");
                        break;
                    case 2:
                        m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/密谋事件玻璃");
                        break;
                    case 3:
                        m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/交际事件玻璃");
                        break;
                }
        
                m_eventImage.transform.Find("ImageEventIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Events/" + m_myCatEventInfo.Imageout);
                m_eventImage.enabled = true;
                m_eventImage.transform.Find("ImageEventIcon").GetComponent<Image>().enabled = true;
            }

            InitHandler();
            m_mask.gameObject.SetActive(true);
        
            EventHandlerManager.Instance.ResetColumnImage();
        }
        else
        {
            if ((pointerDragCache.GetComponent<DragHandlerSpecialEvent>()) != null)
            {
                pointerDragCache.GetComponent<DragHandlerSpecialEvent>().EndDrag();
            }
            else if ((pointerDragCache.GetComponent<DragHandlerNPCEvent>()) != null)
            {
                pointerDragCache.GetComponent<DragHandlerNPCEvent>().EndDrag();
            }
        }
    }

    public void Restore(CatEvent inEvent)
    {
        myID = inEvent.ID;
        gameView.currentDialogEventID = myID;
        m_myCatEventInfo = eventHandler.GetEventInfo();
        switch (m_myCatEventInfo.Type)
        {
            case 0:
                m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/特殊事件玻璃");
                break;
            case 1:
                m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/探查事件玻璃");
                break;
            case 2:
                m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/密谋事件玻璃");
                break;
            case 3:
                m_eventImage.sprite = Resources.Load<Sprite>("Sprites/Events/交际事件玻璃");
                break;
        }
        
        m_eventImage.transform.Find("ImageEventIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Events/" + m_myCatEventInfo.Imageout);
        m_eventImage.enabled = true;
        remainingTime = eventHandler.GetTimeRemain();
        textRemainingTime.text = Convert.ToString((remainingTime) * 10);
        imageRemainingTime.gameObject.SetActive(true);
        m_mask.gameObject.SetActive(true);
        
        m_eventImage.transform.Find("ImageEventIcon").GetComponent<Image>().enabled = true;
        
        remainingTime = eventHandler.GetTimeRemain();
        textRemainingTime.text = Convert.ToString((remainingTime) * 10);
        imageRemainingTime.gameObject.SetActive(true);
    }
    
    public void InitHandler()
    {

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
        /*if (TimeTickerManager.Instance.GetSpeed() == 0)
        {
            return;
        }*/
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


        /*if (tempEvent == null && myID == -1)
        {   
            Debug.Log("@@@ on finish");
            OnFinish();
        }*/
           
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
        m_eventImage.gameObject.SetActive(true);
        m_eventImage.transform.Find("ImageEventIcon").GetComponent<Image>().enabled = false;
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

    public void SetFinishGlass()
    {
        m_eventImage.sprite = UIManager.Instance.FinFinishGlass(eventHandler.GetEventInfo().Type);
    }
    public void HighLight(bool highLight)
    {
        highlightBg.gameObject.SetActive(highLight);
        bg.gameObject.SetActive(!highLight);
    }
}
    
