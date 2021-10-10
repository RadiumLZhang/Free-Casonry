using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpecialEventMono : MonoBehaviour
{
    private long myID = -1;
    private Logic.Event.CatEvent m_myCatEventInfo;
    private Text textRemainingTime;
    private Text textMin;
    private long remainingTime;
    

    private Animation m_animation;
    private DragHandlerSpecialEvent m_dragHandlerSpecialEvent;
    private Image m_imageEvent;
    private Image m_imageEventIcon;
    private Transform m_eventBackground;
    private Text m_txtEventBackground;
    
    private void Awake()
    {
        m_animation = transform.GetComponent<Animation>();
            
        var eventIn = m_animation["SpecialEventIn"];
        eventIn.speed = 1;
        eventIn.normalizedTime = 0;
        eventIn.enabled = false;
            
        var scrollMove = m_animation["scrollMove"];
        scrollMove.speed = 1;
        scrollMove.normalizedTime = 0;
        scrollMove.enabled = false;
            
        m_animation.Sample();
        m_animation.Play(scrollMove.name);
        m_animation.PlayQueued(eventIn.name);
        
        textRemainingTime = transform.Find("EventTextBackground/EventTimeBackground/TextEventTime").GetComponent<Text>();
        textMin = textRemainingTime.transform.parent.Find("TextEventMin").GetComponent<Text>();
        m_imageEvent = transform.Find("ImageEvent").GetComponent<Image>();
        m_imageEventIcon = m_imageEvent.transform.Find("ImageEventIcon").GetComponent<Image>();
        m_eventBackground = transform.Find("EventTextBackground");
        m_txtEventBackground = m_eventBackground.Find("TextEvent").GetComponent<Text>();
        m_dragHandlerSpecialEvent = transform.Find("ImageEvent").GetComponent<DragHandlerSpecialEvent>();

    }

    public void InitWithID(long ID)
    {
        myID = ID;
        //读表
        m_myCatEventInfo = EventManager.Instance.GetCatEventByID(myID);
        
        //DragHandler接收ID
        m_dragHandlerSpecialEvent.SetEventID(myID);

        //UI
        remainingTime = m_myCatEventInfo.ConsumeTime;

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
        
        m_imageEventIcon.sprite = Resources.Load<Sprite>("Sprites/Events/" + m_myCatEventInfo.Imageout);
        m_txtEventBackground.text = m_myCatEventInfo.Name;
    }

    public void SetVisible(bool active)
    {
        m_eventBackground.gameObject.SetActive(active);
        m_imageEvent.gameObject.SetActive(active);
    }
    
    public void DestroyAnimation()
    {
        var destroyAnimation = m_animation["DestroySpecialEvent"];
        destroyAnimation.speed = 1;
        destroyAnimation.normalizedTime = 0;
        destroyAnimation.enabled = false;

        m_animation.Sample();
        m_animation.Play(destroyAnimation.name);
    }

    public void DestroySelf()
    {
        Destroy(transform.gameObject);
    }

    private void Update()
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
}
