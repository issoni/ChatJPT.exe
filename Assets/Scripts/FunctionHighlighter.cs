using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class FunctionHighlighter : MonoBehaviour
{
    public TextMeshProUGUI functionText;
    private string selectedText;
    private bool isHighlighting;

    private int highlightStartIndex;
    private int highlightEndIndex;

    private const string mainFunctionText = "public void Main() {\n  Debug.Log(\"Hello\");\n}";

    void Start()
    {
        selectedText = string.Empty;
        isHighlighting = false;

        functionText.text = mainFunctionText; 
    }

    void Update()
    {
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            highlightStartIndex = TMP_TextUtilities.FindIntersectingCharacter(functionText, mousePosition, null, true);
            if(highlightStartIndex >= 0)
            {
                isHighlighting = true; 
            }
        }

        if(isHighlighting && Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            highlightEndIndex = TMP_TextUtilities.FindIntersectingCharacter(functionText, mousePosition, null, true);
            UpdateHighlightVisual();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHighlighting = false;
            LockHighlight(); 
        }
    }

    private void UpdateHighlightVisual()
    {
        if (highlightStartIndex >= 0 && highlightEndIndex >= 0 && highlightStartIndex <= highlightEndIndex)
        {
            string fullText = functionText.text;

            int validStartIndex = fullText.IndexOf(mainFunctionText);
            int validEndIndex = validStartIndex + mainFunctionText.Length - 1;

            if (highlightStartIndex >= validStartIndex && highlightEndIndex <= validEndIndex)
            {
                string beforeHighlight = fullText.Substring(0, highlightStartIndex);
                string highlighted = fullText.Substring(highlightStartIndex, highlightEndIndex - highlightStartIndex + 1);
                string afterHighlight = fullText.Substring(highlightEndIndex + 1);

                functionText.text = $"{beforeHighlight}<mark=#FFFF00>{highlighted}</mark>{afterHighlight}"; 
            } else
            {
                ResetHighlight(); 
            }
        }
    }

    private void LockHighlight() {
        if (highlightStartIndex >= 0 && highlightEndIndex >= 0)
        {
            string fullText = functionText.text;

            int validStartIndex = fullText.IndexOf(mainFunctionText);
            int validEndIndex = validStartIndex + mainFunctionText.Length - 1;

            if (highlightStartIndex >= validStartIndex && highlightEndIndex <= validEndIndex)
            {
                selectedText = mainFunctionText;
                Debug.Log("Highlighted text: " + selectedText);
                return;
            }
        }

        ResetHighlight();
    }

    private void ResetHighlight()
    {
        selectedText = string.Empty;
        functionText.text = mainFunctionText;
        Debug.Log("Invalid highlight reset");
    }

    public string GetSelectedText()
    {
        return selectedText;
    }

    
    public void HighlightMainFunction()
    {
        selectedText = mainFunctionText;
        functionText.text = "<mark=#FFFF00>" + mainFunctionText + "</mark>"; 
    }

    
}
