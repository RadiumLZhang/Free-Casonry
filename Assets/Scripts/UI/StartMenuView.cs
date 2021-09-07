using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuView : MonoBehaviour
{
    public void ButtonStart_OnClick()
    {
        SceneManager.LoadScene("Game");
    }
}
