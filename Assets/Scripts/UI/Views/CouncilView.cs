using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CouncilView : MonoBehaviour
{
    private GameObject panelConspiracy;
    private GameObject panelManage;
    
    //ImageLights
    private GameObject imageConspiracyOff;
    private GameObject imageConspiracyOn;
    private GameObject imageManageOff;
    private GameObject imageManageOn;
    

    void Start()
    {
        panelConspiracy = transform.Find("PanelConspiracy").gameObject;
        panelManage = transform.Find("PanelManage").gameObject;
        imageConspiracyOff = transform.Find("ImageConspiracyOff").gameObject;
        imageConspiracyOn = transform.Find("ImageConspiracyOn").gameObject;
        imageManageOff = transform.Find("ImageManageOff").gameObject;
        imageManageOn = transform.Find("ImageManageOn").gameObject;
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
        SwitchLightToConspiracy(true);
        panelConspiracy.SetActive(true);
        panelManage.SetActive(false);
    }
    public void ButtonManage_OnClick()
    {
        SwitchLightToConspiracy(false);
        panelConspiracy.SetActive(false);
        panelManage.SetActive(true);
    }
}
