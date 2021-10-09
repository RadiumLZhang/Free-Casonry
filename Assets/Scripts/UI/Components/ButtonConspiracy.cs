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
    public AudioSource adplayer;

    void Start()
    {
        adplayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
    }

    public void OnClick()
    {
        AudioClip m_clip = Resources.Load<AudioClip>("AudioClips/猫咪议会/" + "目标阅览");
        adplayer.clip = m_clip;
        adplayer.Play();
        CouncilView councilView = UIManager.Instance.panelCouncil.GetComponent<CouncilView>();

        councilView.SwitchConspiracyButton();
        transform.GetComponent<Image>().sprite =
            (id == 71090) ? councilView.conspiracyFinalSpriteChosen : councilView.conspiracySpriteChosen;
        TotalConspiracy.TotalConspiracy.Types.TotalConspiracyItem item = TotalConspiracyLoader.Instance.FindTotalConspiracyItem(id);
        if (item != null)
            UIManager.Instance.panelCouncil.GetComponent<CouncilView>().SwitchConspiracy(item);
    }
}
