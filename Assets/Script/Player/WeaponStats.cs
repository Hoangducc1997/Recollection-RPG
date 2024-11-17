using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    public string weaponName;        // Tên vũ khí
    public int animationIndex;       // Số thứ tự của animation
    public int damage;               // Sát thương của vũ khí
    public float rangeAtk;           // Vùng hoạt động của vũ khí để tấn công 
    public float cooldownTime;       // Thời gian hồi chiêu
    public float attackDuration;     // Thời gian Anim tấn công kéo dài
}
