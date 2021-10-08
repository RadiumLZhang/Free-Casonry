using System;
using System.Collections;
using System.Collections.Generic;
using Language;
using Logic;
using Logic.Conspiracy;
using UnityEngine;
using UnityEngine.UI;

public class CouncilView : MonoBehaviour
{
    private enum CouncilState
    {
        Conspiracy,
        Manage
    }
    
    private GameObject panelConspiracy;
    private GameObject panelManage;
    
    //ImageLights
    private GameObject imageConspiracyOff;
    private GameObject imageConspiracyOn;
    private GameObject imageManageOff;
    private GameObject imageManageOn;

    private Animation m_conspiracyPanelAnimation;
    private Animation m_managePanelAnimation;

    //阴谋panel
    private Text TextConspiracyName;
    private Text TextConspiracyDetail;
    public Image[] ImageConspiracyButtons;
    public Image ImageConspiracyFinalButton;
    public Sprite conspiracySprite;
    public Sprite conspiracyFinalSprite;
    public Sprite conspiracySpriteChosen;
    public Sprite conspiracyFinalSpriteChosen;
    
    //管理panel
    private Text TextCatName;
    private Text TextCatID;
    private Text TextCatCategory;
    private Transform ImageCatState;
    private Text TextCatState;
    private Text TextScoutValue;
    private Text TextConspiracy;
    private Text TextCommunication;
    private Text TextSkill;
    private Text TextBiography;
    
    private CouncilState state;
    

    void Start()
    {
        panelConspiracy = transform.Find("PanelConspiracy").gameObject;
        panelManage = transform.Find("PanelManage").gameObject;
        imageConspiracyOff = transform.Find("ImageConspiracyOff").gameObject;
        imageConspiracyOn = transform.Find("ImageConspiracyOn").gameObject;
        imageManageOff = transform.Find("ImageManageOff").gameObject;
        imageManageOn = transform.Find("ImageManageOn").gameObject;
        
        Transform manageDetailPanel = panelManage.transform.Find("animationRoot/ImageCatDetail");
        TextCatName = manageDetailPanel.Find("TextCatName").GetComponent<Text>();
        TextCatID = manageDetailPanel.Find("TextCatID").GetComponent<Text>();
        TextCatCategory = manageDetailPanel.Find("TextCatCategory").GetComponent<Text>();
        ImageCatState = manageDetailPanel.Find("ImageCatState");
        TextCatState = ImageCatState.transform.Find("TextCatState").GetComponent<Text>();
        TextScoutValue = manageDetailPanel.Find("TextScoutValue").GetComponent<Text>();
        TextConspiracy = manageDetailPanel.Find("TextConspiracy").GetComponent<Text>();
        TextCommunication = manageDetailPanel.Find("TextCommunication").GetComponent<Text>();
        TextSkill = manageDetailPanel.Find("TextSkill").GetComponent<Text>();
        TextBiography = manageDetailPanel.Find("TextBiography").GetComponent<Text>();

        Transform conspiracyDetailPanel = panelConspiracy.transform.Find("animationRoot/PanelConspiracyInfo");
        TextConspiracyName = conspiracyDetailPanel.Find("TextConspiracyName").GetComponent<Text>();
        TextConspiracyDetail = conspiracyDetailPanel.Find("TextConspiracyDetail").GetComponent<Text>();
        ImageConspiracyButtons =  panelConspiracy.transform.Find("animationRoot/ImageTarget/ButtonsConspiracy").GetComponentsInChildren<Image>();
        ImageConspiracyFinalButton = panelConspiracy.transform.Find("animationRoot/ImageTarget/ButtonsConspiracy/ButtonConspiracyFinal").GetComponent<Image>();
        // conspiracySprite = Resources.Load<Sprite>("Sprites/Council/猫咪阴谋/3目标节点（不亮）.png");
        // conspiracyFinalSprite = Resources.Load<Sprite>("Sprites/Council/猫咪阴谋/3目标最终节点（不亮）.png");
        // conspiracySpriteChosen = Resources.Load<Sprite>("Sprites/Council/猫咪阴谋/3目标节点（点亮）.png");
        // conspiracyFinalSpriteChosen = Resources.Load<Sprite>("Sprites/Council/猫咪阴谋/3目标最终节点（点亮）.png");

        m_conspiracyPanelAnimation = panelConspiracy.GetComponent<Animation>();
        m_managePanelAnimation = panelManage.GetComponent<Animation>();

        state = CouncilState.Manage;
    }

    public void SwitchConspiracyButton()
    {
        foreach (Image child in ImageConspiracyButtons)
        {
            child.sprite = conspiracySprite;
        }

        ImageConspiracyFinalButton.sprite = conspiracyFinalSprite;

    }
    private void SwitchLightToConspiracy(bool bIsConspiracy)
    {
        imageConspiracyOn.SetActive(bIsConspiracy);
        imageManageOff.SetActive(bIsConspiracy);
        imageManageOn.SetActive(!bIsConspiracy);
        imageConspiracyOff.SetActive(!bIsConspiracy);
    }
    public void ButtonConspiracy_OnClick()
    {
        if (state == CouncilState.Conspiracy)
        {
            return;
        }

        state = CouncilState.Conspiracy;
        
        SwitchLightToConspiracy(true);
        panelConspiracy.SetActive(true);
        panelManage.SetActive(false);
        
        PanelInAnimation(m_conspiracyPanelAnimation);
        PanelOutAnimation(m_managePanelAnimation);
    }
    public void ButtonManage_OnClick()
    {
        if (state == CouncilState.Manage)
        {
            return;
        }

        state = CouncilState.Manage;
        
        SwitchLightToConspiracy(false);
        panelConspiracy.SetActive(false);
        panelManage.SetActive(true);

        PanelInAnimation(m_managePanelAnimation);
        PanelOutAnimation(m_conspiracyPanelAnimation);
    }

    private void PanelInAnimation(Animation animation)
    {
        var panelIn = animation["PanelIn"];
        panelIn.speed = 1;
        panelIn.normalizedTime = 0;
        panelIn.enabled = false;

        animation.Sample();
        animation.Play(panelIn.name);
    }

    private void PanelOutAnimation(Animation animation)
    {
        var panelIn = animation["PanelIn"];
        panelIn.speed = -1;
        panelIn.normalizedTime = 1;
        panelIn.enabled = false;

        animation.Sample();
        animation.Play(panelIn.name);
    }

    public void SwitchConspiracy(Conspiracy conspiracy)
    {
        TextConspiracyName.text = conspiracy.Desc;
        TextConspiracyDetail.text = conspiracy.Desc;
    }
    public void SwitchCatDisplay(Cat cat)
    {
        TextCatName.text = cat.Name;
        TextCatID.text = cat.ID.ToString();
        TextCatCategory.text = cat.Type;
        var stateTextItem = LanguageLoader.Instance.FindLanguageItem(cat.CatState.ToString());
        if(stateTextItem != null)
            TextCatState.text = stateTextItem.Value;
        ImageCatState.gameObject.SetActive(TextCatState.text != "");
        TextScoutValue.text = cat.ScoutValue.ToString();
        TextConspiracy.text = cat.Conspiracy.ToString();
        TextCommunication.text = cat.Communication.ToString();
        var skillTextItem = LanguageLoader.Instance.FindLanguageItem(cat.CatState.ToString());
        if(skillTextItem != null)
            TextSkill.text = skillTextItem.Value;
        TextBiography.text = cat.Description;
    }
}
