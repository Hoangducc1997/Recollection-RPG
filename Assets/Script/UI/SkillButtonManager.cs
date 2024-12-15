using UnityEngine;

public class SkillButtonManager : MonoBehaviour
{
    public PlayerStats playerStats; // Tham chiếu đến PlayerStats
    public SkillPanelManager skillPanelManager; // Tham chiếu đến SkillPanelManager
    public WeaponLevelManager weaponLevelManager; // Thêm tham chiếu đến WeaponLevelManager

    private int currentLevel = 0; // Lưu cấp độ hiện tại của kiếm

    void Start()
    {
        currentLevel = 0; // Khởi đầu cấp độ kiếm là 0
    }
    public void IncreaseSpeed(int level)
    {
        playerStats.IncreaseSpeedByLevel(level); // Gọi phương thức với cấp độ cụ thể
        Debug.Log($"Speed upgraded at Level {level}. New Speed: {playerStats.GetMoveSpeed()}");
        CloseSkillPanel(); // Đóng bảng kỹ năng
    }

    public void UpgradeSword()
    {
        if (currentLevel + 1 < weaponLevelManager.swordWeapons.Length)
        {
            currentLevel++; // Tăng cấp độ
            weaponLevelManager.UnlockWeapon(currentLevel, WeaponType.Sword); // Mở khóa kiếm
            weaponLevelManager.SwitchWeapon(currentLevel, WeaponType.Sword); // Chuyển vũ khí
            Debug.Log($"Sword upgraded to Level {currentLevel}");
        }
        else
        {
            Debug.Log("No more sword levels available!");
        }

        CloseSkillPanel(); // Đóng bảng kỹ năng
    }



    private void CloseSkillPanel()
    {
        skillPanelManager.ResumeGame();
    }
}
