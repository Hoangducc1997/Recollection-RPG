using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player; // Tham chiếu tới script GamePlay
    [SerializeField] private GameObject[] levelUpWeapons; // Mỗi cấp độ có một vũ khí mới

    private int currentLevel = 1; // Mặc định là cấp độ 1

    public void LevelUp()
    {
        currentLevel++;

        if (currentLevel - 1 < levelUpWeapons.Length)
        {
            GameObject newWeapon = levelUpWeapons[currentLevel - 1];
            Debug.Log("Nhận vũ khí mới: " + newWeapon.name);
            // Logic để gắn vũ khí mới cho nhân vật, nếu cần
        }
        else
        {
            Debug.Log("Không còn vũ khí mới cho cấp độ này.");
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
