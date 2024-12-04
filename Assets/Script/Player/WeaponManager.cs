using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponChoose = new List<GameObject>();
    private List<bool> weaponOwned = new List<bool>();
    private int currentWeaponIndex;

    private static WeaponManager instance;
    public static WeaponManager Instance => instance ??= FindObjectOfType<WeaponManager>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        weaponOwned = new List<bool>(new bool[weaponChoose.Count]);
        if (weaponChoose.Count > 0)
        {
            weaponOwned[0] = true;
            SwitchWeapon(0);
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count || !weaponOwned[weaponIndex])
        {
            Debug.LogWarning("weaponIndex không hợp lệ hoặc chưa sở hữu!");
            return;
        }

        foreach (var weapon in weaponChoose)
        {
            weapon?.SetActive(false);
        }

        weaponChoose[weaponIndex]?.SetActive(true);
        currentWeaponIndex = weaponIndex;
    }

    public void PickUpWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count)
        {
            Debug.LogError("weaponIndex không hợp lệ!");
            return;
        }

        weaponOwned[weaponIndex] = true;
        SwitchWeapon(weaponIndex);
    }
}
