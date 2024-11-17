using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // To manage scene transitions

public class ScrollingText : MonoBehaviour
{
    [SerializeField] private Text uiText; // Reference to the Text UI component
    [SerializeField] private float revealSpeed = 0.1f; // Time interval to reveal each character
    [SerializeField] private Button revealAllButton; // Reference to the Button UI component
    [SerializeField] private UnityEngine.GameObject panelPilotText;
    [SerializeField] private UnityEngine.GameObject buttonNextScene;

    private string fullText; // The full text to display
    private int currentCharIndex = 0; // Tracks the current character to reveal
    private bool isRevealing = true; // Indicates if text is revealing or not
    private int buttonClickCount = 0; // To track the number of button clicks

    void Start()
    {
        panelPilotText.SetActive(true);
        buttonNextScene.SetActive(false);
        // Store the full text and start with an empty display
        fullText = uiText.text;
        uiText.text = "";

        // Start revealing the text
        StartCoroutine(RevealText());

        // Add listener for the button click
        revealAllButton.onClick.AddListener(OnButtonClick);
    }

    IEnumerator RevealText()
    {
        // Loop through all characters in the fullText
        while (currentCharIndex < fullText.Length)
        {
            // Add the next character to the displayed text
            uiText.text += fullText[currentCharIndex];
            currentCharIndex++;

            // Wait for the revealSpeed time before showing the next character
            yield return new WaitForSeconds(revealSpeed);
        }

        // After the text is fully revealed, stop further revealing
        isRevealing = false;
    }


    // Handle button clicks based on the current state
    private void OnButtonClick()
    {
        buttonClickCount++; // Increase the button click count

        if (buttonClickCount == 1)
        {
            // On first click, reveal all text
            RevealAllText();
        }
        else if (buttonClickCount == 2)
        {
            // On second click, switch to the next scene
            panelPilotText.SetActive(false);
            buttonNextScene.SetActive(true);
        }
    }

    // Function to instantly reveal all the text when the button is pressed
    private void RevealAllText()
    {
        if (isRevealing)
        {
            StopAllCoroutines(); // Stop the revealing coroutine
            uiText.text = fullText; // Show all the text immediately
            isRevealing = false; // Mark as finished revealing
        }
    }
}
