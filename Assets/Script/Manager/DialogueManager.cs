using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static DialogueTrigger;

public class DialogueManager : UIPopup
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    
    int activeMessage = 0;
    public static bool isActive = false;

    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;
        Debug.Log("Started conversation! Load Message: " + messages.Length);
        DisplayMessage();  // Call to display the first message
        backgroundBox.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
    }


    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
        actorName.text = currentActors[messageToDisplay.actorId].name;
        actorImage.sprite = currentActors[messageToDisplay.actorId].sprite;
        AnimateTextColor();
        
    }

    public void NextMessage()
    {
        activeMessage++;
        if(activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Conversation Ended");
            //Ensure the box is hidden when conversation endeds
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo(); 
            isActive = false;
        }
    }

    void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);
    }

    void Start()
    {
        backgroundBox.localScale = Vector3.zero;  // Ensure the box is hidden when Start game
    }
    void Update()
    {
        if (isActive == true && currentMessages != null && currentActors != null && Input.GetMouseButtonDown(0))  // Detect tap or click
        {
            NextMessage();  // Go to the next message
        }
    }

}
