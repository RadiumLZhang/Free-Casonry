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
    
    public int index;
    private long myID = -1;

    //remainingTime维护剩余时间
    private long remainingTime;

    private Transform imageRemainingTime;
    private Text textRemainingTime;
    
    void Start()
    {
        gameView = GameObject.Find("Canvas").GetComponent<GameView>();
        imageRemainingTime = transform.Find("CatPortrait").Find("ImageRemainingTime");
        textRemainingTime = imageRemainingTime.Find("TextRemainingTime").GetComponent<Text>();
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
                
                transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
                transform.Find("ImageEvent").GetComponent<Image>().enabled = true;
            }
        }
        else if ((droppedNPCEvent = pointerDragCache.GetComponent<DragHandlerNPCEvent>()) != null)
        {
            myID = droppedNPCEvent.GetEventID();
            DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByIndex(index);
            
            // on preinit
            eventHandler.OnInit(myID);
            gameView.currentDialogEventID = myID;
            
        }

        InitHandler();
    }

    public void InitHandler()
    {
        DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByIndex(index);
        // transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
        // transform.Find("ImageEvent").GetComponent<Image>().enabled = true;
        
        remainingTime = eventHandler.GetTimeRemain();
        textRemainingTime.text = Convert.ToString((remainingTime) * 10) + "分钟";
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
        if (myID != -1)
        {
            textRemainingTime.text = Convert.ToString((remainingTime) * 10) + "分钟";
        }
        
        if (tempEvent == null)
            OnFinish();
    }

    //TODO:事件完成时调这个方法清空栏位，可以是(0 == remainingTime)的时候？
    public void OnFinish()
    {
        transform.Find("ImageEvent").GetComponent<Image>().enabled = false;
        transform.Find("ImageEvent").GetComponent<Image>().sprite = null;
        imageRemainingTime.gameObject.SetActive(false);
        myID = -1;
        transform.GetComponent<Image>().raycastTarget = true;
        droppedSpecialEvent = null;
        droppedNPCEvent = null;
    }
    
    public void ButtonFinishFlag_OnClick()
    {
        EventHandlerManager.Instance.GetHandlerByIndex(index).OnFinish();
    }
    
    public void ButtonEmergencyFlag_OnClick()
    {
        EventHandlerManager.Instance.GetHandlerByIndex(index).OnEmergency();
    }
}
    
