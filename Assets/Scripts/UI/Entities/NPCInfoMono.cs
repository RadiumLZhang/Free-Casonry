using System.Collections;
using System.Collections.Generic;
using Logic.Human;
using UnityEngine;
using UnityEngine.UI;

public class NPCInfoMono : MonoBehaviour
{
    private Image m_imageNPC;
    private Text m_textNPCName;
    private Text m_textNPCTitle;
    private Text m_textNPCVisibility;
    private Text m_textLabelA;
    private Text m_textLabelB;
    private Text m_textLabelC;
    private Animation m_animation;

    private const string PanelInAnimation = "NpcPanelIn";
    
    void Start()
    {
        m_imageNPC = transform.Find("animationRoot/ImageNPCBackground/ImageNPC").GetComponent<Image>();
        m_textNPCName = transform.Find("animationRoot/TextNPCName").GetComponent<Text>();
        m_textNPCTitle = transform.Find("animationRoot/TextNPCTitle").GetComponent<Text>();
        m_textNPCVisibility = transform.Find("animationRoot/TextNPCVisibility").GetComponent<Text>(); 
        m_textLabelA = transform.Find("animationRoot/TextLabelA").GetComponent<Text>();  
        m_textLabelB = transform.Find("animationRoot/TextLabelB").GetComponent<Text>();  

        m_animation = transform.Find("animationRoot").GetComponent<Animation>();
        m_animation.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchNpcInfo(Human npc)
    {
        m_imageNPC.sprite = Resources.Load<Sprite>(npc.Image);
        m_textNPCName.text = npc.Name;
        m_textNPCTitle.text = npc.Title;
        m_textNPCVisibility.text = npc.Visibility.ToString();
           
        //todo 读两个label
        m_textLabelA.text = npc.Name;
        m_textLabelB.text = npc.Name;
    }

    public void OpenNpcInfo()
    {
        var inAni = m_animation[PanelInAnimation];
        inAni.speed = 1;
        inAni.normalizedTime = 0;

        inAni.enabled = false;
        m_animation.Sample();
        m_animation.Play(PanelInAnimation);
    }

    public void CloseNpcInfo()
    {
        var inAni = m_animation[PanelInAnimation];
        inAni.speed = -1;
        inAni.normalizedTime = 1;

        inAni.enabled = false;
        m_animation.Sample();
        m_animation.Play(PanelInAnimation);
        
    }
}
