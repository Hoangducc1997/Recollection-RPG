using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WeaponRangedDatabase", menuName = "Weapon/WeaponRangedDatabase")]
public class WeaponRangedDatabase : ScriptableObject
{
    public List<WeaponRangedStats> weapons; // Danh sách tất cả các vũ khí và cấp độ
}
