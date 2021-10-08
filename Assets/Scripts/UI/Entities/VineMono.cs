using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.UI;

public class VineMono : MonoBehaviour
{
    public int id;
    private UITransitionEffect effect;
    private Transform relation;
    private Text relationText;
    
    public bool Active { get; set; }
    
    void Start()
    {
        effect = transform.GetComponent<UITransitionEffect>();
        relation = transform.Find("Image");
        relationText = transform.Find("Image/Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        Active = true;
        gameObject.SetActive(true);
        effect.Show();
    }

    public void Hide()
    {
        Active = false;
        effect.Hide();
    }

    public void SetText(int id)
    {
        //todo 把int映射到不同的关系上
        relationText.text = id.ToString();
    }
}