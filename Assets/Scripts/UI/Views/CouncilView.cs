using System;
using System.Collections;
using System.Collections.Generic;
using CatConspiracyInfo;
using Language;
using Logic;
using Logic.Condition;
using Logic.Conspiracy;
using Manager;
using TotalConspiracy;
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
    public GameObject[] conspiracyRequirements;

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
    private GameObject[] ImageCats;
    
    
    //Btns
    private GameObject buttonConspiracyChosen;
    private GameObject buttonManageChosen;
    public GameObject StartConspiracyButton;

    private CouncilState state;
    public AudioSource adplayer;

    void Awake()
    {
        adplayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
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
        ImageCats = new GameObject[4];
        Transform catList = panelManage.transform.Find("animationRoot/ScrollListCats/Viewport/Content");
        for (int i = 0; i < 4; i++)
        {
            ImageCats[i] = catList.Find("Image" + i + "/ImageCat").gameObject;
        }

        Transform conspiracyDetailPanel = panelConspiracy.transform.Find("animationRoot/PanelConspiracyInfo");
        TextConspiracyName = conspiracyDetailPanel.Find("TextConspiracyName").GetComponent<Text>();
        TextConspiracyDetail = conspiracyDetailPanel.Find("TextConspiracyDetail").GetComponent<Text>();
        ImageConspiracyButtons =  panelConspiracy.transform.Find("animationRoot/ImageTarget/ButtonsConspiracy").GetComponentsInChildren<Image>();
        ImageConspiracyFinalButton = panelConspiracy.transform.Find("animationRoot/ImageTarget/ButtonsConspiracy/ButtonConspiracyFinal").GetComponent<Image>();
        Transform conspiracyRequirementPanel = conspiracyDetailPanel.Find("ImageRequirement");
        conspiracyRequirements = new GameObject[4];
        conspiracyRequirements[0] = conspiracyRequirementPanel.Find("Requirement1").gameObject;
        conspiracyRequirements[1] = conspiracyRequirementPanel.Find("Requirement2").gameObject;
        conspiracyRequirements[2] = conspiracyRequirementPanel.Find("Requirement3").gameObject;
        conspiracyRequirements[3] = conspiracyRequirementPanel.Find("Requirement4").gameObject;

        m_conspiracyPanelAnimation = panelConspiracy.GetComponent<Animation>();
        m_managePanelAnimation = panelManage.GetComponent<Animation>();

        buttonConspiracyChosen = transform.Find("ButtonConspiracyChosen").gameObject;
        buttonManageChosen = transform.Find("ButtonManageChosen").gameObject;
        
        StartConspiracyButton.GetComponent<ButtonConspiracy>().RefreshConspiracy();
        state = CouncilState.Manage;
    }

    private void OnEnable()
    {
        RefreshCatList();
    }

    public void RefreshCatList()
    {
        for (int i = 0; i < 4; i++)
        {
            ImageCats[i].SetActive(EventHandlerManager.Instance.GetHandlerByIndex(i).GetValid());
        }
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
    
    // 猫咪阴谋
    public void ButtonConspiracy_OnClick()
    {
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "事件完成-暗黑地牢");
        adplayer.clip = m_clip;
        adplayer.Play();
        
        if (state == CouncilState.Conspiracy)
        {
            return;
        }

        state = CouncilState.Conspiracy;
        buttonManageChosen.SetActive(false);
        buttonConspiracyChosen.SetActive(true);
        SwitchLightToConspiracy(true);
        panelConspiracy.SetActive(true);
        panelManage.SetActive(false);
        
        PanelInAnimation(m_conspiracyPanelAnimation);
        PanelOutAnimation(m_managePanelAnimation);
    }
    
    // 猫咪管理
    public void ButtonManage_OnClick()
    {
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/主界面/" + "事件完成-暗黑地牢");
        adplayer.clip = m_clip;
        adplayer.Play();
        
        if (state == CouncilState.Manage)
        {
            return;
        }

        state = CouncilState.Manage;
        buttonManageChosen.SetActive(true);
        buttonConspiracyChosen.SetActive(false);
        SwitchLightToConspiracy(false);
        panelConspiracy.SetActive(false);
        panelManage.SetActive(true);
        RefreshCatList();

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

    public void SwitchConspiracy(TotalConspiracy.TotalConspiracy.Types.TotalConspiracyItem conspiracy)
    {
        foreach (var item in conspiracyRequirements)
        {
            item.SetActive(false);
        }
        TextConspiracyName.text = conspiracy.ConspiracyTitle;
        TextConspiracyDetail.text = conspiracy.ConspiracyDesc;
        switch (conspiracy.ConspiracyId.Count)
        {
            case 1:
                RefreshRequirement(conspiracy, 0);
                break;
            case 2:
                RefreshRequirement(conspiracy, 0);
                RefreshRequirement(conspiracy, 1);
                break;
            case 3:
                RefreshRequirement(conspiracy, 0);
                RefreshRequirement(conspiracy, 1);
                RefreshRequirement(conspiracy, 2);
                break;
            case 4:
                RefreshRequirement(conspiracy, 0);
                RefreshRequirement(conspiracy, 1);
                RefreshRequirement(conspiracy, 2);
                RefreshRequirement(conspiracy, 3);
                break;
        }
    }

    private void RefreshRequirement(TotalConspiracy.TotalConspiracy.Types.TotalConspiracyItem conspiracy,
        int requirementIndex)
    {
        var conspiracyInfo = CatConspiracyInfoLoader.Instance.FindCatConspiracyItem(conspiracy.ConspiracyId[requirementIndex]);
        conspiracyRequirements[requirementIndex].SetActive(true);
        conspiracyRequirements[requirementIndex].transform.Find("ImageDone").gameObject.SetActive(ConditionUtils.CheckCondition(conspiracyInfo.Condition));
        conspiracyRequirements[requirementIndex].transform.Find("TextRequirement").GetComponent<Text>().text =
            conspiracyInfo.Description;
    }

    public void SwitchCatDisplay(Cat cat, int index)
    {
        if (EventHandlerManager.Instance.GetHandlerByIndex(index).GetValid())
        {
            TextCatName.text = cat.Name;
            TextCatID.text = cat.ID.ToString();
            TextCatCategory.text = cat.Type;
            ImageCatState.gameObject.SetActive(false);
            if (cat.CatState != 0)
            {
                ImageCatState.gameObject.SetActive(true);
                var stateTextItem = LanguageLoader.Instance.FindLanguageItem(cat.CatState.ToString());
                if(stateTextItem != null)
                    TextCatState.text = stateTextItem.Value;
            }
            TextScoutValue.text = cat.ScoutValue.ToString();
            TextConspiracy.text = cat.Conspiracy.ToString();
            TextCommunication.text = cat.Communication.ToString();
            var skillTextItem = LanguageLoader.Instance.FindLanguageItem(cat.CatState.ToString());
            if(skillTextItem != null)
                TextSkill.text = skillTextItem.Value;
            TextBiography.text = cat.Description;
        }
        else
        {
            TextCatName.text = "未解锁";
            TextCatID.text = "??????";
            TextCatCategory.text = "未知";
            ImageCatState.gameObject.SetActive(false);
            TextScoutValue.text = "?";
            TextConspiracy.text = "?";
            TextCommunication.text = "?";
            TextSkill.text = "未知";
            TextBiography.text = "";
        }
        
    }
}
