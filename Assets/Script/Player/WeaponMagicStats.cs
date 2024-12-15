using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicWeapon", menuName = "Weapons/Magic")]
public class WeaponMagicStats : WeaponStats
{
    [SerializeField] private GameObject magicPrefab;  // Prefab của phép thuật
    [SerializeField] private float magicSpeed;        // Tốc độ phép thuật

    public GameObject MagicPrefab => magicPrefab;     // Getter để lấy prefab
    public float MagicSpeed => magicSpeed;            // Getter để lấy magicSpeed

    public override void Attack()
    {
        base.Attack();
        // Có thể thêm logic tấn công riêng cho phép thuật ở đây
    }
}
