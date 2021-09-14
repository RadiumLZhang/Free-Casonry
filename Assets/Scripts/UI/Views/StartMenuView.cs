using System.Collections;
using System.Collections.Generic;
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
        SceneManager.LoadScene("Game");
    }
}
