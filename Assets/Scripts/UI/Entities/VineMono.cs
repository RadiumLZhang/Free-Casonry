using System.Collections;
using System.Collections.Generic;
using Coffee.UIEffects;
using Language;
using UnityEngine;
using UnityEngine.UI;

public class VineMono : MonoBehaviour
{
    public int id;
    private UITransitionEffect effectVine;
    private UITransitionEffect effectBg;
    private UITransitionEffect effectText;
    private Transform relation;
    private Text relationText;

    public int relationId;
    
    public bool Active { get; set; }
    
    void Start()
    {
        relation = transform.Find("Image");
        relationText = transform.Find("Image/Text").GetComponent<Text>();
        
        effectVine = transform.GetComponent<UITransitionEffect>();
        effectBg = relation.GetComponent<UITransitionEffect>();
        effectText = relationText.GetComponent<UITransitionEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        Active = true;
        gameObject.SetActive(true);
        effectVine.Show();
        effectBg.Show();
        effectText.Show();
    }

    public void Hide()
    {
        Active = false;
        effectVine.Hide();
    }

    public void SetText(int id)
    {
        relationId = id;

        var item = LanguageLoader.Instance.FindLanguageItem(id.ToString());
        relationText.text = item.Value;

        effectText.Show();
        effectBg.Show();
    }
}
