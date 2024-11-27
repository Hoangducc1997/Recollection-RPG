using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponChoose = new List<GameObject>();

    private int currentWeaponIndex = 0;

    public void SwitchWeapon(int weaponIndex)
    {
        // Kiểm tra index hợp lệ
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count) return;

        // Tắt tất cả các GameObject trong danh sách
        foreach (var weapon in weaponChoose)
        {
            if (weapon != null)
                weapon.SetActive(false);
        }

        // Bật GameObject tương ứng với weaponIndex
        if (weaponChoose[weaponIndex] != null)
        {
            weaponChoose[weaponIndex].SetActive(true);
            currentWeaponIndex = weaponIndex;
        }
    }
}
