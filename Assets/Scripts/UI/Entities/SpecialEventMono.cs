using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpecialEventMono : MonoBehaviour
{
    private long myID = -1;
    private Logic.Event.CatEvent m_myCatEventInfo;
    private Text textRemainingTime;
    private long remainingTime;

    public void InitWithID(long ID)
    {
        myID = ID;
        //读表
        //TODO:demo里不读表的话就自己写下myEventInfo结构体，有ID,Name,ConsumeTime和sprite即可
        m_myCatEventInfo = new Logic.Event.CatEvent(myID);

        //DragHandler接收ID
        transform.Find("ImageEvent").GetComponent<DragHandlerSpecialEvent>().SetEventID(myID);

        //UI
        remainingTime = m_myCatEventInfo.ConsumeTime;
        textRemainingTime = transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime")
            .GetComponent<Text>();
        transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Image);
        print(transform.Find("ImageEvent").GetComponent<Image>().sprite);
        print(Resources.Load<Sprite>(m_myCatEventInfo.Image));
        print(m_myCatEventInfo.Image);
        
        transform.Find("EventTextBackground").Find("TextEvent").GetComponent<Text>().text = m_myCatEventInfo.Name;
        textRemainingTime.text = Convert.ToString(m_myCatEventInfo.ConsumeTime) + "秒";
        //TODO:监听销毁事件的Event，获取传参的ID并判断是否符合自身ID，是则销毁自身
    }
}
