using UnityEngine;
using TMPro;
using Assets.SimpleLocalization.Scripts;

[System.Serializable]
public class LevelData
{
    public string level;         // Số cấp độ (dùng hiển thị trực tiếp)
    public string levelNameKey;  // LocalizationKey cho tên cấp độ
    public int expThreshold;     // Kinh nghiệm cần thiết để lên cấp
}

public class PlayerExpManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levels;      // Danh sách thông tin cấp độ
    private int currentExp = 0;                       // Kinh nghiệm hiện tại
    private int currentLevelIndex = 0;               // Index của cấp độ hiện tại

    public PlayerExpBar expBar;                      // Tham chiếu tới thanh exp
    public PlayerLevelManager levelManager;          // Tham chiếu tới quản lý cấp độ
    public TMP_Text levelUIText;                     // Hiển thị số cấp độ
    public TMP_Text levelNameText;                   // Hiển thị tên cấp độ
    [SerializeField] private SkillPanelManager skillPanelManager;

    void Start()
    {
        InitializeExp();
        UpdateLevelText(); // Hiển thị cấp độ ban đầu
    }

    private void InitializeExp()
    {
        currentExp = 0;
        expBar.UpdateExpBar(currentExp, levels[currentLevelIndex].expThreshold);
    }

    public void AddExp(int exp)
    {
        currentExp += exp;

        // Kiểm tra nếu đủ kinh nghiệm để lên cấp
        if (currentExp >= levels[currentLevelIndex].expThreshold)
        {
            currentExp -= levels[currentLevelIndex].expThreshold; // Reset kinh nghiệm cho cấp độ tiếp theo
            LevelUp();
        }

        // Cập nhật thanh exp
        expBar.UpdateExpBar(currentExp, levels[currentLevelIndex].expThreshold);
    }

    private void LevelUp()
    {
        Debug.Log($"Level Up! New Level: {levels[currentLevelIndex].level}");

        if (currentLevelIndex < levels.Length - 1)
        {
            currentLevelIndex++;
            levelManager.LevelUp(); // Gọi chức năng lên cấp từ PlayerLevelManager
            AudioManager.Instance.PlayVFX("PlayerLevelUp");
        }
        else
        {
            Debug.Log("Đã đạt cấp độ tối đa!");
        }

        skillPanelManager.UpdateSkillPanel(currentLevelIndex); // Cập nhật hiển thị các panel kỹ năng

        UpdateLevelText();
        expBar.UpdateExpBar(currentExp, levels[currentLevelIndex].expThreshold);
    }

    private void UpdateLevelText()
    {
        // Cập nhật text số cấp độ
        levelUIText.text = levels[currentLevelIndex].level;

        // Cập nhật tên cấp độ qua LocalizationKey
        levelNameText.GetComponent<LocalizedTMPText>().LocalizationKey = levels[currentLevelIndex].levelNameKey;
    }
}
