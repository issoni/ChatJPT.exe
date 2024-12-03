using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DesktopManager : MonoBehaviour
{
    public RectTransform slidingPanel;
    public Button restoreButton;
    public GameObject webpagePanel; 

    private Animator animator; 

    private void Start()
    {
        animator = slidingPanel.GetComponent<Animator>();

        slidingPanel.gameObject.SetActive(false);
        restoreButton.gameObject.SetActive(false);

        StartCoroutine(ShowSlidingPanelWithDelay(2f)); 
    }

    private IEnumerator ShowSlidingPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        slidingPanel.gameObject.SetActive(true);
        restoreButton.gameObject.SetActive(true);

        animator.Play("SlideIn");


    }

    public void ShowWebpage()
    {
        webpagePanel.SetActive(true); 
    }
}
