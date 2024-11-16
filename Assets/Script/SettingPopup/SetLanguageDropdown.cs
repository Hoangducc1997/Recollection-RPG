using Assets.SimpleLocalization.Scripts;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class SetLanguageDropdown : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _curLabel;
    [SerializeField] private List<Sprite> listIconLanguages = new List<Sprite>();
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] GraphicsSetting _graphicSetting;
    string currentLanguage = "";

    void OnEnable()
    {
        if (dropdown == null) dropdown = GetComponent<TMP_Dropdown>();

        // Khởi tạo ngôn ngữ theo cài đặt đã lưu
        currentLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();
        LocalizationManager.Language = currentLanguage; // Cập nhật ngôn ngữ trong LocalizationManager

        InitLanguageList();
        _curLabel.SetText(currentLanguage);

        // Thiết lập listener cho sự kiện thay đổi giá trị dropdown
        dropdown.onValueChanged.RemoveListener(OnValueChanged);
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    // Khi giá trị dropdown thay đổi
    public void OnValueChanged(int index)
    {
        if (index < 0) index = 0;  // Đảm bảo chỉ số hợp lệ

        // Lấy ngôn ngữ đã chọn từ dropdown
        currentLanguage = dropdown.options[index].text;

        // Cập nhật ngôn ngữ trong LocalizationManager
        LocalizationManager.Language = currentLanguage;

        // Cập nhật lại UI với ngôn ngữ đã chọn
        _curLabel.SetText(currentLanguage);

        // Cập nhật lại cài đặt đồ họa nếu cần
        _graphicSetting.InitData();

        // Lưu ngôn ngữ đã chọn vào SettingManager
        SettingManager.Instance.SettingSaveData.GameLanguage = (Language)Enum.Parse(typeof(Language), currentLanguage);
        SettingManager.Instance.SaveSettingSaveData();
    }



    // Phục hồi ngôn ngữ đã lưu
    public void RevertLanguage()
    {
        string savedLanguage = SettingManager.Instance.SettingSaveData.GameLanguage.ToString();
        LocalizationManager.Language = savedLanguage;
        _curLabel.SetText(savedLanguage);

        InitLanguageList();  // Khởi tạo lại danh sách ngôn ngữ
    }

    // Khởi tạo danh sách ngôn ngữ cho dropdown
    private void InitLanguageList()
    {
        List<string> languages = new List<string>();
        foreach (Language language in Enum.GetValues(typeof(Language)))
        {
            languages.Add(language.ToString());
        }

        dropdown.ClearOptions();
        dropdown.AddOptions(languages);

        // Đặt giá trị dropdown tương ứng với ngôn ngữ hiện tại
        dropdown.value = languages.IndexOf(LocalizationManager.Language);
    }

    // Khi nhấn vào dropdown, làm mới danh sách ngôn ngữ
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        InitLanguageList();
    }
}
