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
    public List<string> playerPrewrittenTexts;
    public List<string> npcPrewrittenTexts;

    // dialogue system
    public List<Dialogue> dialogues;
    private int dialogueIndex = 0; 

    private int playerTextIndex = 0;
    private int npcTextIndex = 0;
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

        //StartCoroutine(DelayNPCFirstMessage());
        StartCoroutine(PlayDialogues()); 
     }

    void Update() //update this to work with the new dialogues !!! 
    {
        if (isPlayerTurn && playerTextIndex < playerPrewrittenTexts.Count)
        {
            if (Input.anyKeyDown && !IsMouseInput() && inputField.text.Length < playerPrewrittenTexts[playerTextIndex].Length)
            {
                string currentText = playerPrewrittenTexts[playerTextIndex].Substring(0, inputField.text.Length + 1);
                inputField.text = currentText;
                inputField.caretPosition = inputField.text.Length;

                if (inputField.text == playerPrewrittenTexts[playerTextIndex])
                {
                    isTypingComplete = true;
                }
            }


            if (isTypingComplete && Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage(true, playerPrewrittenTexts[playerTextIndex]);
                playerTextIndex++;
                StartCoroutine(ScheduleNPCResponse());
                ResetPlayerInput(); 
            }
        }
    }

    void SendMessage(bool isPlayer, string messageText)
    {
        GameObject messagePrefab = isPlayer ? playerMessagePrefab : npcMessagePrefab;

        GameObject newMessage = Instantiate(messagePrefab, messagesContainer.transform);
        TMP_Text messageContent = newMessage.GetComponent<TMP_Text>();

        if (isPlayer)
        {
            messageContent.text = "<color=#FF0000><b>for:</b></color>" + messageText;
        } else
        {
            messageContent.text = "<color=#0077FF><b>jas:</b></color>" + messageText;
        }

        Canvas.ForceUpdateCanvases();
        var contentRect = messagesContainer.GetComponent<RectTransform>(); 
        contentRect.anchoredPosition = new Vector2(0, 0); 
    }

    IEnumerator ScheduleNPCResponse()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(2f);

        if (npcTextIndex < npcPrewrittenTexts.Count)
        {
            SendMessage(false, npcPrewrittenTexts[npcTextIndex]);
            npcTextIndex++; 
        }

        StartCoroutine(EnablePlayerTurn());

    }

    IEnumerator EnablePlayerTurn()
    {
        yield return new WaitForSeconds(1f);
        isPlayerTurn = true; 
    }

    void ResetPlayerInput()
    {
        inputField.text = "";
        inputField.caretPosition = 0;
        isTypingComplete = false; 
    }

    private bool IsMouseInput()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
    }

    /*

    IEnumerator DelayNPCFirstMessage()
    {
        yield return new WaitForSeconds(5f);

        if (npcPrewrittenTexts.Count > 0)
        {
            SendMessage(false, npcPrewrittenTexts[npcTextIndex]);
            npcTextIndex++;
            StartCoroutine(EnablePlayerTurn());
        }
    }
    */

    // dialogue functions

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

    IEnumerator PlayDialogues()
    {
        while (dialogueIndex < dialogues.Count)
        {
            Dialogue currentDialogue = dialogues[dialogueIndex];

            bool isPlayer = currentDialogue.speaker == "Jasper";
            SendMessage(isPlayer, currentDialogue.text);

            if (currentDialogue.triggersAction)
            {
                PerformAction(currentDialogue.action);
            }

            dialogueIndex++;
            yield return new WaitForSeconds(2f);
        }

        isPlayerTurn = true; 
    }
}


