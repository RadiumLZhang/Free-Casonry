using System;
using System.Collections;
using System.Collections.Generic;
using EventHandler;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Logic;
using Logic.Event;
using Manager;
using UnityEngine.Android;
using Random = UnityEngine.Random;

public class GameView : MonoBehaviour
{
    private GameObject panelCouncil;
    private GameObject panelSettings;
    private GameObject panelEventExe;
    private GameObject panelResources;
    private Text textMoney;
    private Text textInfluence;
    private Text textHidency;
    private GameObject scrollSpecialEvent;
    private RectTransform contentTransform;
    private Button buttonOpenExePanel;
    private Button buttonCloseExePanel;
    private RectTransform rectExePanel;
    private RectTransform specialEventPrefab;
    public long currentDialogEventID;

    //左上角时间
    private Text textDate;
    private Text textTime;
    
    private Coroutine openExePanel_coroutine;
    private Coroutine closeExePanel_coroutine;

    public GameObject DroppedImage;
    void Start()
    {
        panelCouncil = transform.Find("PanelCouncil").gameObject;
        panelSettings = transform.Find("PanelSettings").gameObject;
        panelEventExe = transform.Find("PanelEventExe").gameObject;
        panelResources = transform.Find("PanelResources").gameObject;
        textMoney = panelResources.transform.Find("TextMoney").GetComponent<Text>();
        textInfluence = panelResources.transform.Find("TextInfluence").GetComponent<Text>();
        textHidency = panelResources.transform.Find("TextHidency").GetComponent<Text>();
        scrollSpecialEvent = transform.Find("ScrollSpecialEvent").gameObject;
        specialEventPrefab = Resources.Load<RectTransform>("Prefabs/SpecialEvent");
        
        //左上角时间
        textDate = transform.Find("ImageTime").Find("TextDate").GetComponent<Text>();
        textTime = transform.Find("ImageTime").Find("TextTime").GetComponent<Text>();
        
        buttonOpenExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").GetComponent<Button>();
        buttonCloseExePanel = panelEventExe.transform.Find("ButtonCloseExePanel").GetComponent<Button>();
        rectExePanel = panelEventExe.GetComponent<RectTransform>();
        contentTransform = scrollSpecialEvent.transform.Find("Viewport").Find("ContentSpecialEvent").GetComponent<RectTransform>();
        
        UIManagerInit();
        EventHandlerManager.Instance.InitMono(panelEventExe.transform);
    }

    private void UIManagerInit()
    {
        UIManager.Instance.darkBackgroundImage = transform.Find("ScrollRelationship/Viewport/Content/BackgroundImage/ImageBackgroundDark").gameObject;
        UIManager.Instance.panelCouncil = transform.Find("PanelCouncil").gameObject;
        UIManager.Instance.panelSettings = transform.Find("PanelSettings").gameObject;
        UIManager.Instance.panelEventExe = transform.Find("PanelEventExe").gameObject;
        UIManager.Instance.panelResources = transform.Find("PanelResources").gameObject;
        UIManager.Instance.panelStartEventDialog = transform.Find("PanelStartEventDialog").gameObject;
        UIManager.Instance.panelEmergencyDialog = transform.Find("PanelEmergencyDialog").gameObject;
        UIManager.Instance.panelFinishEventDialog = transform.Find("PanelFinishEventDialog").gameObject;
        UIManager.Instance.scrollSpecialEvent = transform.Find("ScrollSpecialEvent").gameObject;
        UIManager.Instance.buttonOpenExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
        UIManager.Instance.buttonCloseExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
        UIManager.Instance.buttonCouncil = transform.Find("ButtonCouncil").gameObject;
        UIManager.Instance.buttonCouncilCatManage = panelCouncil.transform.Find("ButtonManage").gameObject;
        UIManager.Instance.InitCatColumns();
        UIManager.Instance.InitDialogs();
    }
    public void ButtonTestEvent_OnClick()
    {
        
        RefreshScrollSpecialEvent();

        // foreach (var item in list)
        // {
        //     Transform temp = Instantiate(specialEventPrefab).transform;
        //     temp.GetComponent<SpecialEventMono>().InitWithID(item.ID);
        //     temp.SetParent(contentTransform);
        //     temp.localPosition = Vector3.zero;
        //     temp.localRotation = Quaternion.identity;
        //     temp.localScale = Vector3.one;
        // }
    }

    public void ButtonSettings_OnClick()
    {
        TimeTickerManager.Instance.StopTick();
        UIManager.Instance.SwitchDarkBackGround(true);
        panelSettings.SetActive(true);
    }
    public void ButtonRelationship_OnClick()
    {
        
    }
    public void ButtonCouncil_OnClick()
    {
        panelCouncil.SetActive(true);
    }
    
    public void ButtonPause_OnClick()
    {
        TimeTickerManager.Instance.StopTick();
    }
    
    public void ButtonNormal_OnClick()
    {
        TimeTickerManager.Instance.StartTick();
    }
    
    public void ButtonSpeed_OnClick()
    {
        TimeTickerManager.Instance.StartTickWithSpeed(TickerSpeedEnum.Fast);
    }
    
    //Button in Council
    public void ButtonBacktoGame_OnClick()
    {
        panelCouncil.SetActive(false);
    }

    //Buttons in Settings
    public void ButtonSaveQuit_OnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void ButtonResume_OnClick()
    {
        TimeTickerManager.Instance.Restore();
        UIManager.Instance.SwitchDarkBackGround(false);
        panelSettings.SetActive(false);
    }
    
