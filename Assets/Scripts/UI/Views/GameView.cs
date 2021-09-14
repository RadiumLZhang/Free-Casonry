using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Logic;
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
            temp.GetComponent<SpecialEventMono>().InitWithID(1);
        }
        else
        {
            temp.GetComponent<SpecialEventMono>().InitWithID(2);
        }
        temp.SetParent(contentTransform);
        temp.localPosition = Vector3.zero;
        temp.localRotation = Quaternion.identity;
        temp.localScale = Vector3.one;
    }

    public void ButtonSettings_OnClick()
    {
        //TODO:Ticker暂停游戏
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
        //TODO:Ticker暂停游戏
    }
    
    public void ButtonNormal_OnClick()
    {
        //TODO:Ticker正常
    }
    
    public void ButtonSpeed_OnClick()
    {
        //TODO:Ticker加速
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
        //TODO:Ticker恢复到正常/加速
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
        rectExePanel.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right,0,rectExePanel.sizeDelta.x);
    }
    public void CloseExePanel()
    {
        buttonOpenExePanel.gameObject.SetActive(true);
        buttonCloseExePanel.gameObject.SetActive(false);
        rectExePanel.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right,-rectExePanel.sizeDelta.x/2.0f,rectExePanel.sizeDelta.x);
    }

    public void UpdatePanelResources()
    {
        panelResources.transform.Find("TextMoney").GetComponent<Text>().text = "" + PlayerModel.Instance.Money;
        panelResources.transform.Find("TextInfluence").GetComponent<Text>().text = "" + PlayerModel.Instance.Influence;
        panelResources.transform.Find("TextCohesion").GetComponent<Text>().text = "" + PlayerModel.Instance.Cohesion;
    }
}
