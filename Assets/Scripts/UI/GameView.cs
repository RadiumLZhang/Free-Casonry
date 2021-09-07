using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameView : MonoBehaviour
{
    public GameObject PanelCouncil;
    public GameObject PanelSettings;
    public void ButtonSettings_OnClick()
    {
        PanelSettings.SetActive(true);
    }
    public void ButtonRelationship_OnClick()
    {
        
    }
    public void ButtonCouncil_OnClick()
    {
        PanelCouncil.SetActive(true);
    }

    //Button in Council
    public void ButtonBacktoGame_OnClick()
    {
        PanelCouncil.SetActive(false);
    }

    //Buttons in Settings Panel
    public void ButtonSaveQuit_OnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
    public void ButtonResume_OnClick()
    {
        PanelSettings.SetActive(false);
    }
}
