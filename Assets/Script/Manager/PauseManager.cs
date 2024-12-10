using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    void Start()
    {
        exitButton.onClick.AddListener((ExitGame));
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game has exited."); // Chỉ hoạt động trong Editor
    }

}
