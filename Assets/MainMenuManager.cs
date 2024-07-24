using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MainMenuManager : MonoBehaviour
{
    public Text bootText1;
    public Text bootText2;
    public Text bootText3;
    public GameObject menuPanel;

    // Start is called before the first frame update
    private void Start()
    {
        bootText1.gameObject.SetActive(false);
        bootText2.gameObject.SetActive(false);
        bootText3.gameObject.SetActive(false);
        menuPanel.SetActive(false); //menu is hidden at the start
        StartCoroutine(BootSequence()); 
    }

    private IEnumerator BootSequence()
    {
        //shows first line of text
        bootText1.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); //wait for 2 secs

        //shows second line of text
        bootText2.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); //wait for 2 secs

        //shows third line of text
        bootText3.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); //wait for 2 secs

        //cursor blinking effect
        StartCoroutine(BlinkCursor());

        //hide the boot texts and show menu
        bootText1.gameObject.SetActive(false);
        bootText2.gameObject.SetActive(false);
        bootText3.gameObject.SetActive(false);
        menuPanel.SetActive(true); 
    }

    private IEnumerator BlinkCursor()
    {
        Text cursor = bootText3;
        while (true)
        {
            cursor.text = "_";
            yield return new WaitForSeconds(0.5f); //blinking duration
            cursor.text = ".";
            yield return new WaitForSeconds(0.5f); 
        }

    }
    
}
