using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettings : SliderSetting
{
    protected override void LoadDataFromSetting()
    {
        base.LoadDataFromSetting();
        HandleSlider(SettingManager.Instance.SettingSaveData.MusicVolume);
    }
    protected override void HandleOutPutVolume(float value)
    {
        HandleSlider(value);
        AudioManager.Instance.GetMusicAudioSource().volume = value;
    }

    public override void RevertSetting()
    {
        base.RevertSetting();

        AudioManager.Instance.GetMusicAudioSource().volume = SettingManager.Instance.SettingSaveData.MusicVolume;
    }
}
