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
    private Text textMin;

    void Awake()
    {
        textRemainingTime = transform.Find("EventTextBackground/EventTimeBackground/TextEventTime").GetComponent<Text>();
        textMin = textRemainingTime.transform.parent.Find("TextEventMin").GetComponent<Text>();
    }

    void Update()
    {
        if (m_myCatEventInfo != null)
        {
            if (m_myCatEventInfo.Countdown == 0)
            {
                textRemainingTime.text = "";
                textMin.text = "";
            }
            else
            {
                textRemainingTime.text = (m_myCatEventInfo.Countdown * 10).ToString();
                textMin.text = "分后销毁";
            }
        }
    }
    
    public void InitWithID(long ID)
    {
        myID = ID;
        //读表
        m_myCatEventInfo = EventManager.Instance.GetCatEventByID(myID);

        //DragHandler接收ID
        transform.Find("ImageEvent").GetComponent<DragHandlerNPCEvent>().SetEventID(myID);

        //UI
        Image m_imageEvent = transform.Find("ImageEvent").GetComponent<Image>();
        m_imageEvent.gameObject.SetActive(true);
        switch (m_myCatEventInfo.Type)
        {
            case 0:
                m_imageEvent.sprite = Resources.Load<Sprite>("Sprites/Events/特殊事件玻璃");
                break;
            case 1:
                m_imageEvent.sprite = Resources.Load<Sprite>("Sprites/Events/探查事件玻璃");
                break;
            case 2:
                m_imageEvent.sprite = Resources.Load<Sprite>("Sprites/Events/密谋事件玻璃");
                break;
            case 3:
                m_imageEvent.sprite = Resources.Load<Sprite>("Sprites/Events/交际事件玻璃");
                break;
        }
        
        m_imageEvent.transform.Find("ImageEventIcon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Events/" + m_myCatEventInfo.Imageout);
        
        transform.Find("EventTextBackground").Find("TextEvent").GetComponent<Text>().text = m_myCatEventInfo.Name;
        transform.Find("RedPoint").gameObject.SetActive(m_myCatEventInfo.IsImportant);
    }

    public void EmptyCol()
    {
        textRemainingTime = transform.Find("EventTextBackground/EventTimeBackground/TextEventTime").GetComponent<Text>();
        textMin = textRemainingTime.transform.parent.Find("TextEventMin").GetComponent<Text>();
        textRemainingTime.text = "";
        textMin.text = "";
        GameObject m_image = transform.Find("ImageEvent").gameObject;
        m_image.GetComponent<Image>().sprite = null;
        m_image.SetActive(false);
        transform.Find("EventTextBackground").Find("TextEvent").GetComponent<Text>().text = "";
        transform.Find("RedPoint").gameObject.SetActive(false);
    }
}
