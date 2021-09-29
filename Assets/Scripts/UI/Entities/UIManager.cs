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
    public Transform textEventName_start;
    public Transform textEventDescription_start;
    public Transform imageEvent_start;
    public Transform textResultPreview_start;
    
    public GameObject imageTarget_start;
    public GameObject imageParticipant_start;
    public GameObject imageParticipantCenter_start;
    
    //FinsihEventDialog
    public Transform textEventName_finish;
    public Transform textEventDescription_finish;
    public Transform imageEvent_finish;
    public Transform textResult_finish;
    public Transform buttonText_finish;
    
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
    public void InitDialogs()
    {
        //StartEventDialog
        textEventName_start = panelStartEventDialog.transform.Find("TextEventName");
        textEventDescription_start = panelStartEventDialog.transform.Find("TextEventDescription");
        imageEvent_start = panelStartEventDialog.transform.Find("ImageEvent");
        textResultPreview_start = panelStartEventDialog.transform.Find("ImageResultPreview/TextResultPreview");
        
        imageTarget_start = panelStartEventDialog.transform.Find("ImageTarget").gameObject;
        imageParticipant_start = panelStartEventDialog.transform.Find("ImageParticipant").gameObject;
        imageParticipantCenter_start = panelStartEventDialog.transform.Find("ImageParticipantCenter").gameObject;
        
        //FinishEventDialog
        textEventName_finish = panelFinishEventDialog.transform.Find("TextEventName");;
        textEventDescription_finish = panelFinishEventDialog.transform.Find("TextEventDescription");
        imageEvent_finish = panelFinishEventDialog.transform.Find("ImageEvent");
        textResult_finish = panelFinishEventDialog.transform.Find("ImageResult/TextResult");
        buttonText_finish = panelFinishEventDialog.transform.Find("ButtonFinishEvent/Text");
        
        //EmergencyDialog
    }
    public void InitStartEventDialog(CatEvent m_myCatEventInfo)
    {
        panelStartEventDialog.SetActive(true);
        textEventName_start.GetComponent<Text>().text = m_myCatEventInfo.Name;
        textEventDescription_start.GetComponent<Text>().text = m_myCatEventInfo.Name;//TODO:读事件描述，然后替换这个.Name
        imageEvent_start.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);//TODO:这个字段策划还没配！！
        textResultPreview_start.GetComponent<Text>().text = m_myCatEventInfo.Name;//TODO:读事件结果，然后替换这个.Name
        if (m_myCatEventInfo.HumanId == 0)
        {
            GameObject.Find("Canvas").GetComponent<UtilsMath>().WriteToFile("走到了human id == 0");
            imageParticipantCenter_start.GetComponent<Image>().sprite = Resources.Load<Sprite>("hahaha");//TODO：换成拖进猫栏对应的猫的头像图片，@muidarzhang
            imageTarget_start.SetActive(false);
            imageParticipant_start.SetActive(false);
            imageParticipantCenter_start.SetActive(true);
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<UtilsMath>().WriteToFile("走到了human id != 0");
            imageParticipant_start.GetComponent<Image>().sprite = Resources.Load<Sprite>("hahaha");//TODO：换成拖进猫栏对应的猫的头像图片，@muidarzhang
            imageTarget_start.GetComponent<Image>().sprite = Resources.Load<Sprite>("hahaha");//TODO：绑定的人类单头像的路径，需要读人物表，@xinqizhou，人物id是m_myCatEventInfo.HumanId
            imageTarget_start.SetActive(true);
            imageParticipant_start.SetActive(true);
            imageParticipantCenter_start.SetActive(false);
        }
    }
    
    public void InitFinishEventDialog(Result m_myResultInfo)
    {
        panelFinishEventDialog.SetActive(true);
        textEventName_finish.GetComponent<Text>().text = m_myResultInfo.Name;
        textEventDescription_finish.GetComponent<Text>().text = m_myResultInfo.Description1;
        //imageEvent_finish.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myResultInfo.ImageIn);//TODO:这个字段策划还没配！！
        textResult_finish.GetComponent<Text>().text = m_myResultInfo.Description2;
        buttonText_finish.GetComponent<Text>().text = m_myResultInfo.BtnTxt;
    }
}
