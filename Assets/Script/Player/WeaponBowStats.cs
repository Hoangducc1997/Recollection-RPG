using UnityEngine;

[CreateAssetMenu(fileName = "NewBowWeapon", menuName = "Weapons/Bow")]
public class WeaponBowStats : WeaponStats
{
    // public float projectileSpeed; // Tốc độ của đạn

    public override void Attack()
    {
        base.Attack();
       // Debug.Log($"Projectile speed is {projectileSpeed}");
    }
}
