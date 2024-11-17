using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponChoose = new List<GameObject>(); // Danh sách các vũ khí
    private int currentWeaponIndex = 0; // Vũ khí hiện tại

    public void AddWeapon(GameObject newWeapon)
    {
        if (!weaponChoose.Contains(newWeapon))
        {
            weaponChoose.Add(newWeapon);
            newWeapon.SetActive(false); // Vũ khí mới được thêm vào nhưng tắt đi để không kích hoạt ngay
            Debug.Log($"Added weapon: {newWeapon.name}");
        }
    }

    public void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weaponChoose.Count)
        {
            currentWeaponIndex = index;

            // Kích hoạt vũ khí được chọn, vô hiệu hóa các vũ khí khác
            for (int i = 0; i < weaponChoose.Count; i++)
            {
                weaponChoose[i].SetActive(i == currentWeaponIndex);
            }

            Debug.Log($"Switched to weapon: {weaponChoose[currentWeaponIndex].name}");
        }
        else
        {
            Debug.LogWarning("Invalid weapon index: " + index);
        }
    }

    public WeaponStats GetCurrentWeapon()
    {
        if (weaponChoose.Count > 0 && currentWeaponIndex >= 0 && currentWeaponIndex < weaponChoose.Count)
        {
            // Lấy WeaponStats từ vũ khí hiện tại
            Weapon weapon = weaponChoose[currentWeaponIndex].GetComponent<Weapon>();
            if (weapon != null)
            {
                return weapon.weaponStats;
            }
        }
        Debug.LogWarning("No valid current weapon found!");
        return null;
    }
}
