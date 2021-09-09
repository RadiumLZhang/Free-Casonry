using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CouncilView : MonoBehaviour
{
    public GameObject panelManage;
    public GameObject panelPlan;
    public void ButtonPlan_OnClick()
    {
        panelPlan.SetActive(true);
        panelManage.SetActive(false);
    }
    public void ButtonManage_OnClick()
    {
        panelPlan.SetActive(false);
        panelManage.SetActive(true);
    }
}
