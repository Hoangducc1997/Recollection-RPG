using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponMeleeDatabase", menuName = "Weapon/WeaponMeleeDatabase")]
public class WeaponMeleeDatabase : ScriptableObject
{
    public List<WeaponMeleeStats> weapons; // Danh sách tất cả các vũ khí và cấp độ
}
