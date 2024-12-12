using UnityEngine;

[CreateAssetMenu(fileName = "NewBowWeapon", menuName = "Weapons/Bow")]
public class WeaponBowStats : WeaponStats
{
    [SerializeField] private GameObject arrowPrefab;  // Thêm vào đây để gán prefab trong Inspector

    public GameObject ArrowPrefab => arrowPrefab; // Getter để truy cập arrowPrefab từ bên ngoài

    public override void Attack()
    {
        base.Attack();
        // Nếu cần thêm logic đặc biệt cho việc tấn công bằng cung, có thể làm ở đây
    }
}
