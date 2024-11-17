using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement player; // Tham chiếu tới script GamePlay
    [SerializeField] private UnityEngine.GameObject[] levelUpWeapons; // Mỗi cấp độ có một vũ khí mới

    private int currentLevel = 1; // Mặc định là cấp độ 1

    // Khi qua màn mới, nhận vũ khí mới và tăng cấp độ
    public void LevelUp()
    {
        currentLevel++;

        if (currentLevel - 1 < levelUpWeapons.Length)
        {
            UnityEngine.GameObject newWeapon = levelUpWeapons[currentLevel - 1];     

           
        }
        else
        {
            Debug.Log("Không còn vũ khí mới cho cấp độ này.");
        }
    }
}
