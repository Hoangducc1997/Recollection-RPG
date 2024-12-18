using UnityEngine;

public class SkillButtonManager : MonoBehaviour
{
    public PlayerStats playerStats; // Tham chiếu đến PlayerStats
    public SkillPanelManager skillPanelManager; // Tham chiếu đến SkillPanelManager
    public WeaponLevelManager weaponLevelManager; // Tham chiếu đến WeaponLevelManager
    public PlayerHealthManager playerHealthManager; // Tham chiếu đến PlayerHealthManager

    private int currentLevel = 0; // Lưu cấp độ hiện tại của kiếm

    void Start()
    {
        currentLevel = 0; // Khởi đầu cấp độ kiếm là 0
    }

    public void IncreaseSpeed()
    {
        if (playerStats != null)
        {
            playerStats.IncreaseSpeed(); // Tăng tốc độ theo cấp độ
            Debug.Log($"New Speed: {playerStats.GetMoveSpeed()}");
        }
        else
        {
            Debug.LogWarning("PlayerStats is not assigned!");
        }
        CloseSkillPanel();
    }


    public void UpgradeSword()
    {
        if (currentLevel + 1 < weaponLevelManager.swordWeapons.Length)
        {
            currentLevel++; // Tăng cấp độ
            Debug.Log($"Upgrading sword to Level {currentLevel}");

            weaponLevelManager.UnlockWeapon(currentLevel, WeaponType.Sword); // Mở khóa kiếm
            weaponLevelManager.SwitchWeapon(currentLevel, WeaponType.Sword); // Chuyển vũ khí
        }
        else
        {
            Debug.LogWarning("No more sword levels available!");
        }

        CloseSkillPanel(); // Đóng bảng kỹ năng
    }

    public void HealthIncrease(int healthIncreaseAmount)
    {
        if (playerHealthManager != null)
        {
            playerHealthManager.IncreaseHealth(healthIncreaseAmount); // Tăng máu
            Debug.Log($"Health increased by {healthIncreaseAmount}. Current Health: {playerHealthManager.CurrentHealth}");
        }
        else
        {
            Debug.LogWarning("PlayerHealthManager is not assigned!");
        }
        CloseSkillPanel();
    }

    private void CloseSkillPanel()
    {
        skillPanelManager.ResumeGame();
    }
}
