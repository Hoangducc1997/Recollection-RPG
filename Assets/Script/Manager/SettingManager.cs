using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public static SettingManager Instance { get; private set; }

    public SettingSaveData SettingSaveData { get; private set; }

    [Header("Frame Settings")]
    public float TargetFrameRate = 60.0f;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Keep instance across scenes
        InitializeSettings();
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

    private void InitializeSettings()
    {
        SettingSaveData = new SettingSaveData();
        LoadSettingSaveData();
    }

    public void LoadSettingSaveData()
    {
        if (SettingSaveData == null)
        {
            SettingSaveData = new SettingSaveData();
        }

        SettingSaveData.MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        SettingSaveData.VfxVolume = PlayerPrefs.GetFloat("VfxVolume", 1.0f);

        // Kiểm tra giá trị hợp lệ cho GameLanguage
        int languageValue = PlayerPrefs.GetInt("GameLanguage", 0);
        if (Enum.IsDefined(typeof(Language), languageValue))
        {
            SettingSaveData.GameLanguage = (Language)languageValue;
        }
        else
        {
            SettingSaveData.GameLanguage = Language.English; // Giá trị mặc định
            Debug.LogWarning("Invalid GameLanguage value. Resetting to English.");
        }

        // Kiểm tra giá trị hợp lệ cho GameQuality
        int qualityValue = PlayerPrefs.GetInt("GameQuality", 0);
        if (Enum.IsDefined(typeof(GameQuality), qualityValue))
        {
            SettingSaveData.GameQuality = (GameQuality)qualityValue;
        }
        else
        {
            SettingSaveData.GameQuality = GameQuality.Medium; // Giá trị mặc định
            Debug.LogWarning("Invalid GameQuality value. Resetting to Medium.");
        }

        ApplyGraphicsSettings();
    }


    private void ApplyGraphicsSettings()
    {
        if (SettingSaveData != null)
        {
            TargetFrameRate = SettingSaveData.GameQuality == GameQuality.Low ? 30f : 60f;
            Application.targetFrameRate = (int)TargetFrameRate;

            // Áp dụng cài đặt đồ họa
            switch (SettingSaveData.GameQuality)
            {
                case GameQuality.Low:
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    QualitySettings.antiAliasing = 0;
                    break;

                case GameQuality.Medium:
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    QualitySettings.antiAliasing = 2;
                    break;

                case GameQuality.High:
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    QualitySettings.antiAliasing = 4;
                    break;
            }

            Debug.Log($"Graphics settings applied: {SettingSaveData.GameQuality}");
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
    Low,
    Medium,
    High
}
