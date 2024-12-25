// WeaponLevelManager.cs
using UnityEngine;

public class WeaponLevelManager : MonoBehaviour
{
    [Header("Weapon Arrays")]
    public WeaponSwordStats[] swordWeapons;
    public WeaponBowStats[] bowWeapons;
    public WeaponMagicStats[] magicWeapons;

    private WeaponStats currentWeapon; // Vũ khí đang được sử dụng
    [SerializeField] private PlayerAction playerAction;

    private bool[] swordUnlocked; // Trạng thái mở khóa của kiếm
    private bool[] bowUnlocked; // Trạng thái mở khóa của cung
    private bool[] magicUnlocked; // Trạng thái mở khóa của phép thuật

    private void Start()
    {
        // Khởi tạo trạng thái mở khóa của tất cả vũ khí là false
        swordUnlocked = new bool[swordWeapons.Length];
        bowUnlocked = new bool[bowWeapons.Length];
        magicUnlocked = new bool[magicWeapons.Length];

        Debug.Log($"Sword Weapons Length: {swordWeapons.Length}");

        if (swordWeapons.Length > 0)
            swordUnlocked[0] = true; // Mở khóa vũ khí đầu tiên
    }

    /// <summary>
    /// Mở khóa vũ khí theo chỉ số và loại
    /// </summary>
    public void UnlockWeapon(int index, WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                if (index < swordUnlocked.Length)
                {
                    swordUnlocked[index] = true;
                    Debug.Log($"Sword {index} unlocked! Current Unlock Status: {GetUnlockedLevels(swordUnlocked)}");
                }
                else
                {
                    Debug.LogWarning($"Invalid index {index} for Sword.");
                }
                break;

            case WeaponType.Bow:
                if (index < bowUnlocked.Length)
                {
                    bowUnlocked[index] = true;
                    Debug.Log($"Bow {index} unlocked! Current Unlock Status: {GetUnlockedLevels(bowUnlocked)}");
                }
                else
                {
                    Debug.LogWarning($"Invalid index {index} for Bow.");
                }
                break;

            case WeaponType.Magic:
                if (index < magicUnlocked.Length)
                {
                    magicUnlocked[index] = true;
                    Debug.Log($"Magic {index} unlocked! Current Unlock Status: {GetUnlockedLevels(magicUnlocked)}");
                }
                else
                {
                    Debug.LogWarning($"Invalid index {index} for Magic.");
                }
                break;

            default:
                Debug.LogWarning("Invalid weapon type.");
                break;
        }
    }

    /// <summary>
    /// Chọn vũ khí
    /// </summary>
    public void SwitchWeapon(int index, WeaponType weaponType)
    {
        Debug.Log($"Attempting to switch to {weaponType} at level {index}");
        switch (weaponType)
        {
            case WeaponType.Sword:
                if (index < swordWeapons.Length && swordUnlocked[index])
                {
                    currentWeapon = swordWeapons[index];
                    playerAction?.UpdateWeaponPrefabs(currentWeapon);
                    Debug.Log($"Switched to sword: {currentWeapon.weaponName}");
                }
                else
                {
                    Debug.LogWarning($"Sword {index} is locked or invalid.");
                }
                break;

            case WeaponType.Bow:
                if (index < bowWeapons.Length && bowUnlocked[index])
                {
                    currentWeapon = bowWeapons[index];
                    playerAction?.UpdateWeaponPrefabs(currentWeapon);
                    Debug.Log($"Switched to bow: {currentWeapon.weaponName}");
                }
                else
                {
                    Debug.LogWarning($"Bow {index} is locked or invalid.");
                }
                break;

            case WeaponType.Magic:
                if (index < magicWeapons.Length && magicUnlocked[index])
                {
                    currentWeapon = magicWeapons[index];
                    playerAction?.UpdateWeaponPrefabs(currentWeapon);
                    Debug.Log($"Switched to magic: {currentWeapon.weaponName}");
                }
                else
                {
                    Debug.LogWarning($"Magic {index} is locked or invalid.");
                }
                break;

            default:
                Debug.LogWarning("Invalid weapon type.");
                break;
        }
    }

    private string GetUnlockedLevels(bool[] unlocked)
    {
        string levels = "";
        for (int i = 0; i < unlocked.Length; i++)
        {
            if (unlocked[i])
                levels += $"{i} ";
        }
        return levels.Trim();
    }

    /// <summary>
    /// Trả về thông tin vũ khí đang sử dụng
    /// </summary>
    public WeaponStats GetCurrentWeaponStats()
    {
        return currentWeapon;
    }
}
