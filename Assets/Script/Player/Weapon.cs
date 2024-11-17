using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponStats weaponStats; // Dữ liệu của vũ khí được gán từ Inspector

    // Trả về chỉ số sát thương của vũ khí
    public int GetDamage()
    {
        return weaponStats != null ? weaponStats.damage : 0;
    }

    // Trả về phạm vi tấn công của vũ khí
    public float GetRange()
    {
        return weaponStats != null ? weaponStats.rangeAtk : 0f;
    }

    // Trả về thời gian hồi chiêu của vũ khí
    public float GetCooldownTime()
    {
        return weaponStats != null ? weaponStats.cooldownTime : 0f;
    }

    // Trả về thời gian kéo dài của hoạt ảnh tấn công
    public float GetAttackDuration()
    {
        return weaponStats != null ? weaponStats.attackDuration : 0f;
    }

    // Trả về tên của vũ khí
    public string GetWeaponName()
    {
        return weaponStats != null ? weaponStats.weaponName : "No Weapon";
    }
}
