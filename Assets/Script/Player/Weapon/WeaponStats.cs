using UnityEngine;

public abstract class WeaponStats : ScriptableObject
{
    public string weaponName;       // Tên loại vũ khí
    public int level;               // Cấp độ vũ khí
    public int damage;              // Sát thương
    public float rangeAtk;          // Tầm tấn công
    public float cooldownTime;      // kiểm tra thời gian giữa các lần tấn công.
    public float attackDuration;    // để xác định thời gian thực hiện animation và hiệu ứng tấn công
    public int animationIndex;      // Chỉ số animation tương ứng

    // Hàm để áp dụng logic chung khi vũ khí tấn công
    public virtual void Attack()
    {
        Debug.Log($"{weaponName} attacking with damage {damage}.");
    }
}
