using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using FitMode = UnityEngine.UI.ContentSizeFitter.FitMode;
using Image = UnityEngine.UI.Image;

public class DragHandlerNPCEvent : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private long eventID;
    private RectTransform rectTransform;
    private GameObject draggingImage;
    private ScrollRect scrollRect;
    private GameView gameView;
    public AudioSource adplayer;
    private Vector3 pos;                            //控件初始位置
    private Vector3 mousePos;                       //鼠标初始位置
    
    void Start()
    {
        adplayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        scrollRect = GameObject.Find("ScrollSpecialEvent").GetComponent<ScrollRect>();
        gameView = GameObject.Find("Canvas").GetComponent<GameView>();
    }
    private GameObject InsImage()
    {
        transform.Find("ImageEventIcon").GetComponent<Image>().raycastTarget = false;
        GetComponent<Image>().raycastTarget = false;
        GameObject tempImg = Instantiate(gameObject);
        tempImg.transform.SetParent(scrollRect.transform,false);
        return tempImg;
    }
    
    // 事件环
    public void OnBeginDrag(PointerEventData eventData)
    {
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "事件移动音效");
        Debug.Log("muisc:1");
        adplayer.clip = m_clip;
        adplayer.Play();
        //Input.multiTouchEnabled = false;
        UIManager.Instance.SwitchDraggingMask(true);
        draggingImage = InsImage();
        rectTransform = draggingImage.transform.GetComponent<RectTransform>();
        pos = GetComponent<RectTransform>().position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out mousePos);
        gameView.OpenExePanel();
        GetComponent<Image>().enabled = false;
        transform.Find("ImageEventIcon").GetComponent<Image>().enabled = false;
        
        EventHandlerManager.Instance.RefreshColumnImage();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newVec;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out newVec);
        Vector3 offset = new Vector3(newVec.x - mousePos.x, newVec.y - mousePos.y, 0);
        rectTransform.position = pos + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("muisc:2");
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "事件取消-Cultist Simulator");
        adplayer.clip = m_clip;
        adplayer.Play();
        EndDrag();
        EventHandlerManager.Instance.ResetColumnImage();
        //Input.multiTouchEnabled = true;
    }

    public void EndDrag()
    {
        UIManager.Instance.SwitchDraggingMask(false);
        gameView.CloseExePanel();
        Destroy(draggingImage);
        GetComponent<Image>().enabled = true;
        transform.Find("ImageEventIcon").GetComponent<Image>().enabled = true;
        GetComponent<Image>().raycastTarget = true;
        transform.Find("ImageEventIcon").GetComponent<Image>().raycastTarget = true;
    }

    public void SetEventID(long id)
    {
        eventID = id;
    }
    public long GetEventID()
    {
        return eventID;
    }
}