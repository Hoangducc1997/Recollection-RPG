using UnityEngine;

// Main Weapon Manager
public class WeaponLevelManager : MonoBehaviour
{
    [Header("Weapon Arrays")]
    public WeaponSwordStats[] swordWeapons;
    public WeaponBowStats[] bowWeapons;
    public WeaponMagicStats[] magicWeapons;
    public WeaponShieldStats[] Shieldweapons;

    private WeaponStats currentWeapon; // Vũ khí đang được sử dụng
    [SerializeField] private PlayerAction playerAction;

    private bool[] swordUnlocked; // Trạng thái mở khóa của kiếm
    private bool[] bowUnlocked; // Trạng thái mở khóa của cung
    private bool[] magicUnlocked; // Trạng thái mở khóa của phép thuật

    private void Start()
    {
        // Khởi tạo trạng thái mở khóa của tất cả vũ khí là false
        swordUnlocked = new bool[swordWeapons.Length];
        bowUnlocked = new bool[bowWeapons.Length];
        magicUnlocked = new bool[magicWeapons.Length];

        // Giả định Level 1 đã được mở khóa mặc định
        if (swordWeapons.Length > 0)
            swordUnlocked[0] = true; // Mở khóa vũ khí đầu tiên
    }

    /// <summary>
    /// Mở khóa vũ khí theo chỉ số và loại
    /// </summary>
    public void UnlockWeapon(int index, WeaponType weaponType)
    {
        switch (weaponType)
        {
            case WeaponType.Sword:
                if (index < swordUnlocked.Length)
                {
                    swordUnlocked[index] = true;
                    Debug.Log($"Sword {index} unlocked!");
                }
                break;

            case WeaponType.Bow:
                if (index < bowUnlocked.Length)
                {
                    bowUnlocked[index] = true;
                    Debug.Log($"Bow {index} unlocked!");
                }
                break;

            case WeaponType.Magic:
                if (index < magicUnlocked.Length)
                {
                    magicUnlocked[index] = true;
                    Debug.Log($"Magic {index} unlocked!");
                }
                break;

            default:
                Debug.LogWarning("Invalid weapon type or index.");
                break;
        }
    }

    /// <summary>
    /// Chọn vũ khí
    /// </summary>
    public void SwitchWeapon(int index, WeaponType weaponType)
    {
        Debug.Log($"Attempting to switch to {weaponType} at level {index}");
        switch (weaponType)
        {
            case WeaponType.Sword:
                if (index < swordWeapons.Length && swordUnlocked[index])
                {
                    currentWeapon = swordWeapons[index];
                    Debug.Log($"Switched to sword: {currentWeapon.weaponName}");
                }
                else
                {
                    Debug.LogWarning($"Sword {index} is locked or invalid.");
                }
                break;

            case WeaponType.Bow:
                if (index < bowWeapons.Length && bowUnlocked[index])
                {
                    currentWeapon = bowWeapons[index]; // Gán vũ khí mới trước
                    playerAction?.UpdateWeaponPrefabs(currentWeapon); // Sau đó cập nhật prefab
                    Debug.Log($"Switched to bow: {currentWeapon.weaponName}");
                }
                else
                {
                    Debug.LogWarning("Bow is locked or invalid index.");
                }
                break;

            case WeaponType.Magic:
                if (index < magicWeapons.Length && magicUnlocked[index])
                {
                    currentWeapon = magicWeapons[index];
                    Debug.Log($"Switched to magic: {currentWeapon.weaponName}");
                }
                else
                {
                    Debug.LogWarning("Magic is locked or invalid index.");
                }
                break;

            default:
                Debug.LogWarning("Invalid weapon type.");
                break;
        }
    }


    /// <summary>
    /// Trả về thông tin vũ khí đang sử dụng
    /// </summary>
    public WeaponStats GetCurrentWeaponStats()
    {
        return currentWeapon;
    }
}
