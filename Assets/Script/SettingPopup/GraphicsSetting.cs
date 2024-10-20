using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using Assets.SimpleLocalization.Scripts;

public class GraphicsSetting : MonoBehaviour
{
    [SerializeField] private int currentSettingLevel; // 0 is low 1 is medium 2 is high by default
    public int CurrentSettingLevel { get => currentSettingLevel; }
    int previousSettingLevel;
    [SerializeField] private List<GameObject> _graphicButton;
    [SerializeField] TMP_Dropdown _dropDown;

    private void Start()
    {
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

        currentSettingLevel = (int)SettingManager.Instance.SettingSaveData.GameQuality;

        SetGraphicsSetting(currentSettingLevel);

        QualitySettings.SetQualityLevel(currentSettingLevel);

        _dropDown.value = currentSettingLevel;
    }

    public void OnClickChangeSetting(int settingLevel)
    {
        previousSettingLevel = currentSettingLevel;
        currentSettingLevel = settingLevel;
        SetGraphicsSetting(settingLevel);
        QualitySettings.SetQualityLevel(settingLevel);
    }

    void SetGraphicsSetting(int settingLevel)
    {
        switch (settingLevel)
        {
            case 0:
                
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public void SaveSetting()
    {
        previousSettingLevel = currentSettingLevel;
    }

    public void RevertGraphicsSetting()
    {
        currentSettingLevel = previousSettingLevel;
        SetGraphicsSetting(previousSettingLevel);
        QualitySettings.SetQualityLevel(previousSettingLevel);
    }

    private void AddOptionList()
    {
        List<string> list = new List<string>();
        string option1 = string.Format(GameQuality.Low.ToString());
        string option2 = string.Format(GameQuality.High.ToString());

        list.Add(option1);
        list.Add(option2);

        _dropDown.ClearOptions();
        _dropDown.AddOptions(list);
    }
}
