using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class HW3FIleIcon : MonoBehaviour
{

    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f;
    public GameObject filePanel;
  
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        if (timeSinceLastClick <= doubleClickThreshold)
        {
            OpenFile();
        }

        lastClickTime = Time.time;
    }

    void OpenFile()
    {
        
        filePanel.SetActive(true);
        
    }
}
