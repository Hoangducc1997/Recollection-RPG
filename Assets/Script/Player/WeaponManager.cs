using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> ownedWeapons = new List<Weapon>();
    private int currentWeaponIndex = 0;

    // Lấy vũ khí hiện tại
    public Weapon GetCurrentWeapon()
    {
        if (ownedWeapons.Count == 0) return null; // Trả về null nếu không có vũ khí
        return ownedWeapons[currentWeaponIndex];
    }

    // Thêm vũ khí mới
    public void AddWeapon(Weapon newWeapon)
    {
        if (!ownedWeapons.Contains(newWeapon))
        {
            ownedWeapons.Add(newWeapon);
            Debug.Log("Added weapon: " + newWeapon.weaponName);
        }
    }

    // Chuyển đổi vũ khí
    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < ownedWeapons.Count)
        {
            currentWeaponIndex = index;
            Debug.Log("Switched to weapon: " + GetCurrentWeapon().weaponName);
        }
    }

    // Chọn vũ khí ngẫu nhiên
    public void SwitchToRandomWeapon()
    {
        if (ownedWeapons.Count > 0)
        {
            currentWeaponIndex = Random.Range(0, ownedWeapons.Count);
            Debug.Log("Switched to random weapon: " + GetCurrentWeapon().weaponName);
        }
    }
}
