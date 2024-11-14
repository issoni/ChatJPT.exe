using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LoadingScreenManager : MonoBehaviour
{

    public Slider loadingBar; // make this an image instead 
    public float loadingTime = 3.5f;
    private float currentLoadTime = 0f; 


    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        loadingBar.value = 0f;

        float fillStep = 0.05f;
        float waitTime = loadingTime * fillStep;

        while (currentLoadTime < loadingTime)
        {
            loadingBar.value += fillStep;
            yield return new WaitForSeconds(waitTime); 
        }

        loadingBar.value = 4f;

        SceneManager.LoadScene("Starting");
    }

    

    /*
    private IEnumerator LoadScene()
    {
        loadingBar.value = 0f;

        while (currentLoadTime < loadingTime)
        {
            currentLoadTime += Time.deltaTime;
            loadingBar.value = currentLoadTime / loadingTime;
            yield return null; 
        }

        loadingBar.value = 4f;

        // For later: 
        //SceneManager.LoadScene("Starting"); 
    }
    */ 
}
