using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class LevelData
{
    public string level; // Tên cấp độ
    public int expThreshold; // Kinh nghiệm cần thiết để lên cấp độ này
}

public class PlayerExpManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levels; // Danh sách thông tin cấp độ
    private int currentExp = 0;                 // Kinh nghiệm hiện tại
    private int currentLevelIndex = 0;         // Index của cấp độ hiện tại

    public PlayerExpBar expBar; // Tham chiếu tới thanh exp
    public PlayerLevelManager levelManager; // Tham chiếu tới quản lý cấp độ
    public TMP_Text levelUIText; 

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

        if (currentLevelIndex < levels.Length - 1) // Kiểm tra nếu còn cấp độ tiếp theo
        {
            currentLevelIndex++;
            levelManager.LevelUp(); // Gọi chức năng lên cấp từ PlayerLevelManager
        }
        else
        {
            Debug.Log("Đã đạt cấp độ tối đa!");
        }

        UpdateLevelText(); // Cập nhật UI Text khi lên cấp
        expBar.UpdateExpBar(currentExp, levels[currentLevelIndex].expThreshold); // Cập nhật thanh exp
    }

    private void UpdateLevelText()
    {
        levelUIText.text = $"LEVEL-{levels[currentLevelIndex].level}"; // Cập nhật text UI
    }
}
