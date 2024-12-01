using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Assets.SimpleLocalization.Scripts;
using System;

public class GraphicsSetting : MonoBehaviour
{
    [SerializeField] private int currentSettingLevel; // 0 is low 1 is medium 2 is high by default
    public int CurrentSettingLevel { get => currentSettingLevel; }
    int previousSettingLevel;
    [SerializeField] private List<GameObject> _graphicButton;
    [SerializeField] TMP_Dropdown _dropDown;

    private void Start()
    {
        if (_dropDown == null)
        {
            Debug.LogError("Dropdown is not assigned in the Inspector.");
            return;
        }
        _dropDown.onValueChanged.AddListener(OnClickChangeSetting);
    }



    //private void OnEnable() 
    //{
    //    AddOptionList();
    //    InitSettingInfo();
    //}

    public void InitData()
    {
        AddOptionList();
        InitSettingInfo();
    }

    private void InitSettingInfo()
    {
        currentSettingLevel = PlayerPrefs.GetInt("GraphicsSetting", 1); // Mặc định là Medium nếu không có dữ liệu
        SetGraphicsSetting(currentSettingLevel);
        QualitySettings.SetQualityLevel(currentSettingLevel);
        _dropDown.value = currentSettingLevel;
    }


    public void OnClickChangeSetting(int settingLevel)
    {
        if (settingLevel < 0 || settingLevel > 2)
        {
            Debug.LogError("Invalid graphics setting level selected.");
            return;
        }

        previousSettingLevel = currentSettingLevel;
        currentSettingLevel = settingLevel;
        SetGraphicsSetting(settingLevel);
        QualitySettings.SetQualityLevel(settingLevel);
    }
        
    void SetGraphicsSetting(int settingLevel)
    {
        Debug.Log($"Setting graphics quality to {((GameQuality)settingLevel).ToString()}");

        // Các thiết lập tương ứng
        switch (settingLevel)
        {
            case 0: QualitySettings.shadowResolution = ShadowResolution.Low; QualitySettings.antiAliasing = 0; break;
            case 1: QualitySettings.shadowResolution = ShadowResolution.Medium; QualitySettings.antiAliasing = 2; break;
            case 2: QualitySettings.shadowResolution = ShadowResolution.High; QualitySettings.antiAliasing = 4; break;
            default: Debug.LogWarning("Invalid graphics setting level."); break;
        }
    }


    public void SaveSetting()
    {
        previousSettingLevel = currentSettingLevel;
        PlayerPrefs.SetInt("GraphicsSetting", currentSettingLevel);
        PlayerPrefs.Save();
    }


    public void RevertGraphicsSetting()
    {
        currentSettingLevel = previousSettingLevel;
        SetGraphicsSetting(previousSettingLevel);
        QualitySettings.SetQualityLevel(previousSettingLevel);
    }

    private void AddOptionList()
    {
        List<string> list = new List<string>(Enum.GetNames(typeof(GameQuality)));
        _dropDown.ClearOptions();
        _dropDown.AddOptions(list);
    }

}
