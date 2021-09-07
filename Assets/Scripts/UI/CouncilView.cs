using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CouncilView : MonoBehaviour
{
    public GameObject PanelManage;
    public GameObject PanelPlan;
    public void ButtonPlan_OnClick()
    {
        PanelPlan.SetActive(true);
        PanelManage.SetActive(false);
    }
    public void ButtonManage_OnClick()
    {
        PanelPlan.SetActive(false);
        PanelManage.SetActive(true);
    }
}
