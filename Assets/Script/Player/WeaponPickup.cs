using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private int weaponIndex; // Index của vũ khí
    [SerializeField] private string playerTag = "Player"; // Tag của Player

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag(playerTag))
    //    {
    //        WeaponManager weaponManager = WeaponManager.Instance; // Truy cập WeaponManager qua Instance
    //        if (weaponManager != null)
    //        {
    //            Debug.Log($"Player nhặt được vũ khí {weaponIndex}");
    //            weaponManager.PickUpWeapon(weaponIndex); // Gọi hàm PickUpWeapon
    //            Destroy(gameObject); // Xóa vũ khí khỏi màn chơi
    //        }
    //        else
    //        {
    //            Debug.LogError("WeaponManager chưa được gán hoặc không tìm thấy!");
    //        }
    //    }
    //}
}
