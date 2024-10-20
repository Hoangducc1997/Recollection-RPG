using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [System.Serializable]
    public class UIInScene
    {
        public string sceneName;  // Name of the scene
        public PopupName popup;   // Corresponding popup to show
    }

    [SerializeField] private UIInScene[] uiInScenes;  // Array of scenes and their corresponding popups

    private void Start()
    {
        // Show popup for the current scene on start
        ShowPopupForCurrentScene();

        // Subscribe to the sceneLoaded event to update the popup when the scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // This method is called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Hide the current popup before showing a new one
        if (UIManager.Instance.CurrentPopup != null)
        {
            UIManager.Instance.HidePopup(UIManager.Instance.CurrentPopup.GetPopupName());
        }

        // Show the popup for the new scene
        ShowPopupForCurrentScene();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void ShowPopupForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        foreach (var ui in uiInScenes)
        {
            if (ui.sceneName == currentSceneName)
            {
                Debug.Log("Showing popup for scene: " + currentSceneName);
                UIManager.Instance.ShowPopup(ui.popup, null, true);  // Ensure the popup is shown
                break;
            }
        }
    }
}
