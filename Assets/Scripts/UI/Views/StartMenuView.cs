using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartMenuView : MonoBehaviour
{
    public Text TextUserName;
    public VideoPlayer OPPlayer;
    private float TimerSkipOP;
    private GameObject ButtonSkipOP;
    private GameObject ButtonContinue;
    private GameObject ButtonContinueDisabled;
    private Text UserNameReminder;
    void Awake()
    {
        Input.multiTouchEnabled = true;
        TextUserName = transform.Find("InputField/Text").GetComponent<Text>();
        OPPlayer = transform.Find("OPPlayer").GetComponent<VideoPlayer>();
        ButtonSkipOP = transform.Find("ButtonSkipOP").gameObject;
        ButtonContinue = transform.Find("ButtonContinue").gameObject;
        ButtonContinueDisabled = transform.Find("ButtonContinueDisabled").gameObject;
        UserNameReminder = transform.Find("InputField/Placeholder").GetComponent<Text>();
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
        bool bIsDataExists = File.Exists("Assets/Resources/Save/" + TextUserName.text + ".txt");
        ButtonContinue.SetActive(bIsDataExists);
        ButtonContinueDisabled.SetActive(!bIsDataExists);
    }
    public void ButtonStart_OnClick()
    {
        if (TextUserName.text != "")
        {
            OPPlayer.gameObject.SetActive(true);
            OPPlayer.Play();
            OPPlayer.loopPointReached += OPFinished;
        }
        else
        {
            UserNameReminder.text = "输入一个用户名...";
            UserNameReminder.color = new Color(0.6f, 0f, 0f, 0.7f);
        }
    }
    public void ButtonContinue_OnClick()
    {
        if (TextUserName.text != "")
        {
            PlayerPrefs.SetString("userName", TextUserName.text);
            PlayerPrefs.SetString("saveName", TextUserName.text);
            SceneManager.LoadScene("Game");
        }
        else
        {
            UserNameReminder.text = "输入一个用户名...";
            UserNameReminder.color = new Color(0.6f, 0f, 0f, 0.7f);
        }
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
        PlayerPrefs.SetString("userName", TextUserName.text);
        PlayerPrefs.SetString("saveName", "");
        SceneManager.LoadScene("Game");
    }
}
