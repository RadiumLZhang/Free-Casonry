using System.Collections;
using System.Collections.Generic;
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
    
    private Vector3 pos;                            //控件初始位置
    private Vector3 mousePos;                       //鼠标初始位置
    
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
        draggingImage = InsImage();
        rectTransform = draggingImage.transform.GetComponent<RectTransform>();
        pos = GetComponent<RectTransform>().position;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, null, out mousePos);
        gameView.OpenExePanel();
        transform.Find("ImageEventEmpty").gameObject.SetActive(true);
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
        EndDrag();
    }

    public void EndDrag()
    {
        gameView.CloseExePanel();
        Destroy(draggingImage);
        transform.Find("ImageEventEmpty").gameObject.SetActive(false);
        GetComponent<Image>().raycastTarget = true;
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