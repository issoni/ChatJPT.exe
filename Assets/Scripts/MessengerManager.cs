using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// dialogue system
[System.Serializable]
public class Dialogue
{
    public string speaker;
    public string text;
    public bool triggersAction;
    public string action;
    public float delay; // waiting before sending message 

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
    //private bool isImageClicked = false; 

    // other apps 
    public GameObject moozikPanel;



    void Start()
    {
        dialogues = new List<Dialogue>
        {
            new Dialogue { speaker = "Forrest", text = "yo", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "yo, what's up bro"},
            new Dialogue { speaker = "Forrest", text = "nothing bro, just listening to music while trying to work on hw 3", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "ooooo what music? bless my ears rn"},
            new Dialogue { speaker = "Forrest", text = "hehe this SONG NAME", triggersAction = true, action = "SongLinkImage", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "hmm... don't know how you are getting hw done with that shit on but you do you bro :D"},
            new Dialogue { speaker = "Forrest", text = "uh yeah… actually jas", delay = 1.0f},
            new Dialogue { speaker = "Forrest", text = "i haven't even started T_T", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "you know it’s due tmr right…"},
            new Dialogue { speaker = "Forrest", text = "IKKKK", delay = 1.0f},
            new Dialogue { speaker = "Forrest", text = "i left that shit for last minute like i always do. not only do i have to do the work but prepare for a mental breakdown", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "rip, good luck. it took me a full week to finish lol"},
            new Dialogue { speaker = "Forrest", text = "shit. how much you got left?", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "just gotta test one more function and then im donee. lmk if you're gonna need help"},
            new Dialogue { speaker = "Forrest", text = "perhaps i'll take you up on that offer", delay = 1.0f},
            new Dialogue { speaker = "Forrest", text = "wanna give me a headstart and let me see what you did for the main function?", delay = 1.0f},
            new Dialogue { speaker = "Forrest", text = "pretty please", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "are you asking me to take part in plagiarism right now :O"},
            new Dialogue { speaker = "Forrest", text = "you’re acting like this is your first time :|", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "lmao im kidding bro. hold on, lemme pull it up - u want me to send it by mail or what?"},
            new Dialogue { speaker = "Forrest", text = "nah just copy paste that shit here ;)", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "lol ok. one sec", triggersAction = true, action = "CopyCode"},
            // copy paster code action 
            new Dialogue { speaker = "Forrest", text = "beautiful", delay = 1.0f},
            new Dialogue { speaker = "Forrest", text = "amazing", delay = 1.0f},
            new Dialogue { speaker = "Forrest", text = "divine", delay = 1.0f},
            new Dialogue { speaker = "Forrest", text = "thanks bro", delay = 1.0f},
            new Dialogue { speaker = "Jasper", text = "yeah yeah np :p lmk if you need more of my “help”"},
            /*
            new Dialogue { speaker = "Forrest", text = "fo sho, thanks brother"},
            new Dialogue { speaker = "Jasper", text = "i gotchu, you must be busy from your internship - congrats on that btw :)"},
            new Dialogue { speaker = "Forrest", text = "hehe thank you thank you"},
            new Dialogue { speaker = "Forrest", text = "and yeah, it’s been feeling like a lot lately but i made a new friend at work so that’s cool"},
            new Dialogue { speaker = "Jasper", text = "lessgoooo. so who is this new friend?"},
            new Dialogue { speaker = "Forrest", text = "ethan"},
            new Dialogue { speaker = "Forrest", text = "bro is pretty introverted and can be awkward at times but we seem to get along fine"},
            new Dialogue { speaker = "Jasper", text = "niceee, hey atleast you have someone to keep you company at work right"},
            new Dialogue { speaker = "Forrest", text = "fo shooo"},
            new Dialogue { speaker = "Forrest", text = "omg btw, Ethan told me about this new app he had been working on"},
            new Dialogue { speaker = "Forrest", text = "it's some crazy futuristic shit"},
            new Dialogue { speaker = "Jasper", text = ":o damn, tell me more"},
            new Dialogue { speaker = "Forrest", text = "ye, i think he is done with the development and just needs to do testing"},
            new Dialogue { speaker = "Forrest", text = "it’s called ChatJPT"},
            new Dialogue { speaker = "Jasper", text = "it does sound like some crazy futuristic shit lol what is it about"},
            new Dialogue { speaker = "Forrest", text = "it’s like this machine learning model that he trained using hella data and techniques"},
            new Dialogue { speaker = "Forrest", text = "that’s what he told me at least"},
            new Dialogue { speaker = "Jasper", text = "damn, that actually sounds kinda cool. so what does it do?"},
            new Dialogue { speaker = "Forrest", text = "it can do anything bro. write an essay, code your hw, make a resume for you, im telling you literally anything"},
            new Dialogue { speaker = "Forrest", text = "it can also probably give you tips on how to flirt with Shura too lmao"},
            new Dialogue { speaker = "Jasper", text = "stfu :|"},
            new Dialogue { speaker = "Forrest", text = "nah but seriously tho it seems pretty cool"},
            new Dialogue { speaker = "Forrest", text = "he actually sent it to me to test it this past week"},
            new Dialogue { speaker = "Forrest", text = "i just haven't gotten around to it cuz of school, work and maya"},
            new Dialogue { speaker = "Jasper", text = "hmm i see. you know, i'm pretty free today and you got me interested in this thing..." },
            new Dialogue { speaker = "Forrest", text = "ofc i got you interested in this, you nerd"},
            new Dialogue { speaker = "Jasper", text = "if Ethan’s cool with it, do you think i can test it out too?"},
            new Dialogue { speaker = "Forrest", text = "sure brother, i was gonna ask you anyways hehe"},
            new Dialogue { speaker = "Forrest", text = "lemme send it to you, one sec"},
            // SENDS CHATJPT
            new Dialogue { speaker = "Forrest", text = "lemme know what you think!"},
            // Jasper clicks and downloads it 
            new Dialogue { speaker = "Jasper", text = "nice :) i'll let you know for sure"},
            new Dialogue { speaker = "Forrest", text = "cool beanzzz"},
            new Dialogue { speaker = "Jasper", text = "btw how is it going with maya lately?"},
            new Dialogue { speaker = "Forrest", text = "it's good, bro :) im just"},
            new Dialogue { speaker = "Forrest", text = "really very happy :))"},
            new Dialogue { speaker = "Jasper", text = "so happy for you bromie!! i remember when you used to tell me about the little things you and her used to talk about, and now ya’ll dating :)"},
            new Dialogue { speaker = "Forrest", text = "yeah bro, i feel so lucky"},
            new Dialogue { speaker = "Forrest", text = "how are things with Shura?"},
            new Dialogue { speaker = "Forrest", text = "in which decade will you ask her out?? :p"},
            new Dialogue { speaker = "Jasper", text = "ummmmmmm… i was actually going to do it today :D"},
            new Dialogue { speaker = "Forrest", text = "WAIT FR?? lets goooooo"},
            new Dialogue { speaker = "Jasper", text = "i'm a little nervous but i think i am finally ready to do it. she has been living in my head rent free"},
            new Dialogue { speaker = "Forrest", text = "lmao yeah bro just do it, you're gonna feel so relieved and happy im telling u"},
            // SHURA sends a message to us - add it later 
            new Dialogue { speaker = "Forrest", text = "and watch her reciprocate those feelings back to you"},
            new Dialogue { speaker = "Jasper", text = "ok ok imma do it. in fact, she just messaged me..."},
            new Dialogue { speaker = "Forrest", text = "IT'S A SIGN, DO IT RNNNN"},
            new Dialogue { speaker = "Jasper", text = "shit, really?"},
            new Dialogue { speaker = "Forrest", text = "YESSSSS BRAH"},
            new Dialogue { speaker = "Jasper", text = "sdhfudhfsduhnfsdk okay fine ill do it"},
            new Dialogue { speaker = "Forrest", text = "go get her tiger"},
            new Dialogue { speaker = "Forrest", text = "i have to go work on this damn hw anyways"},
            new Dialogue { speaker = "Forrest", text = "lmk what happens ;)"},
            new Dialogue { speaker = "Jasper", text = "damn you're gonna leave me alone now huh, see ya bro T_T"},
            // Forrest likes the message
            // GLITCH

            // SHURA ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

            new Dialogue { speaker = "Shura", text = "hey Jas! :-)"},
            new Dialogue { speaker = "Jasper", text = "hi Shura!!"},
            new Dialogue { speaker = "Shura", text = "whatchu up toooo"},
            new Dialogue { speaker = "Jasper", text = "i was just chatting with Forrest. we were actually talking about you just now haha"},
            new Dialogue { speaker = "Shura", text = "talking shit about me huh :(((("},
            // GLITCH
            new Dialogue { speaker = "Jasper", text = "nooo, just was thinking about texting you so it just came up hehe"},
            new Dialogue { speaker = "Shura", text = "oooo, Jasper Frye was thinking about texting me??"},
            new Dialogue { speaker = "Shura", text = "i'm so lucky omg"},
            new Dialogue { speaker = "Jasper", text = "weelllllll, you are the chosen one"},
            new Dialogue { speaker = "Shura", text = "hehe"},
            new Dialogue { speaker = "Shura", text = "what’s up tho? what did you wanna talk about?"},
            // GLITCH
            new Dialogue { speaker = "Jasper", text = "um... i wanted to ask you something"},
            new Dialogue { speaker = "Shura", text = "go for ittt"},
            new Dialogue { speaker = "Jasper", text = "no no wait actually i wanted to tell you something"},
            new Dialogue { speaker = "Shura", text = "go for ittt ^.^"},
            // WORSE GLITCH 
            new Dialogue { speaker = "Jasper", text = "i wanted to tell you that i’ve had feelings for you for a while now"},
            // GLITCH AND WAIT - viewport shows: "i wanted to tell you that i think youre really ugly and just absolutely hideous"
            new Dialogue { speaker = "Shura", text = "what?"},
            new Dialogue { speaker = "Shura", text = "wth jas?"},
            new Dialogue { speaker = "Jasper", text = "wait shura i'm sorry idk wat happened, i said i have feelings for you"},
            // GLITCH AND WAIT - viewport shows: "wait Shura i'm sorry idk wat happened, i meant i think you're really ugly AND you smell like a dog’s fart"
            new Dialogue { speaker = "Shura", text = "wow."},
            new Dialogue { speaker = "Shura", text = "what the hell is wrong with you?"},
            new Dialogue { speaker = "Shura", text = "i can’t believe i actually thought you were a good friend"},
            // WORSE GLITCH 
            new Dialogue { speaker = "Jasper", text = "wait idk how this is happening shura, i really didn't write that"},
            // GLITCH AND WAIT - viewport shows: "well you thought wrong, considering you lack intelligence as well"
            new Dialogue { speaker = "Shura", text = "okay im done."},
            new Dialogue { speaker = "Shura", text = "you know jasper"},
            new Dialogue { speaker = "Shura", text = "i was actually starting to like you more than a friend but i'm glad i didn’t say anything cuz now i know what kind of a person you are"},
            new Dialogue { speaker = "Shura", text = "go to hell jasper"},
            new Dialogue { speaker = "Jasper", text = "wait just hear me out shura"},
            // viewport shows: "not if you go first! :)"
            // WORST GLITCH EVER
            // Shura blocks you

            // Error frenzy
            */
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
        if (inputField.interactable)
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
                    //linkClicked = false; 
                }
            }
        }
            
    }

    void SendMessage(bool isPlayer, string messageText)
    {
        GameObject messagePrefab = isPlayer ? playerMessagePrefab : npcMessagePrefab;

        GameObject newMessage = Instantiate(messagePrefab, messagesContainer.transform);

        TMP_Text messageContent = newMessage.GetComponent<TMP_Text>();

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
        

        yield return new WaitForSeconds(dialogues[dialogueIndex].delay);
        //Debug.Log(dialogues[dialogueIndex].delay); 

        while (dialogueIndex < dialogues.Count && dialogues[dialogueIndex].speaker == "Forrest") //changed this to while from if 
        {
            Dialogue currentDialogue = dialogues[dialogueIndex];
            SendMessage(false, currentDialogue.text);

            yield return new WaitForSeconds(currentDialogue.delay);

            if (currentDialogue.triggersAction)
            {
                PerformAction(currentDialogue.action); 
            }

            dialogueIndex++; 
        }

        yield return new WaitForSeconds(dialogues[dialogueIndex].delay);
        //Debug.Log(dialogues[dialogueIndex].delay);


        isPlayerTurn = true;

    }

    void ResetInputField()
    {
        inputField.text = "";
        inputField.caretPosition = 0;
        isTypingComplete = false;
    }


    IEnumerator PlayDialogues()
    {
        yield return new WaitForSeconds(dialogues[dialogueIndex].delay);
        //Debug.Log(dialogues[dialogueIndex].delay);


        while (dialogueIndex < dialogues.Count && dialogues[dialogueIndex].speaker == "Forrest")
        {
            Dialogue currentDialogue = dialogues[dialogueIndex];
            SendMessage(false, currentDialogue.text);

            if (currentDialogue.triggersAction)
            {
                PerformAction(currentDialogue.action);
            }

            dialogueIndex++;
            yield return new WaitForSeconds(currentDialogue.delay);
            //Debug.Log(dialogues[dialogueIndex].delay);

        }


        isPlayerTurn = true;

    }

    void ToggleInputField (bool isEnabled)
    {
        inputField.interactable = isEnabled;
        //Debug.Log($"InputField interactable set to: {isEnabled}");
    }


    void PerformAction(string action)
    {
        if (action == "SongLinkImage")
        {
            GameObject newImageButton = Instantiate(buttonLink, messagesContainer.transform);
            ToggleInputField(false);


            Button button = newImageButton.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    //Debug.Log("Image button clicked. Activating song panel...");
                    OpenMoozikPanel();
                });
            }

        } else if (action == "CopyCode")
        {
            ToggleInputField(false);
            const string mainFunctionText = "public void Main() {\n  Debug.Log(\"Hello\");\n}";

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
            {
                string clipboardText = GUIUtility.systemCopyBuffer;

                if (clipboardText == mainFunctionText) {
                    ToggleInputField(true);
                    inputField.text += clipboardText;
                    isTypingComplete = true;
                    Debug.Log("Pasted into messenger"); 
                } else
                {
                    Debug.LogWarning("Invalid text in clipboard."); 
                }
            }

            if (isTypingComplete && Input.GetKeyDown(KeyCode.Return))
            {
                SendMessage(true, mainFunctionText);
                dialogueIndex++;

                StartCoroutine(ScheduleNPCResponse());
                ResetInputField();
                //linkClicked = false; 
            }

        }
    }


    public void OpenMoozikPanel()
    {
        //Debug.Log("Opening Moozik application");
        moozikPanel.SetActive(true);
        ToggleInputField(true);
        //link1Clicked = true;
    }


    private bool IsMouseInput()
    {
        return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
    }
}


