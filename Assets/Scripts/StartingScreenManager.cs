using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartingScreenManager : MonoBehaviour
{
    public Text welcomeText;
    public Button signInButton; 


    private void Start()
    {
        welcomeText.color = new Color(welcomeText.color.r, welcomeText.color.g, welcomeText.color.b, 0);
        signInButton.gameObject.SetActive(false);

        signInButton.onClick.AddListener(() => SceneManager.LoadScene("Desktop"));

        StartCoroutine(DisplayWelcomeSequence());

    }

    private IEnumerator DisplayWelcomeSequence()
    {
        yield return StartCoroutine(FadeInText(welcomeText, 2f));
        yield return new WaitForSeconds(1f);

        signInButton.gameObject.SetActive(true); 
    }

    private IEnumerator FadeInText(Text text, float duration)
    {
        float elapsedTime = 0f;
        Color startColor = text.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            yield return null; 
        }

        text.color = targetColor; 
    }
}
