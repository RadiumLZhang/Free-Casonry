using System;
using System.Collections;
using System.Collections.Generic;
using Logic.Conspiracy;
using Manager;
using TotalConspiracy;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConspiracy : MonoBehaviour
{
    public long id;

    public void OnClick()
    {
        CouncilView councilView = transform.parent.parent.parent.parent.parent.GetComponent<CouncilView>();
        councilView.SwitchConspiracyButton();
        transform.GetComponent<Image>().sprite =
            (id == 71090) ? councilView.conspiracyFinalSpriteChosen : councilView.conspiracySpriteChosen;
        TotalConspiracy.TotalConspiracy.Types.TotalConspiracyItem item = TotalConspiracyLoader.Instance.FindTotalConspiracyItem(id);
        if (item != null)
            UIManager.Instance.panelCouncil.GetComponent<CouncilView>().SwitchConspiracy(item);
    }
}
