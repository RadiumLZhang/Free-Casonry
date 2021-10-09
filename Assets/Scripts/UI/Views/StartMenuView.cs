using System.Collections;
using System.Collections.Generic;
using System.Timers;
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
    private float TimerSkipOP;
    private GameObject ButtonSkipOP;
    void Awake()
    {
        Input.multiTouchEnabled = true;
        TextUserName = transform.Find("InputField/Text").GetComponent<Text>();
        OPPlayer = transform.Find("OPPlayer").GetComponent<VideoPlayer>();
        ButtonSkipOP = transform.Find("ButtonSkipOP").gameObject;
    }

    void Update()
    {
        if (TimerSkipOP > 0)
        {
            TimerSkipOP -= Time.deltaTime;
            ButtonSkipOP.SetActive(true);
        }
        else
        {
            TimerSkipOP = 0;
            ButtonSkipOP.SetActive(false);
        }
    }
    public void ButtonStart_OnClick()
    {
        UserName = TextUserName.text;
        OPPlayer.gameObject.SetActive(true);
        OPPlayer.Play();
        OPPlayer.loopPointReached += OPFinished;
    }
    public void ButtonContinue_OnClick()
    {
        UserName = TextUserName.text;
        PlayerPrefs.SetString("userName", UserName);
        PlayerPrefs.SetString("saveName", UserName);
        SceneManager.LoadScene("Game");
    }
    
    public void ButtonOP_OnClick()
    {
        TimerSkipOP = 3.5f;
    }
    public void ButtonSkipOP_OnClick()
    {
        OPPlayer.Stop();
        StartGame();
    }
    public void OPFinished(VideoPlayer videoPlayer)
    {
        StartGame();
    }
    private void StartGame()
    {
        OPPlayer.gameObject.SetActive(false);
        PlayerPrefs.SetString("userName", UserName);
        PlayerPrefs.SetString("saveName", "");
        SceneManager.LoadScene("Game");
    }
}
