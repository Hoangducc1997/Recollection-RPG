using Assets.SimpleLocalization.Scripts;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Linq;


public class SetLanguageDropdown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI currentLabel;
    [SerializeField] private TMP_Dropdown dropdown;

    private string currentLanguage = "";

    private void Awake()
    {
        if (dropdown == null)
        {
            Debug.LogError("Dropdown component is not assigned!");
        }

        if (currentLabel == null)
        {
            Debug.LogError("Current Label component is not assigned!");
        }
    }

    private void OnEnable()
    {
        if (dropdown == null) dropdown = GetComponent<TMP_Dropdown>();

        // Get the language from the saved data
        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();

        // Ensure the default language if no valid value is found
        if (!Enum.IsDefined(typeof(Language), currentLanguage))
        {
            currentLanguage = Language.English.ToString(); // Default language
        }

        // Update the localization system
        LocalizationManager.Language = currentLanguage;

        InitLanguageList();
        currentLabel.SetText(currentLanguage);

        dropdown.onValueChanged.RemoveListener(OnValueChanged);
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private Dictionary<string, string> languageMapping = new Dictionary<string, string>
    {
        { "English", "English" },
        { "Tiếng Việt", "Vietnamese" }
    };

    public void OnValueChanged(int index)
    {
        // Get the display string from the Dropdown
        string selectedDisplayName = dropdown.options[index].text;

        // Map the display string to a valid value for LocalizationManager
        if (languageMapping.TryGetValue(selectedDisplayName, out string mappedLanguage))
        {
            LocalizationManager.Language = mappedLanguage;

            // Update the current label
            currentLabel.SetText(selectedDisplayName);

            // Save the setting
            SettingManager.Instance.SettingSaveData.GameLanguage =
                (Language)Enum.Parse(typeof(Language), mappedLanguage);

            SettingManager.Instance.SaveSettingSaveData();
        }
        else
        {
            Debug.LogError($"Language mapping not found for: {selectedDisplayName}");
        }
    }

    // New method to get the current language
    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }

    public void SetCurrentLanguage(Language language)
    {
        currentLanguage = language.ToString();
        SettingManager.Instance.SettingSaveData.GameLanguage = language;
        LocalizationManager.Language = currentLanguage;

        currentLabel.SetText(currentLanguage);
        InitLanguageList(); // Update the dropdown list
    }

    private void InitLanguageList()
    {
        List<string> displayNames = new List<string>();

        foreach (var pair in languageMapping)
        {
            displayNames.Add(pair.Key); // Display friendly names like "English", "Tiếng Việt"
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(displayNames);

        // Set the current value in the dropdown
        string currentDisplayName = languageMapping.FirstOrDefault(x => x.Value == currentLanguage).Key;
        dropdown.value = displayNames.IndexOf(currentDisplayName);
    }

    public void RevertLanguage()
    {
        // Revert to previous language
        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();

        if (!Enum.IsDefined(typeof(Language), currentLanguage))
        {
            currentLanguage = Language.English.ToString(); // Default language
        }

        LocalizationManager.Language = currentLanguage;

        currentLabel.SetText(currentLanguage);
        InitLanguageList(); // Sync with dropdown
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        InitLanguageList();
    }
}
