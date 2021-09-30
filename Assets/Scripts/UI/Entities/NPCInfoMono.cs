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
    // Start is called before the first frame update
    void Start()
    {
        m_imageNPC = transform.Find("ImageNPCBackground/ImageNPC").GetComponent<Image>();
        m_textNPCName = transform.Find("TextNPCName").GetComponent<Text>();
        m_textNPCTitle = transform.Find("TextNPCTitle").GetComponent<Text>();
        m_textNPCVisibility = transform.Find("TextNPCVisibility").GetComponent<Text>(); 
        m_textLabelA = transform.Find("TextLabelA").GetComponent<Text>();  
        m_textLabelB = transform.Find("TextLabelB").GetComponent<Text>();  
        m_textLabelC = transform.Find("TextLabelC").GetComponent<Text>(); 
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
           
        //todo 读三个label
        m_textLabelA.text = npc.Name;
        m_textLabelB.text = npc.Name;
        m_textLabelC.text = npc.Name;
    }
    
}
