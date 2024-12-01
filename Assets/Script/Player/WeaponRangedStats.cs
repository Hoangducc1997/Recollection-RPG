using UnityEngine;

[System.Serializable]
public class WeaponRangedStats
{
    public string weaponName;       // Tên loại vũ khí
    public int level;               // Cấp độ vũ khí
    public int damage;              // Sát thương
    public float rangeAtk;          // Tầm tấn công
    public float cooldownTime;      // Thời gian hồi chiêu
    public float attackDuration;    // Thời gian thực hiện tấn công
    public int animationIndex;      // Chỉ số animation tương ứng
}
