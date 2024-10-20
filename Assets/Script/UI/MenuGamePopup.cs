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
        UIManager.Instance.ShowPopup(PopupName.IntroPilot);
        // Bat dau man hinh game

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