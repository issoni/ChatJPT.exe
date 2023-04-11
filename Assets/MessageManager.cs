using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class MessageManager : MonoBehaviour
{
    public GameObject chatPanel, textObject;
    public TMP_InputField chatBox; 

    [SerializeField]
    List<Message> messageList = new List<Message>(); 

    void Start()
    {
        
    }

    void Update()
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(chatBox.text);
                chatBox.text = "";
            }
        }
        
    }

    public void SendMessageToChat(string text)
    {
        Message newMessage = new Message(); 

        newMessage.text = text;

        GameObject newText = Instantiate(textObject, chatPanel.transform);

        newMessage.textObject = newText.GetComponent<TextMeshPro>();

        newMessage.textObject.text = newMessage.text; 

        messageList.Add(newMessage);
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public TextMeshPro textObject; 
}
