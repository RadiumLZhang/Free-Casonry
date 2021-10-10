using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using FitMode = UnityEngine.UI.ContentSizeFitter.FitMode;
using Image = UnityEngine.UI.Image;

public class DragHandlerSpecialEvent : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private long eventID;
    private RectTransform rectTransform;
    private ScrollRect scrollRect;
    private GameView gameView;
    
    private Vector3 pos;                            //控件初始位置
    private Vector3 mousePos;                       //鼠标初始位置
    private Vector3 localPos;                       //控件初始相对位置

    public float extractAngle;
    public bool bIsExtracting;
    private uint timerDragStart;
    private float tempMousePos_x;
    public AudioSource adplayer;
    
    void Start()
    {
        adplayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        scrollRect = GameObject.Find("ScrollSpecialEvent").GetComponent<ScrollRect>();
        gameView = GameObject.Find("Canvas").GetComponent<GameView>();
        rectTransform = transform.GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Input.multiTouchEnabled = false;
        UIManager.Instance.SwitchDraggingMask(true);
        Debug.Log("muisc:4");
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "事件移动音效");
        adplayer.clip = m_clip;
        adplayer.Play();
        
        bIsExtracting = false;
        timerDragStart = 6;
        GetComponent<Image>().raycastTarget = false;
        
        pos = rectTransform.position;
        localPos = rectTransform.localPosition;
        
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out mousePos);
        tempMousePos_x = mousePos.x;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        
        Vector3 newVec;          
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out newVec);
        Vector3 offset = new Vector3(newVec.x - mousePos.x, newVec.y - mousePos.y, 0);
        
        if (timerDragStart > 1)
        {
            timerDragStart--;
            
        }
        if (timerDragStart == 2)
        {
            if (offset.y > 0 && offset.y/Mathf.Abs(offset.x) >= Mathf.Sin(extractAngle * Mathf.Deg2Rad))
            {
                bIsExtracting = true;
                transform.GetComponent<Image>().maskable = false;
                transform.Find("ImageEventIcon").GetComponent<Image>().maskable = false;
                //transform.Find("EventTimeBackground").gameObject.SetActive(false);
                gameView.OpenExePanel();
            }
        }
        
        if (!bIsExtracting)
        {
            rectTransform.localPosition = localPos;
            if (timerDragStart == 1)
            {
                GetComponent<Image>().raycastTarget = true;
            }
            scrollRect.horizontalNormalizedPosition -= (Input.mousePosition.x - tempMousePos_x) * Screen.width * 1.7f / Mathf.Pow(HandleSelfFittingHorizontal(transform.parent.parent),2.0f);
            tempMousePos_x = Input.mousePosition.x;
        }
        else
        {
            rectTransform.position = pos + offset;
        }
        
        if (bIsExtracting)
        {
            EventHandlerManager.Instance.RefreshColumnImage();
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("muisc:3");
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "事件取消-Cultist Simulator");
        adplayer.clip = m_clip;
        adplayer.Play();
        EndDrag();
        EventHandlerManager.Instance.ResetColumnImage();
        //Input.multiTouchEnabled = true;
        UIManager.Instance.SwitchDraggingMask(false);
    }

    public void EndDrag()
    {
        if (bIsExtracting) gameView.CloseExePanel();
        bIsExtracting = false;
        timerDragStart = 6;
        rectTransform.localPosition = localPos;
        transform.GetComponent<Image>().maskable = true;
        transform.Find("ImageEventIcon").GetComponent<Image>().maskable = true;
        //transform.Find("EventTimeBackground").gameObject.SetActive(true);
        GetComponent<Image>().raycastTarget = true;
    }
    
    private float HandleSelfFittingHorizontal(Transform obj)
    {
        FitMode fitting = obj.GetComponent<ContentSizeFitter>().horizontalFit;
        if (fitting == FitMode.MinSize)
        {
            return LayoutUtility.GetMinSize(obj.GetComponent<RectTransform>(), 0);
        }
        else
        {
            return LayoutUtility.GetPreferredSize(obj.GetComponent<RectTransform>(), 0);
        }
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