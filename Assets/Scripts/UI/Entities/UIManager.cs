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
    public bool buttonCouncilActive = false;
    
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
    public NPCInfoMono panelNPCInfo;
    
    //Scrolls
    public GameObject scrollSpecialEvent;
    public GameObject scrollRelationShip;
    
    //Buttons
    public GameObject buttonOpenExePanel;
    public GameObject buttonCloseExePanel;
    public GameObject buttonCouncil;
    public GameObject buttonCouncilCatManage;
    public GameObject buttonCloseRelationship;
    public GameObject buttonRelationship;
    
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
    public CatColumnHandler[] catColumnHandlers;

    //Animation
    public GameObject EventPopAnimation;

    public void SwitchDarkBackGround(bool bIsSwitchToDark)
    {
        darkBackgroundImage.SetActive(bIsSwitchToDark);
    }

    public void SwitchFinishFlag(int index,bool bIsSwitchToShown)
    {
        catColumnHandlers[index].SetFinishFlag(bIsSwitchToShown);
    }
    public void SwitchEmergencyFlag(int index,bool bIsSwitchToShown)
    {
        catColumnHandlers[index].SetEmergencyFlag(bIsSwitchToShown);
    }

    public void SwitchCatColumn(int index, bool bIsSwitchToShown)
    {
        catColumnHandlers[index].gameObject.SetActive(bIsSwitchToShown);
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
        scrollRelationShip = gameView.Find("ScrollRelationship").gameObject;
        buttonOpenExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
        buttonCloseExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
        buttonCouncilCatManage = panelCouncil.transform.Find("ButtonManage").gameObject;
        buttonCloseRelationship = gameView.Find("ButtonCloseRelationship").gameObject;
        buttonRelationship = gameView.Find("ButtonRelationship").gameObject;
        EventPopAnimation = gameView.Find("Animation/EventPopAnimation").gameObject;
        
        buttonCouncil = gameView.Find("ButtonCouncil").gameObject;
        buttonCouncil.SetActive(buttonCouncilActive);

        panelNPCInfo = gameView.Find("PanelNPCInfo").GetComponent<NPCInfoMono>();

        InitCatColumns();
        InitDialogs();
    }
    private void InitCatColumns()
    {
        catColumnHandlers = new[]
        {
            panelEventExe.transform.Find("EventSlot").GetComponent<CatColumnHandler>(),
            panelEventExe.transform.Find("EventSlot1").GetComponent<CatColumnHandler>(),
            panelEventExe.transform.Find("EventSlot2").GetComponent<CatColumnHandler>(),
            panelEventExe.transform.Find("EventSlot3").GetComponent<CatColumnHandler>()
        };
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
        panelNPCInfo.SwitchNpcInfo(m_NPC);
    }

    public void ScaleRelationship(float scale)
    {
        scrollRelationShip.transform.localScale = new Vector3(scale, scale, 1.0f);
    }

    public void SetButtonCouncil(bool isActive)
    {
        buttonCouncilActive = isActive;
        buttonCouncil.SetActive(isActive);
    }
}
