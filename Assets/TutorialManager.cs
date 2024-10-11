using System.Collections;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject touchSkipTutorial;

    private void Start()
    {
        touchSkipTutorial.SetActive(true); // Enable the object when the game starts
    }

    private void Update()
    {
        DetectTouch(); // Check for touch input every frame
    }

    private void DetectTouch()
    {
        // Check if the player touches the screen on mobile or clicks the left mouse button on PC
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            touchSkipTutorial.SetActive(false); // Disable the touchSkipTutorial object
        }
    }
}
