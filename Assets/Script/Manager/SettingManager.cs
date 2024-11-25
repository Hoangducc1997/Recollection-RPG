using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }

    [Header("Frame Settings")]
    public float TargetFrameRate = 60.0f;

    [Header("Saved Settings")]
    public SettingSaveData SettingSaveData;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Tải dữ liệu cài đặt
        LoadSettingSaveData();
    }

    public void SaveSettingSaveData()
    {
        PlayerPrefs.SetFloat("MusicVolume", SettingSaveData.MusicVolume);
        PlayerPrefs.SetFloat("VfxVolume", SettingSaveData.VfxVolume);
        PlayerPrefs.SetInt("GameLanguage", (int)SettingSaveData.GameLanguage);
        PlayerPrefs.SetInt("GameQuality", (int)SettingSaveData.GameQuality);

        PlayerPrefs.Save();
        Debug.Log("Settings Saved!");
    }

    public void LoadSettingSaveData()
    {
        if (SettingSaveData == null)
        {
            SettingSaveData = new SettingSaveData();
        }

        SettingSaveData.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        SettingSaveData.VfxVolume = PlayerPrefs.GetFloat("VfxVolume", 1.0f);
        SettingSaveData.GameLanguage = (Language)PlayerPrefs.GetInt("GameLanguage", 0);
        SettingSaveData.GameQuality = (GameQuality)PlayerPrefs.GetInt("GameQuality", 0);

        ApplyGraphicsSettings();
    }

    private void ApplyGraphicsSettings()
    {
        if (SettingSaveData != null)
        {
            TargetFrameRate = SettingSaveData.GameQuality == GameQuality.Low ? 30f : 60f;
            Application.targetFrameRate = (int)TargetFrameRate;
        }
    }
}

[System.Serializable]
public class SettingSaveData
{
    public float MusicVolume;
    public float VfxVolume;
    public Language GameLanguage;
    public GameQuality GameQuality;
}

public enum Language
{
    English,
    Vietnamese
}

public enum GameQuality
{
    High,
    Low
}
