using Assets.SimpleLocalization.Scripts;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class SetLanguageDropdown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI currentLabel;
    [SerializeField] private TMP_Dropdown dropdown;

    private string currentLanguage = "";

    private void OnEnable()
    {
        if (dropdown == null) dropdown = GetComponent<TMP_Dropdown>();

        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();
        LocalizationManager.Language = currentLanguage;

        InitLanguageList();
        currentLabel.SetText(currentLanguage);

        dropdown.onValueChanged.RemoveListener(OnValueChanged);
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    public void OnValueChanged(int index)
    {
        currentLanguage = dropdown.options[index].text;
        LocalizationManager.Language = currentLanguage;

        currentLabel.SetText(currentLanguage);

        SettingManager.Instance.SettingSaveData.GameLanguage =
            (Language)Enum.Parse(typeof(Language), currentLanguage);

        SettingManager.Instance.SaveSettingSaveData();
    }

    public Language GetCurrentLanguage()
    {
        return SettingManager.Instance.SettingSaveData.GameLanguage;
    }

    public void SetCurrentLanguage(Language language)
    {
        SettingManager.Instance.SettingSaveData.GameLanguage = language;
        LocalizationManager.Language = language.ToString(); // Cập nhật ngôn ngữ trong hệ thống
        currentLabel.SetText(language.ToString()); // Cập nhật UI
    }



    private void InitLanguageList()
    {
        List<string> languages = new List<string>();
        foreach (Language lang in Enum.GetValues(typeof(Language)))
        {
            languages.Add(lang.ToString());
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(languages);
        dropdown.value = languages.IndexOf(currentLanguage);
    }

    public void RevertLanguage()
    {
        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();
        LocalizationManager.Language = currentLanguage;

        currentLabel.SetText(currentLanguage);
        InitLanguageList();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InitLanguageList();
    }
}
