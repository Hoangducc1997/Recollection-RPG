using UnityEngine;

public class WeaponLevelManager : MonoBehaviour
{
    [SerializeField] private WeaponSwordStats[] swordWeapons;
    [SerializeField] private WeaponBowStats[] bowWeapons;
    [SerializeField] private WeaponMagicStats[] magicWeapons;

    private WeaponStats currentWeapon;

    public void SwitchWeapon(int index, WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                if (index < swordWeapons.Length)
                    currentWeapon = swordWeapons[index];
                break;

            case WeaponType.Bow:
                if (index < bowWeapons.Length)
                    currentWeapon = bowWeapons[index];
                break;

            case WeaponType.Magic:
                if (index < magicWeapons.Length)
                    currentWeapon = magicWeapons[index];
                break;

            default:
                Debug.LogWarning("Invalid weapon type or index.");
                return;
        }

        Debug.Log($"Switched to {weaponType} weapon: {currentWeapon.weaponName}");
    }

    public WeaponStats GetCurrentWeaponStats()
    {
        return currentWeapon;
    }

    public void PerformAttack()
    {
        if (currentWeapon != null)
            currentWeapon.Attack();
        else
            Debug.LogWarning("No weapon is currently selected.");
    }
}
