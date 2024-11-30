using System.Collections.Generic;
using UnityEngine;

public class WeaponLevelManager : MonoBehaviour
{
    [SerializeField] private WeaponMeleeDatabase weaponDatabase; // CSDL chứa danh sách vũ khí
    [SerializeField] private List<GameObject> weaponLevelChoose = new List<GameObject>();
    [SerializeField] private WeaponLevel weaponLevel; // Gắn trực tiếp trong Inspector
    private WeaponMeleeStats currentWeaponStats;      // Thông tin vũ khí hiện tại
    private int currentWeaponIndex = 0;

    private void Start()
    {
        UpdateCurrentWeaponStats(); // Khởi tạo vũ khí ban đầu
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponLevelChoose.Count)
        {
            Debug.LogError("weaponIndex không hợp lệ!");
            return;
        }

        // Tắt tất cả vũ khí
        foreach (var weapon in weaponLevelChoose)
        {
            if (weapon != null)
                weapon.SetActive(false);
        }

        // Bật vũ khí được chọn
        if (weaponLevelChoose[weaponIndex] != null)
            weaponLevelChoose[weaponIndex].SetActive(true);

        // Cập nhật loại vũ khí trong WeaponLevel
        string weaponType = GetWeaponTypeFromIndex(weaponIndex);
        weaponLevel?.SetWeaponType(weaponType);

        // Cập nhật thông tin vũ khí
        UpdateCurrentWeaponStats();
    }

    public void SetWeaponLevel(int level)
    {
        if (weaponLevel != null)
        {
            weaponLevel.SetWeaponLevel(level);
            UpdateCurrentWeaponStats();
        }
    }

    private void UpdateCurrentWeaponStats()
    {
        currentWeaponStats = weaponDatabase.weapons.Find(w =>
            w.weaponName == weaponLevel.GetWeaponType() && w.level == weaponLevel.GetWeaponLevel());

        if (currentWeaponStats != null)
        {
            weaponLevel.SetCurrentWeaponStats(currentWeaponStats); // Cập nhật vào WeaponLevel
            Debug.Log($"Vũ khí hiện tại: {currentWeaponStats.weaponName}, cấp {currentWeaponStats.level}");
        }
        else
        {
            Debug.LogWarning($"Không tìm thấy vũ khí '{weaponLevel.GetWeaponType()}' cấp {weaponLevel.GetWeaponLevel()}!");
            SetDefaultWeapon();
        }
    }


    private void SetDefaultWeapon()
    {
        // Đặt vũ khí mặc định
        currentWeaponStats = weaponDatabase.weapons.Find(w => w.weaponName == "Sword" && w.level == 1);
        if (currentWeaponStats != null)
        {
            weaponLevel.SetWeaponType("Sword");
            weaponLevel.SetWeaponLevel(1);
            Debug.Log("Đã đặt vũ khí mặc định: Sword cấp 1.");
        }
        else
        {
            Debug.LogError("Không thể đặt vũ khí mặc định! Hãy kiểm tra WeaponDatabase.");
        }
    }

    private string GetWeaponTypeFromIndex(int index)
    {
        switch (index)
        {
            case 0: return "Sword";
            case 1: return "Bow";
            case 2: return "Magic";
            default: return null;
        }
    }

    public WeaponMeleeStats GetCurrentWeaponStats()
    {
        return currentWeaponStats;
    }
}
