using UnityEngine;

public class WeaponLevelManager : MonoBehaviour
{
    [SerializeField] private WeaponSwordStats[] swordWeapons;
    [SerializeField] private WeaponBowStats[] bowWeapons;
    [SerializeField] private WeaponMagicStats[] magicWeapons;

    private WeaponStats currentWeapon;

    private bool[] swordUnlocked; // Trạng thái mở khóa của kiếm
    private bool[] bowUnlocked; // Trạng thái mở khóa của cung
    private bool[] magicUnlocked; // Trạng thái mở khóa của phép thuật

    private void Start()
    {
        // Khởi tạo trạng thái mở khóa của tất cả vũ khí là false
        swordUnlocked = new bool[swordWeapons.Length];
        bowUnlocked = new bool[bowWeapons.Length];
        magicUnlocked = new bool[magicWeapons.Length];
    }

    public void UnlockWeapon(int index, WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                if (index < swordUnlocked.Length)
                    swordUnlocked[index] = true; // Mở khóa kiếm
                break;

            case WeaponType.Bow:
                if (index < bowUnlocked.Length)
                    bowUnlocked[index] = true; // Mở khóa cung
                break;

            case WeaponType.Magic:
                if (index < magicUnlocked.Length)
                    magicUnlocked[index] = true; // Mở khóa phép
                break;

            default:
                Debug.LogWarning("Invalid weapon type or index.");
                break;
        }
    }

    public void SwitchWeapon(int index, WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                if (index < swordWeapons.Length && swordUnlocked[index])
                    currentWeapon = swordWeapons[index];
                else
                    Debug.LogWarning("This sword weapon is locked!");
                break;

            case WeaponType.Bow:
                if (index < bowWeapons.Length && bowUnlocked[index])
                    currentWeapon = bowWeapons[index];
                else
                    Debug.LogWarning("This bow weapon is locked!");
                break;

            case WeaponType.Magic:
                if (index < magicWeapons.Length && magicUnlocked[index])
                    currentWeapon = magicWeapons[index];
                else
                    Debug.LogWarning("This magic weapon is locked!");
                break;

            default:
                Debug.LogWarning("Invalid weapon type or index.");
                break;
        }

        if (currentWeapon != null)
            Debug.Log($"Switched to {weaponType} weapon: {currentWeapon.weaponName}");
    }

    public WeaponStats GetCurrentWeaponStats()
    {
        return currentWeapon;
    }
}
