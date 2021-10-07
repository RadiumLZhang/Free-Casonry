using System;
using System.Collections;
using System.Collections.Generic;
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

    private CouncilState state;
    

    void Start()
    {
        panelConspiracy = transform.Find("PanelConspiracy").gameObject;
        panelManage = transform.Find("PanelManage").gameObject;
        imageConspiracyOff = transform.Find("ImageConspiracyOff").gameObject;
        imageConspiracyOn = transform.Find("ImageConspiracyOn").gameObject;
        imageManageOff = transform.Find("ImageManageOff").gameObject;
        imageManageOn = transform.Find("ImageManageOn").gameObject;

        m_conspiracyPanelAnimation = panelConspiracy.GetComponent<Animation>();
        m_managePanelAnimation = panelManage.GetComponent<Animation>();

        state = CouncilState.Manage;
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
        // panelConspiracy.SetActive(true);
        // panelManage.SetActive(false);
        
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
        
        // SwitchLightToConspiracy(false);
        // panelConspiracy.SetActive(false);
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
}
