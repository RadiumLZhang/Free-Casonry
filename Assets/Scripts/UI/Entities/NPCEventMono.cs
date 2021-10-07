using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
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
        m_myCatEventInfo = EventManager.Instance.GetCatEventByID(myID);

        //DragHandler接收ID
        transform.Find("ImageEvent").GetComponent<DragHandlerNPCEvent>().SetEventID(myID);

        //UI
        remainingTime = m_myCatEventInfo.ConsumeTime;
        transform.Find("EventTimeBackground/TextEventTime").GetComponent<Text>().text = (m_myCatEventInfo.ConsumeTime * 10) + "";
        transform.Find("EventTimeBackground/TextEventMin").GetComponent<Text>().text = "min";
        GameObject m_image = transform.Find("ImageEvent").gameObject;
        m_image.SetActive(true);
        m_image.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
        
        transform.Find("EventTextBackground").Find("TextEvent").GetComponent<Text>().text = m_myCatEventInfo.Name;
        transform.Find("RedPoint").gameObject.SetActive(m_myCatEventInfo.IsImportant);
    }

    public void EmptyCol()
    {
        transform.Find("EventTimeBackground/TextEventTime").GetComponent<Text>().text = "";
        transform.Find("EventTimeBackground/TextEventMin").GetComponent<Text>().text = "";
        GameObject m_image = transform.Find("ImageEvent").gameObject;
        m_image.GetComponent<Image>().sprite = null;
        m_image.SetActive(false);
        transform.Find("EventTextBackground").Find("TextEvent").GetComponent<Text>().text = "";
        transform.Find("RedPoint").gameObject.SetActive(false);
    }
}
