using UnityEngine;

public class WeaponLevel : MonoBehaviour
{
    [SerializeField] private WeaponMeleeDatabase weaponDatabase; // Database chứa thông tin vũ khí
    [SerializeField] private string currentWeaponType = "Sword"; // Loại vũ khí hiện tại
    [SerializeField] private int currentLevel = 1; // Cấp độ vũ khí hiện tại

    private WeaponMeleeStats currentWeaponStats; // Thông tin vũ khí hiện tại

    private void Start()
    {
        UpdateCurrentWeaponStats();
    }

    public void SetCurrentWeaponStats(WeaponMeleeStats stats)
    {
        currentWeaponStats = stats;
    }

    public WeaponMeleeStats GetCurrentWeaponStats()
    {
        return currentWeaponStats;
    }

    public string GetWeaponType() => currentWeaponType;

    public int GetWeaponLevel() => currentLevel;

    public void SetWeaponLevel(int level)
    {
        if (level <= 0)
        {
            Debug.LogWarning("Cấp độ không hợp lệ!");
            return;
        }

        currentLevel = level;
        UpdateCurrentWeaponStats();
    }

    public void SetWeaponType(string weaponType)
    {
        if (string.IsNullOrEmpty(weaponType))
        {
            Debug.LogWarning("Loại vũ khí không hợp lệ!");
            return;
        }

        currentWeaponType = weaponType;
        UpdateCurrentWeaponStats();
    }

    private void UpdateCurrentWeaponStats()
    {
        // Tìm thông tin vũ khí dựa trên loại và cấp độ
        currentWeaponStats = weaponDatabase.weapons.Find(w =>
            w.weaponName == currentWeaponType && w.level == currentLevel);

        if (currentWeaponStats != null)
        {
            Debug.Log($"Vũ khí hiện tại: {currentWeaponStats.weaponName}, cấp {currentWeaponStats.level}");
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy thông tin cho vũ khí '{currentWeaponType}' cấp {currentLevel}!");
        }
    }
}
