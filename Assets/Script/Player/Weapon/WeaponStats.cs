using UnityEngine;

public abstract class WeaponStats : ScriptableObject
{
    public string weaponName;       // Tên loại vũ khí
    public int level;               // Cấp độ vũ khí
    public int damage;              // Sát thương
    public float rangeAtk;          // Tầm tấn công
    public float cooldownTime;      // Thời gian hồi chiêu
    public float attackDuration;    // Thời gian thực hiện tấn công
    public int animationIndex;      // Chỉ số animation tương ứng

    // Hàm để áp dụng logic chung khi vũ khí tấn công
    public virtual void Attack()
    {
        Debug.Log($"{weaponName} attacking with damage {damage}.");
    }
}
