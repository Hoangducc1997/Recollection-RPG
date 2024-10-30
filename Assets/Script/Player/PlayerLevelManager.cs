using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    [SerializeField] private Player player; // Tham chiếu tới script GamePlay
    [SerializeField] private Weapon[] levelUpWeapons; // Mỗi cấp độ có một vũ khí mới

    private int currentLevel = 1; // Mặc định là cấp độ 1

    // Khi qua màn mới, nhận vũ khí mới và tăng cấp độ
    public void LevelUp()
    {
        currentLevel++;

        if (currentLevel - 1 < levelUpWeapons.Length)
        {
            Weapon newWeapon = levelUpWeapons[currentLevel - 1];     

            Debug.Log("GamePlay đã lên cấp " + currentLevel + " và nhận vũ khí mới: " + newWeapon.weaponName);
        }
        else
        {
            Debug.Log("Không còn vũ khí mới cho cấp độ này.");
        }
    }
}
