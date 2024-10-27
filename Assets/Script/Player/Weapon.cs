using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;        // Tên vũ khí
    public AnimationClip attackClip; // Animation cho vũ khí
    public int damage;               // Sát thương của vũ khí
    public float rangeAtk;           // Vùng hoạt động của vũ khí để tấn công 
    public float cooldownTime;       // Thời gian để vũ khí có thể sử dụng lại sau một đòn tấn công.
    public float attackDuration;     // Thời gian Anim tấn công kéo dài và thời gian mà người chơi được coi là đang trong trạng thái tấn công.

}
