using UnityEngine;

[CreateAssetMenu(fileName = "NewBowWeapon", menuName = "Weapons/Bow")]
public class WeaponBowStats : WeaponStats
{
    [SerializeField] private GameObject arrowPrefab;  // Prefab của mũi tên
    [SerializeField] private float arrowSpeed;        // Tốc độ mũi tên

    public GameObject ArrowPrefab => arrowPrefab;     // Getter để lấy prefab
    public float ArrowSpeed => arrowSpeed;            // Getter để lấy arrowSpeed

    public override void Attack()
    {
        base.Attack();
        // Có thể thêm logic tấn công riêng cho cung ở đây
    }
}
