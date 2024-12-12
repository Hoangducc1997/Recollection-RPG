using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicWeapon", menuName = "Weapons/Magic")]
public class WeaponMagicStats : WeaponStats
{
    // public float heathCost;    // Lượng máu tiêu hao
    [SerializeField] private GameObject magicPrefab;  // Thêm vào đây để gán prefab trong Inspector

    public GameObject MagicPrefab => magicPrefab; // Getter để truy cập arrowPrefab từ bên ngoài
    public override void Attack()
    {
        base.Attack();
       // Debug.Log($"Swing range is {swingRange}");
    }
}
