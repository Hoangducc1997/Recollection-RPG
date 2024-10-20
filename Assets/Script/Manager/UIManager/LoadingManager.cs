using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static string nextScene = "MenuGame";  // Scene to load next
    [SerializeField] private GameObject processBar;  // Progress bar GameObject
    [SerializeField] private Text textPercent;  // Text to show percentage progress
    [SerializeField] float fixedLoadingTime = 3f;  // Time for fixed loading
    [SerializeField] Button touchToPlay;  // Button to start loading the scene
    [SerializeField] GameObject LoadingBar;  // Game include LoadingBar

    private void Start()
    {
        // Hide the "TouchToPlay" button at the start
        touchToPlay.gameObject.SetActive(false);

        // Start loading the scene with the progress bar
        StartCoroutine(LoadSceneFixedTime());
    }

    // Load the scene with a fixed time for the loading screen
    public IEnumerator LoadSceneFixedTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fixedLoadingTime)
        {
            float process = Mathf.Clamp01(elapsedTime / fixedLoadingTime);
            processBar.GetComponent<Image>().fillAmount = process;
            textPercent.text = (process * 100).ToString("0") + "%";
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        LoadingBar.SetActive(false);

        // After loading completes, show the TouchToPlay button
        touchToPlay.gameObject.SetActive(true);
        touchToPlay.interactable = true;  // Ensure button is interactable

        // Add listener to the button for scene transition when clicked
        touchToPlay.onClick.AddListener(OnTouchToPlayClicked);
    }

    // Called when the "TouchToPlay" button is clicked
    private void OnTouchToPlayClicked()
    {
        // Disable the button to prevent multiple clicks
        touchToPlay.interactable = false;

        // Start loading the scene asynchronously
        StartCoroutine(LoadSceneAsync(nextScene));
    }

    // Load scene asynchronously
    public IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            processBar.GetComponent<Image>().fillAmount = progress;
            textPercent.text = (progress * 100).ToString("0") + "%";
            yield return null;
        }
    }
}
