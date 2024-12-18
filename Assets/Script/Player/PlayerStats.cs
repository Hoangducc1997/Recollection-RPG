using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Game/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float baseMoveSpeed = 1.5f; // Tốc độ cơ bản
    public List<LevelSpeedData> levelSpeedData; // Danh sách tốc độ tăng theo cấp độ

    private float currentMoveSpeed;
    private int currentLevel = 1; // Cấp độ bắt đầu

    private void OnEnable()
    {
        if (levelSpeedData != null && levelSpeedData.Count > 0)
        {
            currentLevel = 0; // Khởi tạo cấp độ đầu tiên
            currentMoveSpeed = levelSpeedData[currentLevel].speedBoost; // Gán tốc độ của cấp độ đầu tiên
        }
        else
        {
            currentMoveSpeed = baseMoveSpeed;
            Debug.LogWarning("LevelSpeedData is empty or not assigned!");
        }
    }



    public float GetMoveSpeed()
    {
        return currentMoveSpeed;
    }

    public int GetCurrentLevel()
    {
        return currentLevel; // Trả về cấp độ hiện tại
    }

    public void IncreaseSpeed()
    {
        Debug.Log($"Attempting to increase speed. CurrentLevel: {currentLevel}, MaxLevel: {levelSpeedData.Count - 1}");

        if (currentLevel < levelSpeedData.Count - 1)
        {
            currentLevel++;
            currentMoveSpeed = levelSpeedData[currentLevel].speedBoost;
            Debug.Log($"Speed increased to {currentMoveSpeed} at Level {currentLevel}");
        }
        else
        {
            Debug.LogWarning("Max speed level reached!");
        }
    }



}
