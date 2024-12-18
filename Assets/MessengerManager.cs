using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MessengerManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject messagesContainer;
    public GameObject playerMessagePrefab;
    public GameObject npcMessagePrefab;
    public List<string> playerPrewrittenTexts;
    public List<string> npcPrewrittenTexts;

    private int playerTextIndex = 0;
    private int npcTextIndex = 0;
    private bool isPlayerTurn = true;
    private bool isTypingComplete = false; 


    void Start()
    {
        inputField.text = "";
        inputField.readOnly = true; 
    }

    void Update()
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
}
