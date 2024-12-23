
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
            AudioManager.Instance.PlayVFX("PickupItem");
            WeaponManager weaponManager = WeaponManager.Instance;

            if (weaponManager != null)
            {
                weaponManager.PickUpWeapon(weaponIndex);
                weaponManager.GetWeaponLevelManager()?.UnlockWeapon(weaponIndex, weaponType); // Mở khóa weapon
                Destroy(gameObject); // Xóa vũ khí khỏi màn chơi
            }
            else
            {
                Debug.LogError("WeaponManager chưa được gán hoặc không tìm thấy!");
            }
        }
    }

}

