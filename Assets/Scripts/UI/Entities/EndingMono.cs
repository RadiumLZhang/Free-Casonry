﻿using System;
using Language;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingMono : MonoBehaviour
{
    private Animation m_animation;
    private Text m_txtResult;

    private void Start()
    {
        m_animation = GetComponent<Animation>();
        m_txtResult = transform.Find("TxtEnding").GetComponent<Text>();
    }

    public void EnterEnding(int id)
    {
        var item = LanguageLoader.Instance.FindLanguageItem(id.ToString());
        m_txtResult.text = item.ToString();
        
        var state = m_animation["EndingPanelIn"];

        state.speed = 1;
        state.normalizedTime = 0;
        
        state.enabled = false;
        m_animation.Sample();
        m_animation.Play(state.name);
    }

    public void Ending_OnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
}