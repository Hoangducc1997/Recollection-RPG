
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private int weaponIndex; // Index của vũ khí
    [SerializeField] private WeaponType weaponType; // Loại vũ khí (Kiếm, Cung, Phép)
    [SerializeField] private string playerTag = "Player"; // Tag của Player

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            WeaponManager weaponManager = WeaponManager.Instance; // Truy cập WeaponManager qua Singleton
            if (weaponManager != null)
            {
                WeaponLevelManager weaponLevelManager = weaponManager.GetWeaponLevelManager(); // Lấy WeaponLevelManager
                if (weaponLevelManager != null)
                {
                    Debug.Log($"Player nhặt được vũ khí {weaponType} tại index {weaponIndex}");
                    weaponLevelManager.UnlockWeapon(weaponIndex, weaponType); // Mở khóa vũ khí
                    Destroy(gameObject); // Xóa vũ khí khỏi màn chơi
                }
                else
                {
                    Debug.LogError("WeaponLevelManager chưa được gán hoặc không tìm thấy!");
                }
            }
            else
            {
                Debug.LogError("WeaponManager chưa được gán hoặc không tìm thấy!");
            }
        }
    }
}

