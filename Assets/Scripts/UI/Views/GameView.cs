using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Logic;
using Random = UnityEngine.Random;

public class GameView : MonoBehaviour
{
    private GameObject darkBackgroundImage;
    private GameObject panelCouncil;
    private GameObject panelSettings;
    private GameObject panelEventExe;
    private GameObject panelResources;
    private GameObject scrollSpecialEvent;
    private RectTransform contentTransform;
    private Button buttonOpenExePanel;
    private Button buttonCloseExePanel;
    private RectTransform rectExePanel;
    private RectTransform specialEventPrefab;
    
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
        scrollSpecialEvent = transform.Find("ScrollSpecialEvent").gameObject;
        specialEventPrefab = Resources.Load<RectTransform>("Prefabs/SpecialEvent");
        
        buttonOpenExePanel = panelEventExe.transform.Find("ButtonOpenExePanel").GetComponent<Button>();
        buttonCloseExePanel = panelEventExe.transform.Find("ButtonCloseExePanel").GetComponent<Button>();
        rectExePanel = panelEventExe.GetComponent<RectTransform>();
        contentTransform = scrollSpecialEvent.transform.Find("Viewport").Find("ContentSpecialEvent").GetComponent<RectTransform>();
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
    }
    public void UpdatePanelResources()
    {
        panelResources.transform.Find("TextMoney").GetComponent<Text>().text = "" + PlayerModel.Instance.Money;
        panelResources.transform.Find("TextInfluence").GetComponent<Text>().text = "" + PlayerModel.Instance.Influence;
        panelResources.transform.Find("TextCohesion").GetComponent<Text>().text = "" + PlayerModel.Instance.Hidency;
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
