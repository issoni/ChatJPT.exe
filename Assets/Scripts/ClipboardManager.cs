using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class ClipboardManager : MonoBehaviour
{
    public FunctionHighlighter highlighter;
    //maybe add input field here to make it interactable
    public TMP_InputField inputField;



    void Update()
    {
        if (Input.GetKey(KeyCode.LeftCommand) && Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Ctrl + C detected.");

            string textToCopy = highlighter.GetSelectedText();
            Debug.Log("Text to copy: " + (string.IsNullOrEmpty(textToCopy) ? "None" : textToCopy));

            if (!string.IsNullOrEmpty(textToCopy))
            {
                GUIUtility.systemCopyBuffer = textToCopy;

                inputField.text = "";
                inputField.readOnly = true;
                inputField.interactable = true;


                Debug.Log("Copied to clipboard: " + textToCopy); 
            }
        }
    }
}
