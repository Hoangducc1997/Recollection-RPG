using System.Collections.Generic;
using UnityEngine;

public class WeaponLevelManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponLevels; // Danh sách các cấp độ vũ khí
    private int currentLevel = 0; // Cấp độ hiện tại (bắt đầu từ 0)

    public void SetLevel(int level)
    {
        if (level < 0 || level >= weaponLevels.Count)
        {
            Debug.LogWarning("Invalid weapon level: " + level);
            return;
        }

        // Tắt tất cả các vũ khí
        foreach (var weapon in weaponLevels)
        {
            if (weapon != null)
                weapon.SetActive(false);
        }

        // Bật vũ khí ở cấp độ được chọn
        if (weaponLevels[level] != null)
        {
            weaponLevels[level].SetActive(true);
            currentLevel = level;
        }
    }

    public GameObject GetCurrentWeapon()
    {
        if (currentLevel >= 0 && currentLevel < weaponLevels.Count)
        {
            return weaponLevels[currentLevel];
        }
        return null;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
