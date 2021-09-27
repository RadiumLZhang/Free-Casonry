using System.Collections;
using System.Collections.Generic;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager: BaseModel<UIManager>
{
    //Images
    public GameObject darkBackgroundImage;
    
    //Panels
    public GameObject panelCouncil;
    public GameObject panelSettings;
    public GameObject panelEventExe;
    public GameObject panelResources;
    
    //Scrolls
    public GameObject scrollSpecialEvent;

    //Buttons
    public GameObject buttonOpenExePanel;
    public GameObject buttonCloseExePanel;
    public GameObject buttonCouncil;
    public GameObject buttonCouncilCatManage;
    
    //NPCs
    public Dictionary<long, GameObject> NPCEventCycles;
}
