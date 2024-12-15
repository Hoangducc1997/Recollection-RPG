using UnityEngine;
using UnityEngine.UI;

public class PauseManager : UIPopup
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        exitButton.onClick.AddListener(ExitGame);
        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game has exited."); // Chỉ hoạt động trong Editor
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Ngừng thời gian trong game
        Debug.Log("Game Paused.");
    }

    private void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Tiếp tục thời gian trong game
        Debug.Log("Game Resumed.");
    }

    public void OnClickSetting()
    {
        UIManager.Instance.ShowPopup(PopupName.Setting);
    }
}
