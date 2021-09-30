using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuView : MonoBehaviour
{
    void Awake()
    {
        Input.multiTouchEnabled = false;
    }
    
    public void ButtonStart_OnClick()
    {
        PlayerPrefs.SetString("userName", "jonahwei");
        SceneManager.LoadScene("Game");
    }
    public void ButtonContinue_OnClick()
    {
        PlayerPrefs.SetString("userName", "jonahwei");
        SceneManager.LoadScene("Game");
    }
}
