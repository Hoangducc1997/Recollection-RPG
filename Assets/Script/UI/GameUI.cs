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

    void Start()
    {
        ShowPopupForCurrentScene();
    }

    void ShowPopupForCurrentScene()
    {
        // Get the current scene's name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Loop through the array to check for a matching scene
        foreach (var ui in uiInScenes)
        {
            if (ui.sceneName == currentSceneName)
            {
                // Show the popup for the current scene and hide any other active popups
                UIManager.Instance.ShowPopup(ui.popup, null, true);
                Debug.Log("Current Scene: " + currentSceneName);
                break;
            }
        }
    }
}
