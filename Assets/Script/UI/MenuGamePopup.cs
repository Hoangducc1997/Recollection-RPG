using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGamePopup : UIPopup
{


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
        UIManager.Instance.HidePopup(PopupName.MainMenu);
        // Bat dau man hinh game
    }
    #endregion


    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnClickSetting()
    {
        UIManager.Instance.ShowPopup(PopupName.SettingPopup);
    }
}