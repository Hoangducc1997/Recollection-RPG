using UnityEngine;

[CreateAssetMenu(fileName = "NewShieldWeapon", menuName = "Weapons/Shield")]

public class WeaponShieldStats : MonoBehaviour
{
    public string weaponName;       // Tên loại vũ khí
    public int level;               // Cấp độ vũ khí
    public float rangeDef;          // Tầm phòng thủ
    public float cooldownTime;      // Thời gian hồi chiêu
    public int animationIndex;      // Chỉ số animation tương ứng
}
