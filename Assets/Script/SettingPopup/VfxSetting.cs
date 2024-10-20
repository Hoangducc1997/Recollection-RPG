using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxSetting : SliderSetting
{
    protected override void LoadDataFromSetting()
    {
        base.LoadDataFromSetting();
        HandleSlider(SettingManager.Instance.SettingSaveData.VfxVolume);
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
