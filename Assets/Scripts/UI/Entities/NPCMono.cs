using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMono : MonoBehaviour
{
    private GameObject eventCycle;

    private void Start()
    {
        eventCycle = transform.Find("EventCycle").gameObject;
    }

    public void NPC_OnClick()
    {
        eventCycle.SetActive(!eventCycle.activeSelf);
    }
}
