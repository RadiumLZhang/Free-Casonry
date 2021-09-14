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
    private GameObject draggingImage;
    private ScrollRect scrollRect;
    private GameView gameView;
    
    private Vector3 pos;                            //控件初始位置
    private Vector3 mousePos;                       //鼠标初始位置

    public float extractAngle;
    private bool bIsExtracting;
    private uint timerDragStart;
    private float tempMousePos_x;

    void Start()
    {
        scrollRect = GameObject.Find("ScrollSpecialEvent").GetComponent<ScrollRect>();
        gameView = GameObject.Find("Canvas").GetComponent<GameView>();
    }
    private GameObject InsImage()
    {
        GetComponent<Image>().raycastTarget = false;
        GameObject tempImg = Instantiate(gameObject);
        tempImg.transform.SetParent(scrollRect.transform,false);
        return tempImg;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        bIsExtracting = false;
        timerDragStart = 6;
        draggingImage = InsImage();
        rectTransform = draggingImage.transform.GetComponent<RectTransform>();
        draggingImage.SetActive(false);
        
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
                draggingImage.SetActive(true);
                transform.Find("ImageEventEmpty").gameObject.SetActive(true);
                gameView.OpenExePanel();
            }
        }
        
        if (!bIsExtracting)
        {
            rectTransform.position = pos;
            if (timerDragStart == 1)
            {
                draggingImage.SetActive(false);
                transform.Find("ImageEventEmpty").gameObject.SetActive(false);
            }
            scrollRect.horizontalNormalizedPosition -= (Input.mousePosition.x - tempMousePos_x) * Screen.width * 1.7f / Mathf.Pow(HandleSelfFittingHorizontal(transform.parent.parent),2.0f);
            tempMousePos_x = Input.mousePosition.x;
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        bIsExtracting = false;
        timerDragStart = 6;
        Destroy(draggingImage);
        transform.Find("ImageEventEmpty").gameObject.SetActive(false);
        gameView.CloseExePanel();
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