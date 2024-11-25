using UnityEngine;
using System.Linq;
using System;

public class SettingPopup : UIPopup
{
    [SerializeField] private MusicSettings musicSettings;
    [SerializeField] private GraphicsSetting graphicsSetting;

    public override void OnShown(object parameter = null)
    {
        base.OnShown(parameter);
        LoadSetting(); // Restore settings when the popup is shown
        ShowSettingContent(true);
    }

    /// <summary>
    /// Displays or hides the setting content.
    /// </summary>
    /// <param name="isShow">True to show, false to hide.</param>
    public void ShowSettingContent(bool isShow)
    {
        if (isShow)
        {
            graphicsSetting?.InitData();
        }
    }

    /// <summary>
    /// Handles back button click logic.
    /// </summary>
    public override bool OnBackClick()
    {
        return base.OnBackClick();
    }

    /// <summary>
    /// Closes the popup when back button is clicked.
    /// </summary>
    public void OnClickBackButton()
    {
        UIManager.Instance?.HidePopup(PopupName.Setting);
    }

    /// <summary>
    /// Saves settings and closes the popup when OK button is clicked.
    /// </summary>
    public void OnClickOK()
    {
        Debug.unityLogger.logEnabled = true;
        SaveSetting();
        UIManager.Instance?.HidePopup(PopupName.Setting);
    }

    /// <summary>
    /// Saves all current settings to the setting manager and PlayerPrefs.
    /// </summary>
    private void SaveSetting()
    {
        var settingManager = SettingManager.Instance;

        if (settingManager == null)
        {
            Debug.LogError("SettingManager instance is null.");
            return;
        }

        // Save graphics settings
        settingManager.SettingSaveData.GameQuality = (GameQuality)graphicsSetting.CurrentSettingLevel;

        // Save music and VFX settings
        settingManager.SettingSaveData.MusicVolume = musicSettings.GetCurrentMusicVolume();
        settingManager.SettingSaveData.VfxVolume = musicSettings.GetCurrentVfxVolume();

        // Save all settings to PlayerPrefs
        settingManager.SaveSettingSaveData();
    }


    /// <summary>
    /// Loads settings from the setting manager and updates the UI components accordingly.
    /// </summary>
    private void LoadSetting()
    {
        var settingManager = SettingManager.Instance;

        if (settingManager == null)
        {
            Debug.LogError("SettingManager instance is null.");
            return;
        }

        settingManager.LoadSettingSaveData();

        // Update the UI with loaded settings
        graphicsSetting?.InitData();
        musicSettings?.SetMusicVolume(settingManager.SettingSaveData.MusicVolume);
        musicSettings?.SetVfxVolume(settingManager.SettingSaveData.VfxVolume);
    }
}
