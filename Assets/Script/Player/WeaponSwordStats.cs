using UnityEngine;

[CreateAssetMenu(fileName = "NewSwordWeapon", menuName = "Weapons/Sword")]
public class WeaponSwordStats : WeaponStats
{
    //public float swingRange; // Tầm đánh đặc biệt dành riêng cho vũ khí cận chiến

    public override void Attack()
    {
        base.Attack();
       // Debug.Log($"Swing range is {swingRange}");
    }
}
