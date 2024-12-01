using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> weaponChoose = new List<GameObject>(); // Danh sách các vũ khí
    private List<bool> weaponOwned = new List<bool>(); // Trạng thái sở hữu vũ khí

    private int currentWeaponIndex = 0; // Vũ khí hiện tại của Player
    private static WeaponManager instance;
    public static WeaponManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WeaponManager>();
                if (instance == null)
                {
                    Debug.LogError("WeaponManager không tìm thấy trong scene!");
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ WeaponManager khi chuyển scene
        }
        else
        {
            Destroy(gameObject); // Nếu đã có instance, hủy đối tượng này
        }
    }


    private void Start()
    {
        // Khởi tạo danh sách trạng thái sở hữu
        weaponOwned = new List<bool>(new bool[weaponChoose.Count]);

        // Mặc định Player sở hữu vũ khí đầu tiên nếu danh sách không rỗng
        if (weaponChoose.Count > 0)
        {
            weaponOwned[0] = true; // Sở hữu vũ khí đầu tiên
            SwitchWeapon(0);      // Trang bị vũ khí đầu tiên
        }
        else
        {
            Debug.LogWarning("Danh sách weaponChoose trống!");
        }
    }

    // Chuyển đổi sang vũ khí khác
    public void SwitchWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count)
        {
            Debug.LogError("weaponIndex không hợp lệ!");
            return;
        }

        if (!weaponOwned[weaponIndex])
        {
            Debug.LogWarning($"Vũ khí {weaponIndex} chưa được nhặt!");
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
        {
            weaponChoose[weaponIndex].SetActive(true);
            currentWeaponIndex = weaponIndex;
        }
    }

    // Nhặt vũ khí
    public void PickUpWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponChoose.Count)
        {
            Debug.LogError("weaponIndex không hợp lệ!");
            return;
        }

        // Đánh dấu là đã sở hữu
        weaponOwned[weaponIndex] = true;
        Debug.Log($"Player đã nhặt vũ khí {weaponIndex}!");

        // Tự động chuyển đổi sang vũ khí vừa nhặt
        SwitchWeapon(weaponIndex);
    }
}
