using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSettings : MonoBehaviour
{
    public static MusicSettings Instance { get; private set; }

    public float MusicVolume { get; private set; } = 0.5f;
    public float SFXVolume { get; private set; } = 0.5f;

    public bool IsMusicMuted { get; private set; } = false;
    public bool IsSFXMuted { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float GetCurrentMusicVolume()
    {
        return MusicVolume;
    }

    public float GetCurrentVfxVolume()
    {
        return SFXVolume;
    }

    public void SetVfxVolume(float value)
    {
        SFXVolume = value;
        IsSFXMuted = SFXVolume == 0; // Cập nhật trạng thái mute
    }

    public void SetMusicVolume(float value)
    {
        MusicVolume = value;
        IsMusicMuted = MusicVolume == 0;
    }

    public void SetSFXVolume(float value)
    {
        SFXVolume = value;
        IsSFXMuted = SFXVolume == 0;
    }

    public void ToggleMusicMute()
    {
        IsMusicMuted = !IsMusicMuted;
        MusicVolume = IsMusicMuted ? 0 : 0.5f; // Default volume khi bật lại
    }

    public void ToggleSFXMute()
    {
        IsSFXMuted = !IsSFXMuted;
        SFXVolume = IsSFXMuted ? 0 : 0.5f;
    }
}