    //Buttons in Event Exe
    public void ButtonOpenExePanel_OnClick()
    {
        OpenExePanel();
    }
    public void ButtonCloseExePanel_OnClick()
    {
        CloseExePanel();
    }

    public void ButtonStartEvent_OnClick()
    {
        //TODO：把原来的拖入响应事件放到这里
        UIManager.Instance.panelStartEventDialog.SetActive(false);
        var eventHandler = EventHandlerManager.Instance.GetHandlerByEventID(currentDialogEventID);
        eventHandler.OnPostInit(currentDialogEventID);
    }
    
    public void ButtonCloseEventDialog_OnClick()
    {
        UIManager.Instance.panelStartEventDialog.SetActive(false);
        var eventHandler = EventHandlerManager.Instance.GetHandlerByEventID(currentDialogEventID);
        DroppedImage.SetActive(true);
        DroppedImage.GetComponent<DragHandlerSpecialEvent>().EndDrag();
        eventHandler.OnDestroyEvent();
    }
    public void ButtonFinishDialog_OnClick()
    {
        var eventHandler = EventHandlerManager.Instance.GetHandlerByEventID(currentDialogEventID);
        eventHandler.OnPostFinish();
        UIManager.Instance.panelFinishEventDialog.SetActive(false);
    }

    public void ButtonEmergencyDialogChoice1_OnClick()
    {
        var eventHandler = EventHandlerManager.Instance.GetHandlerByEventID(currentDialogEventID);
        eventHandler.OnPostEmergency(1);
    }

    public void ButtonEmergencyDialogChoice2_OnClick()
    {
        var eventHandler = EventHandlerManager.Instance.GetHandlerByEventID(currentDialogEventID);
        eventHandler.OnPostEmergency(2);
    }
    public void OpenExePanel()
    {
        buttonOpenExePanel.gameObject.SetActive(false);
        buttonCloseExePanel.gameObject.SetActive(true);
        if(openExePanel_coroutine != null) StopCoroutine(openExePanel_coroutine);
        if(closeExePanel_coroutine != null) StopCoroutine(closeExePanel_coroutine);
        openExePanel_coroutine = StartCoroutine(ExePanelCoroutine(true));
    }
    public void CloseExePanel()
    {
        buttonOpenExePanel.gameObject.SetActive(true);
        buttonCloseExePanel.gameObject.SetActive(false);
        if(openExePanel_coroutine != null) StopCoroutine(openExePanel_coroutine);
        if(closeExePanel_coroutine != null) StopCoroutine(closeExePanel_coroutine);
        closeExePanel_coroutine = StartCoroutine(ExePanelCoroutine(false));
    }

    public void RefreshScrollSpecialEvent()
    {
        var events = EventManager.Instance.GetCommonEventList();
        var originObjCount = contentTransform.childCount;
        var n = Math.Min(events.Count, originObjCount);
        //刷新现有的
        for (var i = 0; i < n; i++)
        {
            contentTransform.GetChild(i).GetComponent<SpecialEventMono>().InitWithID(events[i].ID);
        }

        //增加新增的
        for (var i = n; i < events.Count; i++)
        {
            var obj = Instantiate(specialEventPrefab).transform;
            obj.GetComponent<SpecialEventMono>().InitWithID(events[i].ID);
            obj.SetParent(contentTransform);
            obj.localPosition = Vector3.zero;
            obj.localRotation = Quaternion.identity;
            obj.localScale = Vector3.one;
        }
        
        //销毁多余的
        for (var i = n; i < originObjCount; i++) {  
            Destroy (contentTransform.GetChild (i).gameObject);  
        }  
    }
    
    void Update()
    {
        UpdatePanelResources();
        UpdateTime();
    }
    public void UpdatePanelResources()
    {
        textMoney.text = "" + PlayerModel.Instance.Money;
        textInfluence.text = "" + PlayerModel.Instance.Influence;
        textHidency.text = "" + PlayerModel.Instance.Hidency;
    }

    public void UpdateTime()
    {
        System.DateTime now = TimeManager.Instance.GetTime();
        uint timeHour = (uint)now.Hour;
        uint timeMinute = (uint)now.Minute;
        uint timeMonth = (uint)now.Month;
        uint timeDate = (uint)now.Day;

        textTime.text = (timeHour < 10 ? "0" + timeHour : timeHour.ToString()) + ":" + (timeMinute < 10 ? "0" + timeMinute : timeMinute.ToString());
        textDate.text = timeMonth + "月" + timeDate + "日" + " "+ (timeHour > 6 && timeHour < 18 ? "昼" : "夜");
    }
    IEnumerator ExePanelCoroutine(bool bIsOpen)
    {
        float openPos = -rectExePanel.sizeDelta.x / 2.0f;
        float curPos = bIsOpen ? openPos : 0;
        for (float i = 0f; i < 0.3f; i += Time.deltaTime)
        {
            if (bIsOpen)
            {
                curPos -= openPos * Time.deltaTime * 3.33f;
                if (curPos > 0) curPos = 0;
            }
            else
            {
                curPos += openPos * Time.deltaTime * 3.33f;
                if (curPos < openPos) curPos = openPos;
            }
            rectExePanel.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right,curPos,rectExePanel.sizeDelta.x);
            yield return null;
        }
    }
}
