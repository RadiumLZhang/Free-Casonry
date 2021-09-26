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
    }

    public void SetOpenedNPC(GameObject NPC)
    {
        currentOpenedNPC = NPC;
    }
    // Update is called once per frame
    void Update()
    {
        if (bIsLerping)
        {
            scrollRect.horizontalNormalizedPosition =Mathf.Lerp(scrollRect.horizontalNormalizedPosition, lerpPosX, 20.0f * Time.deltaTime);
            scrollRect.verticalNormalizedPosition =Mathf.Lerp(scrollRect.verticalNormalizedPosition, lerpPosY, 20.0f * Time.deltaTime);
        }
        if (Mathf.Abs(scrollRect.horizontalNormalizedPosition - lerpPosX) < 0.02f &&
            Mathf.Abs(scrollRect.verticalNormalizedPosition - lerpPosY) < 0.02f)
        {
            scrollRect.horizontalNormalizedPosition = lerpPosX;
            scrollRect.verticalNormalizedPosition = lerpPosY;
            bIsLerping = false;
        }
    }
}
