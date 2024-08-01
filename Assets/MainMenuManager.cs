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
    public GameObject menuPanel;

    // Start is called before the first frame update
    private void Start()
    {
        smolGuy.gameObject.SetActive(false); 
        menuPanel.SetActive(false); //menu is hidden at the start
        StartCoroutine(BootSequence());
        bootText5.gameObject.SetActive(false);
    }

    private IEnumerator BootSequence()
    {
        //shows first line of text
        bootText1.text = "<color=#4CFFD9>ZETA VGA BIOS v1.8</color>";
        yield return new WaitForSeconds(0.5f); //wait for 2 secs

        //shows second line of text
        bootText2.text = "<color=#EBFF8D>512</color>K <color=#EBFF8D>VGA</color> MODE";
        yield return new WaitForSeconds(0.5f); //wait for 2 secs

        //shows third line of text
        bootText3.text = "Copyright 1992-1995 ZETA SYSTEMS INC."; 

        //shows fourth line of text
        bootText6.text = "Copyright 1991-1995 PixelWave Tech.";
        yield return new WaitForSeconds(2f); //wait for 2 secs


        //start integer increment effect
        //bootText5.gameObject.SetActive(true);
        //StartCoroutine(IncrementInteger());

        //cursor blinking effect
        StartCoroutine(BlinkCursor());

        yield return new WaitForSeconds(4f); //wait for 4 secs
        //yield return new WaitUntil(() => Input.anyKeyDown);

        //stop cursor blinking
        StopCoroutine(BlinkCursor());
        //StopCoroutine(IncrementInteger());

        bootText6.text = ""; 

        //hide the boot texts and show menu
        //bootText1.gameObject.SetActive(false);
        //bootText2.gameObject.SetActive(false);
        bootText3.gameObject.SetActive(false);
        //bootText4.gameObject.SetActive(false);
        //bootText5.gameObject.SetActive(false);
        //bootText6.gameObject.SetActive(false);

        smolGuy.gameObject.SetActive(true);
        bootText1.text = "   NanoWare BIOS v3.7QX, Cherry Computing Initiative";
        bootText2.text = "   Copyright (C) 1989-1995, NanoWare Corp.";

        bootText6.text = "Version GH4557";
        yield return new WaitForSeconds(0.5f); //wait for 2 secs

        bootText7.text = "CYRIX MII CPU at 233MHz";
        yield return new WaitForSeconds(0.5f); //wait for 2 secs

        bootText5.gameObject.SetActive(true);
        bootText5.text = "Memory Test : 420K OK";

        StartCoroutine(IncrementInteger());
        yield return new WaitForSeconds(5f); //wait for 4 secs
        StopCoroutine(IncrementInteger());

        bootText9.text = "NanoWare Plug and Play BIOS Extension v1.3";
        yield return new WaitForSeconds(0.5f); //wait for 2 secs
        bootText10.text = "Copyright (C) 1995, NanoWare Corp.";
        yield return new WaitForSeconds(0.5f); //wait for 2 secs


        bootText11.text = "Copyright (C) 1995, NanoWare Corp.";
        yield return new WaitForSeconds(1f); //wait for 2 secs
        bootText12.text = "Copyright (C) 1995, NanoWare Corp.";
        yield return new WaitForSeconds(1f); //wait for 2 secs
        bootText13.text = "Copyright (C) 1995, NanoWare Corp.";
        yield return new WaitForSeconds(1f); //wait for 2 secs






        //menuPanel.SetActive(true); 
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
