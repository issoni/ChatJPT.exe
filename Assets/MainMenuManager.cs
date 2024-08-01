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
    public Text bootText5;
    public Text bootText6;
    public Text bootText7;
    public Text bootText8;
    public Text bootText9;
    public Text bootText10;
    public Text bootText11;
    public Text bootText12;
    public Text bootText13;
    public Text bootText14;
    public Image smolGuy;
    public Image copyrightPanel; 
    public GameObject menuPanel;
    public Text playText;
    public Text settingsText;
    public Text creditsText;
    public Text quitText; 

    public Text[] menuOptions;
    private string[] originalTexts; 
    private int selectedOption = 0;
    private Coroutine cursorBlink; 

    private void Start()
    {
        smolGuy.gameObject.SetActive(false); 
        menuPanel.SetActive(false); //menu is hidden at the start
        bootText5.gameObject.SetActive(false);
        copyrightPanel.gameObject.SetActive(false);

        StartCoroutine(BootSequence());

        menuOptions = new Text[] { playText, settingsText, creditsText, quitText };
        originalTexts = new string[menuOptions.Length];
        for (int i = 0; i < menuOptions.Length; i++)
        {
            originalTexts[i] = menuOptions[i].text; 
        }
    }

    private IEnumerator BootSequence()
    {
        //shows first line of text
        bootText1.text = "<color=#4CFFD9>ZETA VGA BIOS v1.8</color>";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs

        //shows second line of text
        bootText2.text = "<color=#EBFF8D>512</color>K <color=#EBFF8D>VGA</color> MODE";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs

        //shows third line of text
        bootText3.text = "Copyright 1992-1995 ZETA SYSTEMS INC."; 

        //shows fourth line of text
        bootText6.text = "Copyright 1991-1995 PixelWave Tech.";
        yield return new WaitForSeconds(2f); //wait for 2 secs

        //cursor blinking effect
        StartCoroutine(BlinkCursor(bootText4));

        yield return new WaitForSeconds(4f); //wait for 4 secs

        //stop cursor blinking
        StopCoroutine(BlinkCursor(bootText4)); 

        bootText6.text = ""; 

        //hide the boot texts and show menu
        //bootText1.gameObject.SetActive(false);
        //bootText2.gameObject.SetActive(false);
        bootText3.gameObject.SetActive(false); 
        bootText4.gameObject.SetActive(false);
        //bootText5.gameObject.SetActive(false);
        //bootText6.gameObject.SetActive(false);

        smolGuy.gameObject.SetActive(true);
        bootText1.text = "   NanoWare BIOS v3.7QX, Cherry Computing Initiative";
        bootText2.text = "   Copyright (C) 1989-1995, NanoWare Corp.";

        bootText6.text = "Version GH4557";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs

        bootText7.text = "CYRIX MII CPU at 233MHz";
        yield return new WaitForSeconds(0.5f); //wait for 2 secs

        bootText5.gameObject.SetActive(true);
        bootText5.text = "Memory Test : 420K OK";

        StartCoroutine(IncrementInteger());
        yield return new WaitForSeconds(5f); //wait for 5 secs
        StopCoroutine(IncrementInteger()); //why isnt this stopping? FIGURE OUT


        bootText9.text = "NanoWare Plug and Play BIOS Extension v1.3";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs
        bootText10.text = "Copyright (C) 1995, NanoWare Corp.";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs


        bootText11.text = "   Detecting IDE Primary Master   ... ";
        yield return new WaitForSeconds(1f); //wait for 1 secs
        bootText11.text = "   Detecting IDE Primary Master   ... None";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs

        bootText12.text = "   Detecting IDE Primary Slave    ...";
        yield return new WaitForSeconds(1f); //wait for 1 secs
        bootText12.text = "   Detecting IDE Primary Slave    ... None";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs

        bootText13.text = "   Detecting IDE Secondary Master ...";
        yield return new WaitForSeconds(1f); //wait for 1 secs
        bootText13.text = "   Detecting IDE Secondary Master ... None";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs

        bootText14.text = "Press KEY to enter SETUP";

        yield return new WaitUntil(() => Input.anyKeyDown);

        HideBootTexts(); 

        copyrightPanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs
        bootText12.gameObject.SetActive(true);
        bootText12.text = "Starting ChatJPT.exe ...";
        yield return new WaitForSeconds(0.5f); //wait for 0.5 secs
        bootText13.gameObject.SetActive(true);
        bootText13.text = "Press ENTER to pick an OPTION";

        yield return new WaitForSeconds(2f); //wait for 2 secs

        ShowMenu(); 

    }

    private void HideBootTexts()
    {
        bootText1.gameObject.SetActive(false);
        bootText2.gameObject.SetActive(false);
        bootText3.gameObject.SetActive(false);
        bootText4.gameObject.SetActive(false);
        bootText5.gameObject.SetActive(false);
        bootText6.gameObject.SetActive(false);
        bootText7.gameObject.SetActive(false);
        bootText8.gameObject.SetActive(false);
        bootText9.gameObject.SetActive(false);
        bootText10.gameObject.SetActive(false);
        bootText11.gameObject.SetActive(false);
        bootText12.gameObject.SetActive(false);
        bootText13.gameObject.SetActive(false);
        bootText14.gameObject.SetActive(false);
        smolGuy.gameObject.SetActive(false);
    }

    private void ShowMenu()
    {
        menuPanel.SetActive(true);
        UpdateMenuSelection(); 
    }

    private void Update()
    {
        if (menuPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                selectedOption = (selectedOption - 1 + menuOptions.Length) % menuOptions.Length;
                UpdateMenuSelection(); 
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                selectedOption = (selectedOption + 1) % menuOptions.Length;
                UpdateMenuSelection(); 
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                ExecuteMenuOption(); 
            }
        }
    }

    private void UpdateMenuSelection()
    {
        if (cursorBlink != null)
        {
            StopCoroutine(cursorBlink); 
        }

     
        for(int i = 0; i < menuOptions.Length; i++)
        {
            menuOptions[i].text = originalTexts[i]; 

            if (i == selectedOption)
            {
                //var option = menuOptions[i].text; 
                menuOptions[i].color = Color.blue;

                //start cursor blinking for the selected option
                cursorBlink = StartCoroutine(BlinkCursor(menuOptions[i]));
            }
            else
            {
                menuOptions[i].color = Color.white;
                menuOptions[i].text = menuOptions[i].text.TrimEnd('_');
            }
        }
    }

    private void ExecuteMenuOption()
    {
        switch (selectedOption)
        {
            case 0:
                //Load the game scene
                SceneManager.LoadScene("SampleScene");
                break;
            case 1:
                //Show settings
                Debug.Log("Settings Selected");
                break;
            case 2:
                //Show credits
                Debug.Log("Credits Selected");
                break;
            case 3:
                //Quit the game
                Application.Quit();
                break; 
        }
    }

    private IEnumerator BlinkCursor(Text selectedOption)
    {
        string text;
        if (selectedOption.text != null)
        {
            text = selectedOption.text; 
        } else
        {
            text = ""; 
        }

        while (true)
        {
            selectedOption.text = text + "_";
            yield return new WaitForSeconds(0.5f); //blinking duration
            selectedOption.text = text + "";
            yield return new WaitForSeconds(0.5f); 
        }

    }

    private IEnumerator IncrementInteger()
    {
        int counter = 420;
        while (true)
        {
            bootText5.text = "Memory Test : " + counter.ToString() + "K OK";
            counter += Random.Range(2000, 10000); //increases by a random value
            yield return new WaitForSeconds(0.1f); //updates every 0.1 secs 
        }
    }
    
}
