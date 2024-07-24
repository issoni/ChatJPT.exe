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
    public Text bootText4; 
    public GameObject menuPanel;

    // Start is called before the first frame update
    private void Start()
    {
        menuPanel.SetActive(false); //menu is hidden at the start
        StartCoroutine(BootSequence()); 
    }

    private IEnumerator BootSequence()
    {
        //shows first line of text
        bootText1.text = "Initializing...";
        yield return new WaitForSeconds(2f); //wait for 2 secs

        //shows second line of text
        bootText2.text = "Loading system files...";
        yield return new WaitForSeconds(2f); //wait for 2 secs

        //shows third line of text
        bootText3.text = "Press DEL to enter setup"; 
        yield return new WaitForSeconds(2f); //wait for 2 secs

        //cursor blinking effect
        StartCoroutine(BlinkCursor());

        yield return new WaitUntil(() => Input.anyKeyDown);

        //stop cursor blinking
        StopCoroutine(BlinkCursor()); 

        //hide the boot texts and show menu
        bootText1.gameObject.SetActive(false);
        bootText2.gameObject.SetActive(false);
        bootText3.gameObject.SetActive(false);
        menuPanel.SetActive(true); 
    }

    private IEnumerator BlinkCursor()
    {
        Text cursor = bootText4;
        while (true)
        {
            cursor.text = "_";
            yield return new WaitForSeconds(0.5f); //blinking duration
            cursor.text = "";
            yield return new WaitForSeconds(0.5f); 
        }

    }
    
}
