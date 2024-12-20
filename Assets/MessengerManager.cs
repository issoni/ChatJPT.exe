using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// dialogue system
[System.Serializable]
public class Dialogue
{
    public string speaker;
    public string text;
    public bool triggersAction;
    public string action;
}

public class MessengerManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject messagesContainer;
    public GameObject playerMessagePrefab;
    public GameObject npcMessagePrefab;

    // dialogue system
    public List<Dialogue> dialogues;
    private int dialogueIndex = 0; 

    private bool isPlayerTurn = false;
    private bool isTypingComplete = false;


    void Start()
    {
        dialogues = new List<Dialogue>
        {
            new Dialogue { speaker = "Forrest", text = "yo"},
            new Dialogue { speaker = "Jasper", text = "yo, what's up bro"},
            new Dialogue { speaker = "Forrest", text = "nothing bro, just listening to music while trying to work on hw 3"},
            new Dialogue { speaker = "Jasper", text = "ooooo what music? bless my ears rn"},
            new Dialogue { speaker = "Forrest", text = "hehe this SONG NAME", triggersAction = true, action = "openMoozik"}
            // add more dialogue after testing 
        };

        inputField.text = "";
        inputField.readOnly = true;

        StartCoroutine(PlayDialogues()); 
     }

    void Update()  
    {
        if (isPlayerTurn)
        {
            HandlePlayerInput();
        }
    }

    void HandlePlayerInput()
    {
        if (dialogueIndex < dialogues.Count && dialogues[dialogueIndex].speaker == "Jasper")
        {
            if (Input.anyKeyDown && !IsMouseInput() && inputField.text.Length < dialogues[dialogueIndex].text.Length)
            {
                string currentText = dialogues[dialogueIndex].text.Substring(0, inputField.text.Length + 1);
                inputField.text = currentText;
                inputField.caretPosition = inputField.text.Length;

                if (inputField.text == dialogues[dialogueIndex].text)
                {
                    isTypingComplete = true;
                }
            }


            if (isTypingComplete && Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage(true, dialogues[dialogueIndex].text);
                dialogueIndex++;

                StartCoroutine(ScheduleNPCResponse());
                ResetInputField(); 
            }
        }
    }

    void SendMessage(bool isPlayer, string messageText)
    {
        GameObject messagePrefab = isPlayer ? playerMessagePrefab : npcMessagePrefab;

        GameObject newMessage = Instantiate(messagePrefab, messagesContainer.transform);
        TMP_Text messageContent = newMessage.GetComponent<TMP_Text>();

        messageContent.text = isPlayer ? $"<color=#0077FF><b>jas:</b></color> " +
            $"{messageText}" : $"<color=#FF0000><b>for:</b></color> {messageText}";


        Canvas.ForceUpdateCanvases();
        var contentRect = messagesContainer.GetComponent<RectTransform>(); 
        contentRect.anchoredPosition = new Vector2(0, 0); 
    }

    IEnumerator ScheduleNPCResponse()
    {
        isPlayerTurn = false;
        inputField.readOnly = true;
        yield return new WaitForSeconds(2f);

        if (dialogueIndex < dialogues.Count && dialogues[dialogueIndex].speaker == "Forrest")
        {
            Dialogue currentDialogue = dialogues[dialogueIndex];
            SendMessage(false, currentDialogue.text);

            if(currentDialogue.triggersAction)
            {
                PerformAction(currentDialogue.action); 
            }

            dialogueIndex++; 
        }

        yield return new WaitForSeconds(1f);
        isPlayerTurn = true;

        inputField.readOnly = true; // Allow simulated input only

    }

    void ResetInputField()
    {
        inputField.text = "";
        inputField.caretPosition = 0;
        isTypingComplete = false;
    }


    IEnumerator PlayDialogues()
    {
        yield return new WaitForSeconds(2f);

        while (dialogueIndex < dialogues.Count && dialogues[dialogueIndex].speaker == "Forrest")
        {
            Dialogue currentDialogue = dialogues[dialogueIndex];
            SendMessage(false, currentDialogue.text);

            if (currentDialogue.triggersAction)
            {
                PerformAction(currentDialogue.action);
            }

            dialogueIndex++;
            yield return new WaitForSeconds(2f);
        }

        isPlayerTurn = true;
    }


    void PerformAction(string action)
    {
        if (action == "openMoozik")
        {
            Debug.Log("Opening MOOZIK"); 
        } else if (action == "copyCode")
        {
            Debug.Log("Copying code"); 
        }
    }


    private bool IsMouseInput()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
    }
}


