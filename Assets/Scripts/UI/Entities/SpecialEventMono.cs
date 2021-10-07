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
    

    private Animation m_animation;
    private DragHandlerSpecialEvent m_dragHandlerSpecialEvent;
    private Image m_imageEvent;
    private Transform m_eventBackground;
    private Text m_txtEventBackground;
    
    private void Start()
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
    }

    public void InitWithID(long ID)
    {
        myID = ID;
        //读表
        //TODO:demo里不读表的话就自己写下myEventInfo结构体，有ID,Name,ConsumeTime和sprite即可
        m_myCatEventInfo = new Logic.Event.CatEvent(myID);
        
        
        textRemainingTime = transform.Find("EventTimeBackground").Find("TextEventTime").GetComponent<Text>();
        m_imageEvent = transform.Find("ImageEvent").GetComponent<Image>();
        m_eventBackground = transform.Find("EventTextBackground");
        m_txtEventBackground = m_eventBackground.Find("TextEvent").GetComponent<Text>();
        m_dragHandlerSpecialEvent = transform.Find("ImageEvent").GetComponent<DragHandlerSpecialEvent>();

        //DragHandler接收ID
        m_dragHandlerSpecialEvent.SetEventID(myID);

        //UI
        remainingTime = m_myCatEventInfo.ConsumeTime;
        
        m_imageEvent.sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);
        m_txtEventBackground.text = m_myCatEventInfo.Name;
        textRemainingTime.text = (m_myCatEventInfo.ConsumeTime * 10) + "";
        //TODO:监听销毁事件的Event，获取传参的ID并判断是否符合自身ID，是则销毁自身
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
}
