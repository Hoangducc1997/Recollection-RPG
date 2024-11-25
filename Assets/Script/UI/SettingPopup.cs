using UnityEngine;

public class SettingPopup : UIPopup
{
    [SerializeField] private MusicSettings musicSettings;
    [SerializeField] private GraphicsSetting graphicsSetting;
    [SerializeField] private SetLanguageDropdown setLanguageDropdown;

    public override void OnShown(object parament = null)
    {
        base.OnShown(parament);
        LoadSetting(); // Khôi phục cài đặt
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

    public void OnClickBackButton()
    {
        UIManager.Instance.HidePopup(PopupName.Setting);
    }

    public void OnClickOK()
    {
        Debug.unityLogger.logEnabled = true;

        SaveSetting();
        UIManager.Instance.HidePopup(PopupName.Setting);
    }

    private void SaveSetting()
    {
        var settingManager = SettingManager.Instance;

        // Lưu cài đặt đồ họa
        settingManager.SettingSaveData.GameQuality = (GameQuality)graphicsSetting.CurrentSettingLevel;

        // Lưu cài đặt âm nhạc
        settingManager.SettingSaveData.MusicVolume = musicSettings.GetCurrentMusicVolume();
        settingManager.SettingSaveData.VfxVolume = musicSettings.GetCurrentVfxVolume();

        // Lưu cài đặt ngôn ngữ
        settingManager.SettingSaveData.GameLanguage = setLanguageDropdown.GetCurrentLanguage();

        // Lưu tất cả vào PlayerPrefs
        settingManager.SaveSettingSaveData();
    }

    private void LoadSetting()
    {
        var settingManager = SettingManager.Instance;

        if (settingManager == null) return;

        settingManager.LoadSettingSaveData();

        // Cập nhật UI theo giá trị đã lưu
        graphicsSetting.InitData();
        musicSettings.SetMusicVolume(settingManager.SettingSaveData.MusicVolume);
        musicSettings.SetVfxVolume(settingManager.SettingSaveData.VfxVolume);
        setLanguageDropdown.SetCurrentLanguage(settingManager.SettingSaveData.GameLanguage);
    }
}
