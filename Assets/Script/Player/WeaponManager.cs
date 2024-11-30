using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> weaponChoose = new List<GameObject>();

    private int currentWeaponIndex = 0;

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count)
        {
            Debug.LogError("weaponIndex không hợp lệ!");
            return;
        }

        // Tắt tất cả vũ khí
        foreach (var weapon in weaponChoose)
        {
            if (weapon != null)
                weapon.SetActive(false);
        }

        // Bật vũ khí được chọn
        if (weaponChoose[weaponIndex] != null)
            weaponChoose[weaponIndex].SetActive(true);
    }



}
