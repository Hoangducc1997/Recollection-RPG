using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGamePopup : UIPopup
{
    [SerializeField] private string nextSceneName;

    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        ShowSettingContent(true);
    }

    public void ShowSettingContent(bool isShow)
    {

    }

    #region Click Method

    public void StartGame()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion


        
    public void OnClickSetting()
    {
        UIManager.Instance.ShowPopup(PopupName.Setting);
    }
}