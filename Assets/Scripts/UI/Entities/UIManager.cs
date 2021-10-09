using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using EmergencyInfo;
using HumanInfo;
using Logic;
using Logic.Event;
using Logic.Human;
using Manager;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: BaseModel<UIManager>, ISaveObject
{
    public bool buttonCouncilActive = true;

    public bool IsInit = false;
    
    public Transform gameView;
    public GameView gameViewMono;
    
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
    private Animation m_buttonCouncilAnimation;
    public GameObject buttonCouncilCatManage;
    public GameObject buttonCloseRelationship;
    public GameObject buttonRelationship;
    public GameObject buttonNormalTicker;
    public GameObject buttonSpeedTicker;
    
    //NPCs
    public Dictionary<long, GameObject> NPCEventCycles;

    //StartEventDialog
    public Transform textEventName_start;
    public Transform textEventDescription_start;
    public Transform imageEvent_start;
    public Transform imageEventIcon_start;
    public Transform textResultPreview_start;
    public Transform textEventCardTime_start;
    public Transform textEventCardName_start;
    
    public GameObject imageTarget_start;
    public GameObject imageParticipant_start;
    public GameObject imageParticipantCenter_start;

    public GameObject buttonStartEvent;
    public GameObject buttonStartEventDisabled;
    
    //FinsihEventDialog
    public Transform textEventName_finish;
    public Transform textEventDescription_finish;
    public Transform imageEvent_finish;
    public Transform imageEventIcon_finish;
    public Transform textResult_finish;
    public Transform buttonText_finish;
    
    //EmergencyDialog
    public Transform textEventName_emergency;
    public Transform textEventDescription_emergency;
    public Transform imageEvent_emergency;
    public Transform imageEventIcon_emergency;
    public Transform textResult_emergency;
    public Transform textEventCardTime_emergency;
    public Transform textEventCardName_emergency;
    public Transform Choice1_emergency;
    public Transform Choice2_emergency;
    public GameObject buttonChoice1;
    public GameObject buttonChoice1Disabled;
    public GameObject buttonChoice2;
    public GameObject buttonChoice2Disabled;
    
    //CatColumns
    public CatColumnHandler[] catColumnHandlers;
    public GameObject[] eventSlots;

    //Animation
    public GameObject EventPopAnimation;
    
    public EndingMono EndingMono;
    //music
    public AudioSource adplayer;

    

    public string Save()
    {
        var jsonString = JsonConvert.SerializeObject(buttonCouncilActive);
        return jsonString;
    }
    

    public void Load(string json)
    {
        SetButtonCouncil(JsonConvert.DeserializeObject<bool>(json));
    }

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

    public void PlayBGM(string path)
    {
        AudioClip m_clip = Resources.Load<AudioClip>(path);
        adplayer.clip = m_clip;
        adplayer.Play();
    }
    public void SwitchCatColumn(int index, bool bIsSwitchToShown)
    {
        catColumnHandlers[index].gameObject.SetActive(bIsSwitchToShown);
    }

    public void Init()
    {
        adplayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        darkBackgroundImage = gameView.Find("ScrollRelationship/Viewport/Content/BackgroundImageDark").gameObject;
        
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
        buttonNormalTicker = gameView.Find("PanelTickerButtons/ButtonNormal").gameObject;
        buttonSpeedTicker = gameView.Find("PanelTickerButtons/ButtonSpeed").gameObject;
        EventPopAnimation = gameView.Find("Animation/EventPopAnimation").gameObject;
        
        buttonCouncil = gameView.Find("ButtonCouncil").gameObject;
        m_buttonCouncilAnimation = buttonCouncil.GetComponent<Animation>();
        buttonCouncil.SetActive(buttonCouncilActive);

        panelNPCInfo = gameView.Find("PanelNPCInfo").GetComponent<NPCInfoMono>();

        EndingMono = gameView.Find("EndingPanel").GetComponent<EndingMono>();

        InitCatColumns();
        InitDialogs();
        IsInit = true;
    }
    private void InitCatColumns()
    {
        eventSlots = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            eventSlots[i] = panelEventExe.transform.Find("EventSlot" + i).gameObject;
        }
        catColumnHandlers = new[]
        {
            panelEventExe.transform.Find("EventSlot0").GetComponent<CatColumnHandler>(),
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
        imageEventIcon_start = imageEvent_start.Find("ImageEventIcon");
        textEventCardTime_start = imageEvent_start.Find("EventCard/TextEventCardTime");
        textEventCardName_start = imageEvent_start.Find("EventCard/TextEventCardName");
        textResultPreview_start = panelStartEventDialog.transform.Find("ImageResultPreview/TextResultPreview");
        
        imageTarget_start = panelStartEventDialog.transform.Find("ImageTarget").gameObject;
        imageParticipant_start = panelStartEventDialog.transform.Find("ImageParticipant").gameObject;
        imageParticipantCenter_start = panelStartEventDialog.transform.Find("ImageParticipantCenter").gameObject;
        buttonStartEvent = panelStartEventDialog.transform.Find("ButtonStartEvent").gameObject;
        buttonStartEventDisabled = panelStartEventDialog.transform.Find("ButtonStartEventDisabled").gameObject;
        
        //FinishEventDialog
        textEventName_finish = panelFinishEventDialog.transform.Find("TextEventName");;
        textEventDescription_finish = panelFinishEventDialog.transform.Find("TextEventDescription");
        imageEvent_finish = panelFinishEventDialog.transform.Find("ImageEvent");
        imageEventIcon_finish = imageEvent_finish.Find("ImageEventIcon");
        textResult_finish = panelFinishEventDialog.transform.Find("ImageResult/TextResult");
        buttonText_finish = panelFinishEventDialog.transform.Find("ButtonFinishEvent/Text");
        
        //EmergencyDialog
        textEventName_emergency = panelEmergencyDialog.transform.Find("TextEventName");
        textEventDescription_emergency = panelEmergencyDialog.transform.Find("TextEventDescription");
        imageEvent_emergency = panelEmergencyDialog.transform.Find("ImageEvent"); 
        imageEventIcon_emergency = imageEvent_emergency.Find("ImageEventIcon");
        textEventCardTime_emergency = imageEvent_emergency.Find("EventCard/TextEventCardTime");
        Choice1_emergency = panelEmergencyDialog.transform.Find("ImageChoice1");
        Choice2_emergency = panelEmergencyDialog.transform.Find("ImageChoice2");
        buttonChoice1 = Choice1_emergency.Find("ButtonChoice").gameObject;
        buttonChoice1Disabled = Choice1_emergency.Find("ButtonChoiceDisabled").gameObject;
        buttonChoice2 = Choice2_emergency.Find("ButtonChoice").gameObject;
        buttonChoice2Disabled = Choice2_emergency.Find("ButtonChoiceDisabled").gameObject;
    }
    public void InitStartEventDialog(CatEvent m_myCatEventInfo)
    {
        SwitchDarkBackGround(true);
        SwitchTickerButtons(false);
        panelStartEventDialog.SetActive(true);
        textEventName_start.GetComponent<Text>().text = m_myCatEventInfo.Name;
        textEventDescription_start.GetComponent<Text>().text = m_myCatEventInfo.UpDesc;
        imageEvent_start.GetComponent<Image>().sprite = FindEventCard(m_myCatEventInfo.Type);
        imageEventIcon_start.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Sprites/Cards/" + m_myCatEventInfo.ImageIn);
        textResultPreview_start.GetComponent<Text>().text = m_myCatEventInfo.DownDesc;
        bool canExecute = m_myCatEventInfo.CanExecute();
        buttonStartEvent.SetActive(canExecute);
        buttonStartEventDisabled.SetActive(!canExecute);
        textEventCardTime_start.GetComponent<Text>().text = (m_myCatEventInfo.ConsumeTime * 10) + "min";
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
            var eventHandler = EventHandlerManager.Instance.GetHandlerByEventID(m_myCatEventInfo.ID);
            imageParticipantCenter_start.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PortraitsNoColor/" + eventHandler.GetCat().Image);
            imageTarget_start.SetActive(false);
            imageParticipant_start.SetActive(false);
            imageParticipantCenter_start.SetActive(true);
        }
        else
        {
            GameObject.Find("Canvas").GetComponent<UtilsMath>().WriteToFile("走到了human id != 0");
            var eventHandler = EventHandlerManager.Instance.GetHandlerByEventID(m_myCatEventInfo.ID);
            imageParticipant_start.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PortraitsNoColor/" + eventHandler.GetCat().Image);
            imageTarget_start.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/PortraitsNoColor/" + HumanInfoLoader.Instance.Findperson(m_myCatEventInfo.HumanId).Image);
            imageTarget_start.SetActive(true);
            imageParticipant_start.SetActive(true);
            imageParticipantCenter_start.SetActive(false);
        }
    }

    public void InitEmergencyDialog(Emergency m_myEmergencyInfo, long cacheTime)
    {
        SwitchDarkBackGround(true);
        SwitchTickerButtons(false);
        panelEmergencyDialog.SetActive(true);
        textEventName_emergency.GetComponent<Text>().text = m_myEmergencyInfo.Name;
        textEventDescription_emergency.GetComponent<Text>().text = m_myEmergencyInfo.Description;
        textEventCardTime_emergency.GetComponent<Text>().text = (cacheTime * 10) + "min";
        imageEvent_emergency.GetComponent<Image>().sprite = FindEventCard(m_myEmergencyInfo.Type);
        imageEventIcon_emergency.GetComponent<Image>().sprite =
            Resources.Load<Sprite>("Sprites/Cards/" + m_myEmergencyInfo.Picture);
        List<EmergencyInfoConfig.Types.Option> optionList = m_myEmergencyInfo.GetOptions();
        Choice1_emergency.Find("TextChoice").GetComponent<Text>().text = optionList[0].Description;
        Choice1_emergency.Find("ButtonChoice/Text").GetComponent<Text>().text = optionList[0].Name;
        Choice2_emergency.Find("TextChoice").GetComponent<Text>().text = optionList[1].Description;
        Choice2_emergency.Find("ButtonChoice/Text").GetComponent<Text>().text = optionList[1].Name;
        bool bCanChoose1 = m_myEmergencyInfo.OptionCanChoose(1);
        buttonChoice1.SetActive(bCanChoose1);
        buttonChoice1Disabled.SetActive(!bCanChoose1);
        bool bCanChoose2 = m_myEmergencyInfo.OptionCanChoose(2);
        buttonChoice2.SetActive(bCanChoose2);
        buttonChoice2Disabled.SetActive(!bCanChoose2);
    }
    
    public void InitFinishEventDialog(ResultEventInfo.ResultEventInfo.Types.ResultEventItem m_myResultInfo)
    {
        SwitchDarkBackGround(true);
        SwitchTickerButtons(false);
        panelFinishEventDialog.SetActive(true);
        textEventName_finish.GetComponent<Text>().text = m_myResultInfo.Name;
        textEventDescription_finish.GetComponent<Text>().text = m_myResultInfo.Description1;
        imageEvent_finish.GetComponent<Image>().sprite = FindEventCard(m_myResultInfo.Type);
        imageEventIcon_finish.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Cards/" + m_myResultInfo.Picture);
        textResult_finish.GetComponent<Text>().text = m_myResultInfo.Description2;
        //buttonText_finish.GetComponent<Text>().text = m_myResultInfo.BtnTxt;
    }
    
    public void RefreshEventSlots()
    {
        for (int i = 0; i < 4; i++)
        {
            eventSlots[i].SetActive(EventHandlerManager.Instance.GetHandlerByIndex(i).GetValid());
        }
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

        if (isActive)
        {
            var inAni = m_buttonCouncilAnimation["ButtonCouncilIn"];
            inAni.speed = 1;
            inAni.normalizedTime = 0;

            inAni.enabled = false;
            m_buttonCouncilAnimation.Sample();
            m_buttonCouncilAnimation.Play("ButtonCouncilIn");
        }
    }

    public void SwitchTickerButtons(bool bIsShown)
    {
        buttonNormalTicker.SetActive(bIsShown);
        buttonSpeedTicker.SetActive(bIsShown);
    }
    public Sprite FindEventCard(int type)
    {
        switch (type)
        {
            case 0:
                return Resources.Load<Sprite>("Sprites/Cards/特殊事件");
            case 1:
                return Resources.Load<Sprite>("Sprites/Cards/探查事件");
            case 2:
                return Resources.Load<Sprite>("Sprites/Cards/密谋事件");
            case 3:
                return Resources.Load<Sprite>("Sprites/Cards/交际事件");
        }
        return null;
    }
}
