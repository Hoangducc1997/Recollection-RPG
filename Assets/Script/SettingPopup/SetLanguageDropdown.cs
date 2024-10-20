using Assets.SimpleLocalization.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class SetLanguageDropdown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _curLabel;
    [SerializeField] private List<Sprite> listIconLanguages = new List<Sprite>();
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] GraphicsSetting _graphicSetting;
    string currentLanguage = "";
    bool isOpen = false;

    void OnEnable()
    {
        //var dropdown = GetComponent<Dropdown>();
        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();

        if (dropdown == null) dropdown = GetComponent<TMP_Dropdown>();

        Init();
        _curLabel.SetText(currentLanguage);

        // Fill the dropdown elements
        InitLanguageList();

        dropdown.onValueChanged.RemoveListener(OnValueChanged);
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    void Init()
    {
        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();
    }

    void OnValueChanged(int index)
    {
        if (index < 0)
        {
            index = 0;
            dropdown.value = index;
        }
        currentLanguage = dropdown.options[index].text;
        //LocalizationManager.CurrentLanguage = dropdown.options[index].text;
        _curLabel.SetText(dropdown.options[index].text);
        _graphicSetting.InitData();
    }

    public void RevertLanguage()
    {
        // = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();
    }

    private void InitLanguageList()

    {
        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();
        _curLabel.SetText(currentLanguage);

        List<string> languages = new List<string>();
        foreach (Language language in Enum.GetValues(typeof(Language)))
        {
            languages.Add(language.ToString());
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(languages);

        dropdown.value = languages.IndexOf(currentLanguage);
        _curLabel.SetText(currentLanguage);
        dropdown.onValueChanged.RemoveListener(OnValueChanged);
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        InitLanguageList();
    }
}