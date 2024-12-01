using UnityEngine;

public class SkillButtonManager : MonoBehaviour
{
    public PlayerStats playerStats; // Tham chiếu đến PlayerStats
    public SkillPanelManager skillPanelManager; // Tham chiếu đến SkillPanelManager
    private int currentLevel; // Lưu cấp độ hiện tại

    void Start()
    {
        currentLevel = playerStats.GetCurrentLevel(); // Lấy cấp độ hiện tại từ PlayerStats
    }

    public void IncreaseSpeed(int level)
    {
        playerStats.IncreaseSpeedByLevel(level); // Gọi phương thức với cấp độ cụ thể
        Debug.Log($"Speed upgraded at Level {level}. New Speed: {playerStats.GetMoveSpeed()}");
        CloseSkillPanel(); // Đóng bảng kỹ năng
    }


    public void UpgradeWeapon()
    {
        Debug.Log($"Weapon upgraded at Level {currentLevel}");
        CloseSkillPanel(); // Đóng bảng kỹ năng
    }

    private void CloseSkillPanel()
    {
        skillPanelManager.ResumeGame();
    }
}
