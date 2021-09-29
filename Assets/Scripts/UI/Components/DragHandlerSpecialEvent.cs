using System.Collections;
using System.Collections.Generic;
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

    public float extractAngle;
    public bool bIsExtracting;
    private uint timerDragStart;
    private float tempMousePos_x;

    void Start()
    {
        scrollRect = GameObject.Find("ScrollSpecialEvent").GetComponent<ScrollRect>();
        gameView = GameObject.Find("Canvas").GetComponent<GameView>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        bIsExtracting = false;
        timerDragStart = 6;
        GetComponent<Image>().raycastTarget = false;
        rectTransform = transform.GetComponent<RectTransform>();
        
        pos = GetComponent<RectTransform>().position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out mousePos);
        tempMousePos_x = mousePos.x;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newVec;          
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out newVec);
        Vector3 offset = new Vector3(newVec.x - mousePos.x, newVec.y - mousePos.y, 0);
        rectTransform.position = pos + offset;
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
                transform.Find("EventTimeBackground").gameObject.SetActive(false);
                gameView.OpenExePanel();
            }
        }
        
        if (!bIsExtracting)
        {
            rectTransform.position = pos;
            if (timerDragStart == 1)
            {
                GetComponent<Image>().raycastTarget = true;
            }
            scrollRect.horizontalNormalizedPosition -= (Input.mousePosition.x - tempMousePos_x) * Screen.width * 1.7f / Mathf.Pow(HandleSelfFittingHorizontal(transform.parent.parent),2.0f);
            tempMousePos_x = Input.mousePosition.x;
        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    public void EndDrag()
    {
        if (bIsExtracting) gameView.CloseExePanel();
        bIsExtracting = false;
        timerDragStart = 6;
        rectTransform.position = pos;
        transform.GetComponent<Image>().maskable = true;
        transform.Find("EventTimeBackground").gameObject.SetActive(true);
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