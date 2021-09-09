using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandlerSpecialEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    private RectTransform rectTransform;
    private GameObject DraggingImage;
    
    private Vector3 pos;                            //控件初始位置
    private Vector3 mousePos;                       //鼠标初始位置

    private GameObject InsImage()
    {
        GameObject tempImg = Instantiate(gameObject);
        tempImg.transform.SetParent(transform.parent.parent.parent.parent,false);
        return tempImg;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("开始拖拽");
        DraggingImage = InsImage();
        transform.Find("ImageEventEmpty").gameObject.SetActive(true);
        rectTransform = DraggingImage.transform.GetComponent<RectTransform>();
        pos = GetComponent<RectTransform>().position;    
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out mousePos);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newVec;          
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out newVec);
        Vector3 offset = new Vector3(newVec.x - mousePos.x, newVec.y - mousePos.y, 0);
        rectTransform.position = pos + offset;
 
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("结束拖拽");
        GameObject.Destroy(DraggingImage);
        transform.Find("ImageEventEmpty").gameObject.SetActive(false);
    }
 
}