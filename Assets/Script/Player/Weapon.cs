using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats weaponStats;
    void Start()
    {
        // Đảm bảo weaponStats được gán đúng
        if (weaponStats != null)
        {
            Debug.Log("Weapon: " + weaponStats.weaponName + " Animation Index: " + weaponStats.animationIndex);
        }
    }
    public int GetAnimationIndex() => weaponStats != null ? weaponStats.animationIndex : 0;

    public int GetDamage() => weaponStats != null ? weaponStats.damage : 0;
    public float GetRange() => weaponStats != null ? weaponStats.rangeAtk : 0f;
    public float GetCooldownTime() => weaponStats != null ? weaponStats.cooldownTime : 0f;
    public float GetAttackDuration() => weaponStats != null ? weaponStats.attackDuration : 0f;
}
