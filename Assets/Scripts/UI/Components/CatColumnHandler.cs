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
    
    public int index = 0;
    private long myID = -1;

    //remainingTime维护剩余时间
    private long remainingTime;

    public void OnDrop(PointerEventData eventData)
    {
        //TODO:为你的猫咪Handler注册事件
        droppedEvent = eventData.pointerDrag.GetComponent<DragHandlerSpecialEvent>();
        myID = droppedEvent.GetEventID();
        print("what is index over here" + index);
        DesignedEventHandler eventHandler = EventManager.GetInstance().GetHandlerByID(index);
        eventHandler.SetEventInfo((int) myID);

        //TODO:demo里不读表的话就自己写下myEventInfo结构体，有ID,Name,ConsumeTime和sprite即可
        myEventInfo = eventHandler.GetEventInfo();
        transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(myEventInfo.Image);
        transform.Find("ImageEvent").Find("EventTimeBackground").gameObject.SetActive(true);

        //用表中的ConsumeTime(需要消耗的时间)初始化remainingTime
        remainingTime = eventHandler.GetTimeRemain();
        transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime").GetComponent<Text>().text =
            Convert.ToString(remainingTime) + "秒";
        droppedEvent.EndDrag();
        Destroy(droppedEvent.transform.parent.gameObject);
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
    }

    //TODO:事件完成时调这个方法清空栏位，可以是(0 == remainingTime)的时候？
    public void OnFinish()
    {
        //TODO:用猫咪Handler调事件完成方法（影响议会资源之类的）
        
        //清空UI的栏位并重设当前ID为-1
        transform.Find("ImageEvent").GetComponent<Image>().sprite = null;
        transform.Find("ImageEvent").Find("EventTimeBackground").gameObject.SetActive(false);
        myID = -1;
    }
}
    