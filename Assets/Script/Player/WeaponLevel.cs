using UnityEngine;

public class WeaponLevel : MonoBehaviour
{
    [SerializeField] private string currentWeaponType = "Sword";
    [SerializeField] private int currentLevel = 1;

    private WeaponMeleeStats currentWeaponStats; // Lưu trữ thông tin vũ khí từ WeaponLevelManager

    public void SetCurrentWeaponStats(WeaponMeleeStats stats)
    {
        currentWeaponStats = stats;
    }

    public WeaponMeleeStats GetCurrentWeaponStats()
    {
        return currentWeaponStats;
    }

    public string GetWeaponType()
    {
        return currentWeaponType;
    }

    public int GetWeaponLevel()
    {
        return currentLevel;
    }

    // Thêm phương thức SetWeaponLevel
    public void SetWeaponLevel(int level)
    {
        if (level <= 0)
        {
            Debug.LogWarning("Cấp độ không hợp lệ! Giữ nguyên cấp độ hiện tại.");
            return;
        }

        currentLevel = level;
        Debug.Log($"Cấp độ vũ khí được đặt thành {currentLevel}");
    }

    // Nếu cần, thêm SetWeaponType để đồng nhất với WeaponChangeManager
    public void SetWeaponType(string weaponType)
    {
        if (string.IsNullOrEmpty(weaponType))
        {
            Debug.LogWarning("Loại vũ khí không hợp lệ!");
            return;
        }

        currentWeaponType = weaponType;
        Debug.Log($"Loại vũ khí được đặt thành {currentWeaponType}");
    }
}
