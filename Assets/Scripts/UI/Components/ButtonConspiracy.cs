using System;
using System.Collections;
using System.Collections.Generic;
using Logic.Conspiracy;
using Manager;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConspiracy : MonoBehaviour
{
    public long id;

    public void OnClick()
    {
        CouncilView councilView = UIManager.Instance.panelCouncil.GetComponent<CouncilView>();
        councilView.SwitchConspiracyButton();
        transform.GetComponent<Image>().sprite =
            (id == 80051) ? councilView.conspiracyFinalSpriteChosen : councilView.conspiracySpriteChosen;
        Conspiracy temp = ConspiracyManager.Instance.GetConspiracy(id);
        if (temp != null)
            UIManager.Instance.panelCouncil.GetComponent<CouncilView>().SwitchConspiracy(temp);
    }
}
