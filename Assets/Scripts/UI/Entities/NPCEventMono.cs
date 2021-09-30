using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NPCEventMono : MonoBehaviour
{
    private long myID = -1;
    private Logic.Event.CatEvent m_myCatEventInfo;
    private Text textRemainingTime;
    private long remainingTime;

    public void InitWithID(long ID)
    {
        myID = ID;
        //读表
        m_myCatEventInfo = new Logic.Event.CatEvent(myID);

        //DragHandler接收ID
        transform.Find("ImageEvent").GetComponent<DragHandlerNPCEvent>().SetEventID(myID);

        //UI
        remainingTime = m_myCatEventInfo.ConsumeTime;
        textRemainingTime = transform.Find("ImageEvent").Find("EventTimeBackground").Find("TextEventTime")
            .GetComponent<Text>();
        transform.Find("ImageEvent").GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
        transform.Find("EventTextBackground").Find("TextEvent").GetComponent<Text>().text = m_myCatEventInfo.Name;
        textRemainingTime.text = (m_myCatEventInfo.ConsumeTime * 10) + "分钟";
        transform.Find("RedPoint").gameObject.SetActive(m_myCatEventInfo.IsImportant);
    }
}
