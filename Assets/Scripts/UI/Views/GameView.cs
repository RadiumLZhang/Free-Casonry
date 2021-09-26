using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Logic;
using Manager;
using UnityEngine.Android;
using Random = UnityEngine.Random;

public class GameView : MonoBehaviour
{
    private GameObject darkBackgroundImage;
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

    //左上角时间
    private Text textDate;
    private Text textTime;
    
    private Coroutine openExePanel_coroutine;
    private Coroutine closeExePanel_coroutine;

    // 游戏当前运行速度，用于设置当中的继续游戏
    private TICKER_SPEED_ENUM speedEnum;
    void Start()
    {
        darkBackgroundImage = transform.Find("DarkBackgroundImage").gameObject;
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
    }

    private void UIManagerInit()
    {
        UIManager.Instance.darkBackgroundImage = transform.Find("DarkBackgroundImage").gameObject;
        UIManager.Instance.panelCouncil = transform.Find("PanelCouncil").gameObject;
        UIManager.Instance.panelSettings = transform.Find("PanelSettings").gameObject;
        UIManager.Instance.panelEventExe = transform.Find("PanelEventExe").gameObject;
        UIManager.Instance.panelResources = transform.Find("PanelResources").gameObject;
        UIManager.Instance.scrollSpecialEvent = transform.Find("ScrollSpecialEvent").gameObject;
        UIManager.Instance.buttonOpenExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
        UIManager.Instance.buttonCloseExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").gameObject;
    }
    public void ButtonTestEvent_OnClick()
    {
        Transform temp = Instantiate(specialEventPrefab).transform;
        //TODO:把这个random重写成你的事件生成方法（带ID），其余不动
        if (Random.value > 0.5f)
        {
            temp.GetComponent<SpecialEventMono>().InitWithID(1000);
        }
        else
        {
            temp.GetComponent<SpecialEventMono>().InitWithID(1000);
        }
        temp.SetParent(contentTransform);
        temp.localPosition = Vector3.zero;
        temp.localRotation = Quaternion.identity;
        temp.localScale = Vector3.one;
    }

    public void ButtonSettings_OnClick()
    {
        speedEnum = TimeTickerManager.Instance.GetSpeed();
        TimeTickerManager.Instance.StopTick();
        darkBackgroundImage.SetActive(true);
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
        TimeTickerManager.Instance.StartTickWithSpeed(TICKER_SPEED_ENUM.FAST);
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
        TimeTickerManager.Instance.StartTickWithSpeed(speedEnum);
        darkBackgroundImage.SetActive(false);
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
