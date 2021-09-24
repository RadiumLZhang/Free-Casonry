using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCEventMono : MonoBehaviour
{
    private long myID = -1;
    private Logic.Event.Event myEventInfo;
    private Text textRemainingTime;
    private long remainingTime;

    public void InitWithID(long ID)
    {
        myID = ID;
        //读表
        //TODO:demo里不读表的话就自己写下myEventInfo结构体，有ID,Name,ConsumeTime和sprite即可
        myEventInfo = new Logic.Event.Event(myID);

        //DragHandler接收ID
        transform.Find("ImageEvent").GetComponent<DragHandlerSpecialEvent>().SetEventID(myID);

        //UI
        remainingTime = myEventInfo.ConsumeTime;
        textRemainingTime = transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime")
            .GetComponent<Text>();
        transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(myEventInfo.Image);
        transform.Find("EventTextBackground").Find("TextEvent").GetComponent<Text>().text = myEventInfo.Name;
        textRemainingTime.text = Convert.ToString(myEventInfo.ConsumeTime) + "秒";
        //TODO:监听销毁事件的Event，获取传参的ID并判断是否符合自身ID，是则销毁自身
    }
}
