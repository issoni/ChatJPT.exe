using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TabManager : MonoBehaviour
{
    public List<GameObject> tabContents;
    public List<Button> tabButtons;

    private int currentTabIndex = 0; 


    private void Start()
    {
        UpdateTabs(); 
    }

    public void SwitchTab(int tabIndex)
    {
        currentTabIndex = tabIndex;
        UpdateTabs(); 
    }

    private void UpdateTabs()
    {
        for (int i = 0; i < tabContents.Count; i++)
        {
            tabContents[i].SetActive(i == currentTabIndex); 
        }
    }
    
}
