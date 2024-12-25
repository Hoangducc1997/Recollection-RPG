using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance { get; private set; }

    [SerializeField] private WeaponLevelManager weaponLevelManager;
    private HashSet<int> pickedUpWeapons = new HashSet<int>(); // Lưu các index của vũ khí đã nhặt

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public WeaponLevelManager GetWeaponLevelManager()
    {
        return weaponLevelManager;
    }

    public void PickUpWeapon(int index)
    {
        if (!pickedUpWeapons.Contains(index))
        {
            pickedUpWeapons.Add(index);
            Debug.Log($"Weapon {index} has been picked up and unlocked.");

            // Hiển thị nút UI liên quan
            WeaponChangeManager.Instance?.UnlockWeapon(index);
        }
        else
        {
            Debug.Log($"Weapon {index} was already unlocked.");
        }
    }

    public void SwitchWeapon(int index, WeaponType weaponType)
    {
        if (pickedUpWeapons.Contains(index))
        {
            weaponLevelManager.SwitchWeapon(index, weaponType);
        }
        else
        {
            Debug.LogWarning($"Weapon {index} has not been picked up yet.");
        }
    }

    public bool IsWeaponPickedUp(int index)
    {
        return pickedUpWeapons.Contains(index);
    }
}
