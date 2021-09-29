using System.Collections;
using System.Collections.Generic;
using Logic;
using Logic.Event;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: BaseModel<UIManager>
{
    //Images
    public GameObject darkBackgroundImage;

    //Panels
    public GameObject panelCouncil;
    public GameObject panelSettings;
    public GameObject panelEventExe;
    public GameObject panelResources;
    public GameObject panelStartEventDialog;
    public GameObject panelEmergencyDialog;
    public GameObject panelFinishEventDialog;
    
    //Scrolls
    public GameObject scrollSpecialEvent;

    //Buttons
    public GameObject buttonOpenExePanel;
    public GameObject buttonCloseExePanel;
    public GameObject buttonCouncil;
    public GameObject buttonCouncilCatManage;
    
    //NPCs
    public Dictionary<long, GameObject> NPCEventCycles;

    //StartEventDialog
    public Transform textEventName;
    public Transform textEventDescription;
    public Transform imageEvent;
    public Transform textResultPreview;
    
    public GameObject imageTarget;
    public GameObject imageParticipant;
    public GameObject imageParticipantCenter;
    
    //CatColumns
    public GameObject CatColumn0;
    public GameObject CatColumn1;
    public GameObject CatColumn2;
    public GameObject CatColumn3;
    
    //FinishFlags
    public GameObject FinishFlag0;
    public GameObject FinishFlag1;
    public GameObject FinishFlag2;
    public GameObject FinishFlag3;
    public GameObject EmergencyFlag0;
    public GameObject EmergencyFlag1;
    public GameObject EmergencyFlag2;
    public GameObject EmergencyFlag3;
    
    public void SwitchDarkBackGround(bool bIsSwitchToDark)
    {
        darkBackgroundImage.SetActive(bIsSwitchToDark);
    }

    public void SwitchFinishFlag(int index,bool bIsSwitchToShown)
    {
        switch (index)
        {
            case 0:FinishFlag0.SetActive(bIsSwitchToShown);
                break;
            case 1:FinishFlag1.SetActive(bIsSwitchToShown);
                break;
            case 2:FinishFlag2.SetActive(bIsSwitchToShown);
                break;
            case 3:FinishFlag3.SetActive(bIsSwitchToShown);
                break;
        }
    }
    public void SwitchEmergencyFlag(int index,bool bIsSwitchToShown)
    {
        switch (index)
        {
            case 0:EmergencyFlag0.SetActive(bIsSwitchToShown);
                break;
            case 1:EmergencyFlag1.SetActive(bIsSwitchToShown);
                break;
            case 2:EmergencyFlag2.SetActive(bIsSwitchToShown);
                break;
            case 3:EmergencyFlag3.SetActive(bIsSwitchToShown);
                break;
        }
    }

    public void SwitchCatColumn(int index, bool bIsSwitchToShown)
    {
        switch (index)
        {
            case 0:CatColumn0.SetActive(bIsSwitchToShown);
                break;
            case 1:CatColumn1.SetActive(bIsSwitchToShown);
                break;
            case 2:CatColumn2.SetActive(bIsSwitchToShown);
                break;
            case 3:CatColumn3.SetActive(bIsSwitchToShown);
                break;
        }
    }

    public void InitCatColumns()
    {
        CatColumn0 = panelEventExe.transform.Find("EventSlot").gameObject;
        CatColumn1 = panelEventExe.transform.Find("EventSlot1").gameObject;
        CatColumn2 = panelEventExe.transform.Find("EventSlot2").gameObject;
        CatColumn3 = panelEventExe.transform.Find("EventSlot3").gameObject;
        
        FinishFlag0 = CatColumn0.transform.Find("ImageFinishFlag").gameObject;
        FinishFlag1 = CatColumn1.transform.Find("ImageFinishFlag").gameObject;
        FinishFlag2 = CatColumn2.transform.Find("ImageFinishFlag").gameObject;
        FinishFlag3 = CatColumn3.transform.Find("ImageFinishFlag").gameObject;
        
        EmergencyFlag0 = CatColumn0.transform.Find("ImageEmergencyFlag").gameObject;
        EmergencyFlag1 = CatColumn1.transform.Find("ImageEmergencyFlag").gameObject;
        EmergencyFlag2 = CatColumn2.transform.Find("ImageEmergencyFlag").gameObject;
        EmergencyFlag3 = CatColumn3.transform.Find("ImageEmergencyFlag").gameObject;
    }
    public void InitStartEventDialogUI()
    {
        textEventName = panelStartEventDialog.transform.Find("TextEventName");
        textEventDescription = panelStartEventDialog.transform.Find("TextEventDescription");
        imageEvent = panelStartEventDialog.transform.Find("ImageEvent");
        textResultPreview = panelStartEventDialog.transform.Find("ImageResultPreview").Find("TextResultPreview");
        
        imageTarget = panelStartEventDialog.transform.Find("ImageTarget").gameObject;
        imageParticipant = panelStartEventDialog.transform.Find("ImageParticipant").gameObject;
        imageParticipantCenter = panelStartEventDialog.transform.Find("ImageParticipantCenter").gameObject;
    }
    public void InitStartEventDialog(CatEvent m_myCatEventInfo)
    {
        panelStartEventDialog.SetActive(true);
        textEventName.GetComponent<Text>().text = m_myCatEventInfo.Name;
        textEventDescription.GetComponent<Text>().text = m_myCatEventInfo.Name;//TODO:读事件描述，然后替换这个.Name
        imageEvent.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);//TODO:这个字段策划还没配！！
        textResultPreview.GetComponent<Text>().text = m_myCatEventInfo.Name;//TODO:读事件结果，然后替换这个.Name
        if (m_myCatEventInfo.HumanId == 0)
        {
            GameObject.Find("Canvas").GetComponent<UtilsMath>().WriteToFile("走到了human id == 0");
            imageParticipantCenter.GetComponent<Image>().sprite = Resources.Load<Sprite>("hahaha");//TODO：换成拖进猫栏对应的猫的头像图片，@muidarzhang
            imageTarget.SetActive(false);
            imageParticipant.SetActive(false);
            imageParticipantCenter.SetActive(true);
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<UtilsMath>().WriteToFile("走到了human id != 0");
            imageParticipant.GetComponent<Image>().sprite = Resources.Load<Sprite>("hahaha");//TODO：换成拖进猫栏对应的猫的头像图片，@muidarzhang
            imageTarget.GetComponent<Image>().sprite = Resources.Load<Sprite>("hahaha");//TODO：绑定的人类单头像的路径，需要读人物表，@xinqizhou，人物id是m_myCatEventInfo.HumanId
            imageTarget.SetActive(true);
            imageParticipant.SetActive(true);
            imageParticipantCenter.SetActive(false);
        }
    }
}
