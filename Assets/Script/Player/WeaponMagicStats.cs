using UnityEngine;

[CreateAssetMenu(fileName = "NewMagicWeapon", menuName = "Weapons/Magic")]
public class WeaponMagicStats : WeaponStats
{
    // public float heathCost;    // Lượng máu tiêu hao

    public override void Attack()
    {
        base.Attack();
       // Debug.Log($"Swing range is {swingRange}");
    }
}
