using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartMenuView : MonoBehaviour
{
    public string UserName;
    public Text TextUserName;
    public VideoPlayer OPPlayer;
    void Awake()
    {
        Input.multiTouchEnabled = false;
        TextUserName = transform.Find("InputField/Text").GetComponent<Text>();
        OPPlayer = transform.Find("OPPlayer").GetComponent<VideoPlayer>();
    }
    
    public void ButtonStart_OnClick()
    {
        UserName = TextUserName.text;
        OPPlayer.gameObject.SetActive(true);
        OPPlayer.Play();
        OPPlayer.loopPointReached += StartGame;
    }
    public void ButtonContinue_OnClick()
    {
        UserName = TextUserName.text;
        PlayerPrefs.SetString("userName", UserName);
        PlayerPrefs.SetString("saveName", UserName);
        SceneManager.LoadScene("Game");
    }

    public void StartGame(VideoPlayer videoPlayer)
    {
        PlayerPrefs.SetString("userName", UserName);
        PlayerPrefs.SetString("saveName", "");
        SceneManager.LoadScene("Game");
    }
}
