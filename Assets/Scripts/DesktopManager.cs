using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DesktopManager : MonoBehaviour
{
    public RectTransform slidingPanel;
    public Button restoreButton;

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

        animator.Play("SlideIn");

        yield return new WaitForSeconds(1f);

        restoreButton.gameObject.SetActive(true); 
    }
    
}
