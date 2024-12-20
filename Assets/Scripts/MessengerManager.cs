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
    // i can add seconds as well, the time to wait between each text !! 
}

public class MessengerManager : MonoBehaviour
{
    // dialogue system
    public TMP_InputField inputField;
    public GameObject messagesContainer;
    public GameObject playerMessagePrefab;
    public GameObject npcMessagePrefab;
    public GameObject buttonLink; 

    public List<Dialogue> dialogues;
    private int dialogueIndex = 0; 

    private bool isPlayerTurn = false;
    private bool isTypingComplete = false;

    // other apps 
    public GameObject moozikPanel;
    private bool linkClicked = false; 



    void Start()
    {
        dialogues = new List<Dialogue>
        {
            new Dialogue { speaker = "Forrest", text = "yo"},
            new Dialogue { speaker = "Jasper", text = "yo, what's up bro"},
            new Dialogue { speaker = "Forrest", text = "nothing bro, just listening to music while trying to work on hw 3"},
            new Dialogue { speaker = "Jasper", text = "ooooo what music? bless my ears rn"},
            new Dialogue { speaker = "Forrest", text = "hehe this SONG NAME"},
            new Dialogue { speaker = "Jasper", text = "hmm... don't know how you are getting hw done with that shit on but you do you bro :D"}
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

        
        if (Input.GetMouseButtonDown(0))
        {
            TMP_Text npcText = messagesContainer.GetComponentInChildren<TMP_Text>();

                int linkIndex = TMP_TextUtilities.FindIntersectingLink(npcText, Input.mousePosition, Camera.main);
                if (linkIndex != -1)
                {
                    TMP_LinkInfo linkInfo = npcText.textInfo.linkInfo[linkIndex];
                    HandleLinkClick(linkInfo.GetLinkID());
                    Debug.Log("Link clicked"); 
                }
            
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
                linkClicked = false; 
            }
        }
    }

    void SendMessage(bool isPlayer, string messageText)
    {
        GameObject messagePrefab = isPlayer ? playerMessagePrefab : npcMessagePrefab;

        GameObject newMessage = Instantiate(messagePrefab, messagesContainer.transform);
        TMP_Text messageContent = newMessage.GetComponent<TMP_Text>();

        if (!isPlayer && messageText.Contains("SONG NAME")) // CHANGE SONG NAME !!! 
        {
            //GameObject newLink = Instantiate(buttonLink, messagesContainer.transform);
            // instantiate a button link and replace the song name with it 
            messageText = messageText.Replace(
                "SONG NAME",
                "<link=\"song\"><color=#0000FF><u>SONG NAME</u></color></link>"
                );
            // add on click functionalities: opens song panel, link clciked is false agian 
        }

        messageContent.text = isPlayer
            ? $"<color=#0077FF><b>jas:</b></color> {messageText}"
            : $"<color=#FF0000><b>for:</b></color> {messageText}";


        Canvas.ForceUpdateCanvases();

        var contentRect = messagesContainer.GetComponent<RectTransform>(); 
        contentRect.anchoredPosition = new Vector2(0, 0); 
    }

    IEnumerator ScheduleNPCResponse()
    {
        isPlayerTurn = false;
        //inputField.readOnly = true;
        

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

        // what is this for? 
        /*while (!linkClicked && moozikPanel.activeSelf)
        {
            yield return null; 
        }*/

        isPlayerTurn = true;

        //inputField.readOnly = true; // Allow simulated input only

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

    
    void HandleLinkClick(string linkID)
    {
        if (linkID == "song")
        {
            moozikPanel.SetActive(true);
            // wait for 3 seconds so the player can listen to the music? 
            linkClicked = true;
        }
    }
    

    /*
    public void ResumeConversationAfterMoozik()
    {
        // linkClicked = true;
        // once the play button 
    }
    */ 

    private bool IsMouseInput()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
    }
}


