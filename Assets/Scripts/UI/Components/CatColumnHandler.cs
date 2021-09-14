using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CatColumnHandler : MonoBehaviour, IDropHandler
{
    private Logic.Event.Event myEventInfo;
    private DragHandlerSpecialEvent droppedEvent;
    
    public int index;
    private long myID = -1;

    //remainingTime维护剩余时间
    private long remainingTime;

    public void OnDrop(PointerEventData eventData)
    {
        droppedEvent = eventData.pointerDrag.GetComponent<DragHandlerSpecialEvent>();
        myID = droppedEvent.GetEventID();
        print("what is index over here" + index);
        DesignedEventHandler eventHandler = EventManager.GetInstance().GetHandlerByID(index);
        eventHandler.SetEventInfo((int) myID);
        
        myEventInfo = eventHandler.GetEventInfo();
        transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(myEventInfo.Image);
        transform.Find("ImageEvent").GetComponent<Image>().enabled = true;
        transform.Find("ImageEvent").Find("EventTimeBackground").gameObject.SetActive(true);

        remainingTime = eventHandler.GetTimeRemain();
        transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime").GetComponent<Text>().text =
            Convert.ToString(remainingTime) + "秒";
        droppedEvent.EndDrag();
        Destroy(droppedEvent.transform.parent.gameObject);
        transform.GetComponent<Image>().raycastTarget = false;
    }

    public static void SendEventDestroyEvent(long id)
    {
        return;
    }

    void Update()
    {
        DesignedEventHandler eventHandler = EventManager.GetInstance().GetHandlerByID(index);
        remainingTime = eventHandler.GetTimeRemain();
        //只要栏位中有事件(即ID不是默认的-1)就用remainingTime刷新UI显示
        if (myID != -1)
            transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime").GetComponent<Text>().text =
                Convert.ToString(remainingTime) + "秒";
        if (remainingTime == 0)
            OnFinish();
        
    }

    //TODO:事件完成时调这个方法清空栏位，可以是(0 == remainingTime)的时候？
    public void OnFinish()
    {
        transform.Find("ImageEvent").GetComponent<Image>().enabled = false;
        transform.Find("ImageEvent").GetComponent<Image>().sprite = null;
        transform.Find("ImageEvent").Find("EventTimeBackground").gameObject.SetActive(false);
        myID = -1;
        transform.GetComponent<Image>().raycastTarget = true;
    }
}
    