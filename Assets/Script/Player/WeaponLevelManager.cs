using System.Collections.Generic;
using UnityEngine;

public class WeaponLevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponLevelChoose = new List<GameObject>();
    [SerializeField] private List<WeaponLevel> weaponLevels = new List<WeaponLevel>(); // Cận chiến
    [SerializeField] private List<WeaponRangedStats> rangedWeaponLevels = new List<WeaponRangedStats>(); // Tầm xa
    private int currentWeaponIndex = 0;

    private void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }

    public object GetCurrentWeaponStats()
    {
        if (currentWeaponIndex < weaponLevels.Count)
        {
            return weaponLevels[currentWeaponIndex]?.GetCurrentWeaponStats();
        }
        else if (currentWeaponIndex < weaponLevels.Count + rangedWeaponLevels.Count)
        {
            int rangedIndex = currentWeaponIndex - weaponLevels.Count;
            return rangedWeaponLevels[rangedIndex];
        }
        return null;
    }
    public void SetWeaponType(string weaponType)
    {
        if (currentWeaponIndex < weaponLevels.Count)
        {
            weaponLevels[currentWeaponIndex]?.SetWeaponType(weaponType);
        }
        else if (currentWeaponIndex < weaponLevels.Count + rangedWeaponLevels.Count)
        {
            int rangedIndex = currentWeaponIndex - weaponLevels.Count;
            // Cập nhật loại vũ khí tầm xa nếu cần
            Debug.Log($"Set weapon type for ranged weapon: {weaponType}");
        }
    }

    public void SetWeaponLevel(int level)
    {
        if (currentWeaponIndex < weaponLevels.Count)
        {
            weaponLevels[currentWeaponIndex]?.SetWeaponLevel(level);
        }
        else if (currentWeaponIndex < weaponLevels.Count + rangedWeaponLevels.Count)
        {
            int rangedIndex = currentWeaponIndex - weaponLevels.Count;
            // Cập nhật level cho vũ khí tầm xa nếu cần
            Debug.Log($"Set level for ranged weapon: {level}");
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponLevelChoose.Count + rangedWeaponLevels.Count)
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

        // Xử lý vũ khí cận chiến
        if (weaponIndex < weaponLevelChoose.Count && weaponLevelChoose[weaponIndex] != null)
        {
            weaponLevelChoose[weaponIndex].SetActive(true);
        }
        else if (weaponIndex >= weaponLevelChoose.Count)
        {
            // Xử lý vũ khí tầm xa
            Debug.Log("Switched to ranged weapon");
        }
    }

}
