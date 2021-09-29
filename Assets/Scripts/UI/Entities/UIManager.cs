using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EmergencyInfo;
using Logic;
using Logic.Event;
using Logic.Human;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: BaseModel<UIManager>
{
    public Transform gameView;
    
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
    public GameObject panelNPCInfo;
    
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
    public Transform textEventCardTime_start;
    public Transform textEventCardName_start;
    
    public GameObject imageTarget_start;
    public GameObject imageParticipant_start;
    public GameObject imageParticipantCenter_start;
    
    //FinsihEventDialog
    public Transform textEventName_finish;
    public Transform textEventDescription_finish;
    public Transform imageEvent_finish;
    public Transform textResult_finish;
    public Transform buttonText_finish;
    
    //EmergencyDialog
    public Transform textEventName_emergency;
    public Transform textEventDescription_emergency;
    public Transform imageEvent_emergency;
    public Transform textResult_emergency;
    public Transform textEventCardTime_emergency;
    public Transform textEventCardName_emergency;
    public Transform Choice1_emergency;
    public Transform Choice2_emergency;
    
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
    
    //NPCInfo
    public GameObject imageNPC;
    public GameObject textNPCName;
    public GameObject textNPCTitle;
    public GameObject textNPCVisibility;
    public GameObject textLabelA;
    public GameObject textLabelB;
    public GameObject textLabelC;
    
    //Animation
    public GameObject EventPopAnimation;

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

    public void Init()
    {
        darkBackgroundImage = gameView.Find("ScrollRelationship/Viewport/Content/BackgroundImage/ImageBackgroundDark").gameObject;
        panelCouncil = gameView.Find("PanelCouncil").gameObject;
        panelSettings = gameView.Find("PanelSettings").gameObject;
        panelEventExe = gameView.Find("PanelEventExe").gameObject;
        panelResources = gameView.Find("PanelResources").gameObject;
        panelStartEventDialog = gameView.Find("PanelStartEventDialog").gameObject;
        panelEmergencyDialog = gameView.Find("PanelEmergencyDialog").gameObject;
        panelFinishEventDialog = gameView.Find("PanelFinishEventDialog").gameObject;
        scrollSpecialEvent = gameView.Find("ScrollSpecialEvent").gameObject;
        buttonOpenExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
        buttonCloseExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
        buttonCouncil = gameView.Find("ButtonCouncil").gameObject;
        buttonCouncilCatManage = panelCouncil.transform.Find("ButtonManage").gameObject;
        EventPopAnimation = gameView.Find("Animation/EventPopAnimation").gameObject;
        panelNPCInfo = gameView.Find("PanelNPCInfo").gameObject;
        InitNPCInfo();
        InitCatColumns();
        InitDialogs();
    }

    private void InitNPCInfo()
    {
        imageNPC = panelNPCInfo.transform.Find("ImageNPCBackground/ImageNPC").gameObject;
        textNPCName = panelNPCInfo.transform.Find("TextNPCName").gameObject;
        textNPCTitle = panelNPCInfo.transform.Find("TextNPCTitle").gameObject;
        textNPCVisibility = panelNPCInfo.transform.Find("TextNPCVisibility").gameObject; 
        textLabelA = panelNPCInfo.transform.Find("TextLabelA").gameObject;  
        textLabelB = panelNPCInfo.transform.Find("TextLabelB").gameObject;  
        textLabelC = panelNPCInfo.transform.Find("TextLabelC").gameObject; 
    }
    private void InitCatColumns()
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
    private void InitDialogs()
    {
        //StartEventDialog
        textEventName_start = panelStartEventDialog.transform.Find("TextEventName");
        textEventDescription_start = panelStartEventDialog.transform.Find("TextEventDescription");
        imageEvent_start = panelStartEventDialog.transform.Find("ImageEvent");
        textEventCardTime_start = imageEvent_start.Find("EventCard/TextEventCardTime");
        textEventCardName_start = imageEvent_start.Find("EventCard/TextEventCardName");
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
        textEventName_emergency = panelEmergencyDialog.transform.Find("TextEventName");
        textEventDescription_emergency = panelEmergencyDialog.transform.Find("TextEventDescription");
        imageEvent_emergency = panelEmergencyDialog.transform.Find("ImageEvent"); 
        textEventCardTime_emergency = imageEvent_emergency.Find("EventCard/TextEventCardTime");
        Choice1_emergency = panelEmergencyDialog.transform.Find("ImageChoice1");
        Choice2_emergency = panelEmergencyDialog.transform.Find("ImageChoice2");
    }
    public void InitStartEventDialog(CatEvent m_myCatEventInfo)
    {
        SwitchDarkBackGround(true);
        panelStartEventDialog.SetActive(true);
        textEventName_start.GetComponent<Text>().text = m_myCatEventInfo.Name;
        textEventDescription_start.GetComponent<Text>().text = m_myCatEventInfo.Name;//TODO:读事件描述，然后替换这个.Name @muidarzhang
        imageEvent_start.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myCatEventInfo.Imageout);//TODO:这个字段策划还没配！！
        textResultPreview_start.GetComponent<Text>().text = m_myCatEventInfo.Name;//TODO:读事件描述，然后替换这个.Name @muidarzhang
        textEventCardTime_start.GetComponent<Text>().text = (m_myCatEventInfo.ConsumeTime * 10) + "分钟";
        switch(m_myCatEventInfo.Type)
        {
            case 0:
                textEventCardName_start.GetComponent<Text>().text = "特殊事件";
                break;
            case 1:
                textEventCardName_start.GetComponent<Text>().text = "刺探事件";
                break;
            case 2:
                textEventCardName_start.GetComponent<Text>().text = "密谋事件";
                break;
            case 3:
                textEventCardName_start.GetComponent<Text>().text = "交流事件";
                break;
        }

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

    public void InitEmergencyDialog(Emergency m_myEmergencyInfo, long cacheTime)
    {
        SwitchDarkBackGround(true);
        panelEmergencyDialog.SetActive(true);
        textEventName_emergency.GetComponent<Text>().text = m_myEmergencyInfo.Name;
        textEventDescription_emergency.GetComponent<Text>().text = m_myEmergencyInfo.Description;//TODO:读事件描述，然后替换这个.Name @muidarzhang
        textEventCardTime_emergency.GetComponent<Text>().text = (cacheTime * 10) + "分钟";
        //imageEvent_emergency.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myEmergencyInfo.ImageIn);//TODO:这个字段策划还没配！！
        List<EmergencyInfoConfig.Types.Option> optionList = m_myEmergencyInfo.GetOptions();
        if (optionList.Count >= 1)
        {
            Choice1_emergency.Find("TextChoice").GetComponent<Text>().text = optionList[0].Description;
            Choice1_emergency.Find("ButtonChoice/Text").GetComponent<Text>().text = optionList[0].Name;
            if (optionList.Count == 2)
            {
                Choice2_emergency.Find("TextChoice").GetComponent<Text>().text = optionList[1].Description;
                Choice2_emergency.Find("ButtonChoice/Text").GetComponent<Text>().text = optionList[1].Name;
            }
        }
    }
    
    public void InitFinishEventDialog(ResultEventInfo.ResultEventInfo.Types.ResultEventItem m_myResultInfo)
    {
        SwitchDarkBackGround(true);
        panelFinishEventDialog.SetActive(true);
        textEventName_finish.GetComponent<Text>().text = m_myResultInfo.Name;
        textEventDescription_finish.GetComponent<Text>().text = m_myResultInfo.Description1;
        //imageEvent_finish.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_myResultInfo.ImageIn);//TODO:这个字段策划还没配！！
        textResult_finish.GetComponent<Text>().text = m_myResultInfo.Description2;
        //buttonText_finish.GetComponent<Text>().text = m_myResultInfo.BtnTxt;
    }

    public void SwitchNPCInfo(Human m_NPC)
    {
        imageNPC.GetComponent<Image>().sprite = Resources.Load<Sprite>(m_NPC.Image);
        textNPCName.GetComponent<Text>().text = m_NPC.Name;
        textNPCTitle.GetComponent<Text>().text = m_NPC.Title;
        textNPCVisibility.GetComponent<Text>().text = m_NPC.Visibility.ToString();
        
        //TODO:等表改了之后读三个label
        textLabelA.GetComponent<Text>().text = m_NPC.Name;
        textLabelB.GetComponent<Text>().text = m_NPC.Name;
        textLabelC.GetComponent<Text>().text = m_NPC.Name;
    }
}
