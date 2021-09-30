using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuView : MonoBehaviour
{
    public string UserName;
    public Text TextUserName;
    void Awake()
    {
        Input.multiTouchEnabled = false;
        TextUserName = transform.Find("InputField/Text").GetComponent<Text>();
    }

    void Update()
    {
        print(UserName);
    }
    public void ButtonStart_OnClick()
    {
        PlayerPrefs.SetString("userName", "");
        SceneManager.LoadScene("Game");
    }
    public void ButtonContinue_OnClick()
    {
        PlayerPrefs.SetString("userName", UserName);
        SceneManager.LoadScene("Game");
    }
    public void ButtonLogin_OnClick()
    {
        UserName = TextUserName.text;
    }
}
