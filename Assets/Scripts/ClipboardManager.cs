using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardManager : MonoBehaviour
{
    public FunctionHighlighter highlighter; 

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
                Debug.Log("Copied to clipboard: " + textToCopy); 
            }
        }
    }
}
