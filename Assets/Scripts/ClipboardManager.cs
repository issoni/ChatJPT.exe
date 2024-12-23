using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipboardManager : MonoBehaviour
{
    public FunctionHighlighter highlighter; 

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            string textToCopy = highlighter.GetSelectedText();
            if (!string.IsNullOrEmpty(textToCopy))
            {
                GUIUtility.systemCopyBuffer = textToCopy;
                Debug.Log("Copied to clipboard: " + textToCopy); 
            }
        }
    }
}
