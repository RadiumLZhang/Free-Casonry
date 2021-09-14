using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CatColumnHandler : MonoBehaviour,
    IDropHandler
{
    private Logic.Event.Event myEventInfo;
    private long myID = -1;
    private long remainingTime;
    public void OnDrop(PointerEventData eventData)
    {
        //TODO:为你的猫咪Handler注册事件
        myID = eventData.pointerDrag.GetComponent<DragHandlerSpecialEvent>().GetEventID();
        myEventInfo = new Logic.Event.Event(myID);
        transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(myEventInfo.Image);
        transform.Find("ImageEvent").Find("EventTimeBackground").gameObject.SetActive(true);
        remainingTime = myEventInfo.ConsumeTime;
        transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime").GetComponent<Text>().text = Convert.ToString(remainingTime) + "秒";
        //TODO:发送销毁事件的Event给所有SpecialEventMono
    }

    public static void SendEventDestroyEvent(long id)
    {
        return;
    }
    void Update()
    {
        if(myID != -1)
            transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime").GetComponent<Text>().text = Convert.ToString(remainingTime) + "秒";
    }

    public void OnFinish()
    {
        //TODO:事件完成时调这个方法清空栏位
        transform.Find("ImageEvent").GetComponent<Image>().sprite = null;
        transform.Find("ImageEvent").Find("EventTimeBackground").gameObject.SetActive(false);
        myID = -1;
    }
    
    //自己调这个方法读秒
    public void TimeDecrease()
    {
        remainingTime--;
    }
}
