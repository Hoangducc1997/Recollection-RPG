using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingPopup : UIPopup
{
    [SerializeField] MusicSettings musicSettings;
    [SerializeField] VfxSetting VfxSetting;
    [SerializeField] GraphicsSetting graphicsSetting;
    [SerializeField] SetLanguageDropdown setLanguageDropdown;


    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        ShowSettingContent(true);
    }

    public void ShowSettingContent(bool isShow)
    {
        if (isShow)
        {
            graphicsSetting.InitData();
        }
    }
    public override bool OnBackClick()
    {
        return base.OnBackClick();
    }
    #region Click Method

    public void OnClickBackButton()
    {
        UIManager.Instance.HidePopup(PopupName.Setting);
        
    }
    #endregion


    public void OnClickOK()
    {
        Debug.unityLogger.logEnabled = true;

        SaveSetting();

        UIManager.Instance.HidePopup(PopupName.Setting);
    }

    void SaveSetting()
    {
        // 0 is low 1 is medium 2 is high by default

        switch (graphicsSetting.CurrentSettingLevel)
        {

            case 0:
                Application.targetFrameRate = 30;
                break;
            case 1:
                Application.targetFrameRate = 60;

                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
            default:
                break;
        }
        QualitySettings.lodBias = 1f;
        
    }

    void RevertSetting()
    {
        graphicsSetting.RevertGraphicsSetting();
        setLanguageDropdown.RevertLanguage();
        musicSettings.RevertSetting();
    }

}

