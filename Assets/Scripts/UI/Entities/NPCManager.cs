using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    private ScrollRect scrollRect;
    private float lerpPosX;
    private float lerpPosY;
    public bool bIsLerping;
    private bool bIsXStopped;
    private bool bIsYStopped;
    public GameObject currentOpenedNPC;
    void Start()
    {
        lerpPosX = 0.5f;
        lerpPosY = 0.5f;
        scrollRect = GameObject.Find("ScrollRelationship").GetComponent<ScrollRect>();
    }

    public void StartNPCLerp(float x, float y)
    {
        bIsLerping = true;
        lerpPosX = x;
        lerpPosY = y;
        bIsXStopped = false;
        bIsYStopped = false;
    }

    public void SetOpenedNPC(GameObject NPC)
    {
        currentOpenedNPC = NPC;
    }
    // Update is called once per frame
    void Update()
    {
        if (bIsLerping && !bIsXStopped)
        {
            scrollRect.horizontalNormalizedPosition =Mathf.Lerp(scrollRect.horizontalNormalizedPosition, lerpPosX, 10.0f * Time.deltaTime);
        }

        if (bIsLerping && !bIsYStopped)
        {
            scrollRect.verticalNormalizedPosition =Mathf.Lerp(scrollRect.verticalNormalizedPosition, lerpPosY, 10.0f * Time.deltaTime);
        }

        //边界情况处理
        if (lerpPosX <= 0 && scrollRect.horizontalNormalizedPosition < 0.005f)
        {
            scrollRect.horizontalNormalizedPosition = 0;
            bIsXStopped = true;
        }

        if (lerpPosX >= 1 && scrollRect.horizontalNormalizedPosition > 0.995f)
        {
            scrollRect.horizontalNormalizedPosition = 1;
            bIsXStopped = true;
        }
        
        if (lerpPosY <= 0 && scrollRect.verticalNormalizedPosition < 0.005f)
        {
            scrollRect.verticalNormalizedPosition = 0;
            bIsYStopped = true;
        }
        
        if (lerpPosY >= 1 && scrollRect.verticalNormalizedPosition > 0.995f)
        {
            scrollRect.verticalNormalizedPosition = 1;
            bIsYStopped = true;
        }
        
        //做Clamp
        if (Mathf.Abs(scrollRect.horizontalNormalizedPosition - lerpPosX) < 0.005f)
        {
            scrollRect.horizontalNormalizedPosition = lerpPosX;
            bIsXStopped = true;
        }
        
        if (Mathf.Abs(scrollRect.verticalNormalizedPosition - lerpPosY) < 0.005f)
        {
            scrollRect.verticalNormalizedPosition = lerpPosY;
            bIsYStopped = true;
        }
        
        if (bIsXStopped && bIsYStopped)
        {
            bIsLerping = false;
        }
    }
}
