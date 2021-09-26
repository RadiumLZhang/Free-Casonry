using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatColumnHandler : MonoBehaviour, IDropHandler
{
    private Logic.Event.Event myEventInfo;
    private DragHandlerSpecialEvent droppedSpecialEvent;
    private DragHandlerNPCEvent droppedNPCEvent;
    
    public int index;
    private long myID = -1;

    //remainingTime维护剩余时间
    private long remainingTime;

    private Transform imageRemainingTime;
    private Text textRemainingTime;
    
    void Start()
    {
        imageRemainingTime = transform.Find("CatPortrait").Find("ImageRemainingTime");
        textRemainingTime = imageRemainingTime.Find("TextRemainingTime").GetComponent<Text>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if ((droppedSpecialEvent = eventData.pointerDrag.GetComponent<DragHandlerSpecialEvent>()) != null)
        {
            if (droppedSpecialEvent.bIsExtracting)
            {
                myID = droppedSpecialEvent.GetEventID();
                DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByID(index);
                eventHandler.SetEventInfo((int) myID);

                myEventInfo = eventHandler.GetEventInfo();
                transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(myEventInfo.Image);
                transform.Find("ImageEvent").GetComponent<Image>().enabled = true;

                remainingTime = eventHandler.GetTimeRemain();
                textRemainingTime.text = Convert.ToString(remainingTime) + "s";
                imageRemainingTime.gameObject.SetActive(true);

                droppedSpecialEvent.EndDrag();
                Destroy(droppedSpecialEvent.transform.parent.gameObject);
                transform.GetComponent<Image>().raycastTarget = false;
            }
        }
        else if ((droppedNPCEvent = eventData.pointerDrag.GetComponent<DragHandlerNPCEvent>()) != null)
        {
            myID = droppedNPCEvent.GetEventID();
            DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByID(index);
            eventHandler.SetEventInfo((int) myID);
                
            myEventInfo = eventHandler.GetEventInfo();
            transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(myEventInfo.Image);
            transform.Find("ImageEvent").GetComponent<Image>().enabled = true;
                
            remainingTime = eventHandler.GetTimeRemain();
            textRemainingTime.text = Convert.ToString(remainingTime) + "s";
            imageRemainingTime.gameObject.SetActive(true);
                
            droppedNPCEvent.EndDrag();
            Destroy(droppedNPCEvent.transform.parent.gameObject);
            transform.GetComponent<Image>().raycastTarget = false;
        }
    }

    public static void SendEventDestroyEvent(long id)
    {
        return;
    }

    void Update()
    {
        DesignedEventHandler eventHandler = EventHandlerManager.Instance.GetHandlerByID(index);
        remainingTime = eventHandler.GetTimeRemain();
        //只要栏位中有事件(即ID不是默认的-1)就用remainingTime刷新UI显示
        if (myID != -1)
            textRemainingTime.text = Convert.ToString(remainingTime) + "s";
        if (remainingTime == 0)
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
}
    