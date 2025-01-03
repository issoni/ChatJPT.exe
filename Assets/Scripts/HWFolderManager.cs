using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HWFolderManager : MonoBehaviour
{
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    public GameObject folderPanel; 

    void Update()
    {
        
    }

    public void OnClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            OpenFolder();
        }

        lastClickTime = Time.time;
    }

    void OpenFolder()
    {
        if (folderPanel != null)
        {
            folderPanel.SetActive(true); 
        }
    }
}
