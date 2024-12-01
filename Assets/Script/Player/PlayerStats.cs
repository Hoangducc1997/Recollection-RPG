using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ScriptableObject
{
    public float baseMoveSpeed = 2.0f; // Tốc độ cơ bản
    public List<LevelSpeedData> levelSpeedData; // Danh sách tốc độ tăng theo cấp độ

    private float currentMoveSpeed;
    private int currentLevel = 1; // Cấp độ bắt đầu

    private void OnEnable()
    {
        currentMoveSpeed = baseMoveSpeed; // Gán tốc độ cơ bản khi bắt đầu
    }

    public float GetMoveSpeed()
    {
        return currentMoveSpeed;
    }

    public int GetCurrentLevel()
    {
        return currentLevel; // Trả về cấp độ hiện tại
    }

    public void IncreaseSpeedByLevel(int level)
    {
        Debug.Log($"IncreaseSpeedByLevel called with level: {level}");

        // Tìm giá trị tốc độ cho cấp độ hiện tại
        float newSpeed = baseMoveSpeed; // Tốc độ mặc định là baseMoveSpeed

        foreach (var data in levelSpeedData)
        {
            if (data.level == level)
            {
                newSpeed = data.speedBoost; // Chỉ định tốc độ mới cho cấp độ
                break;
            }
        }

        currentMoveSpeed = newSpeed; // Chỉ định tốc độ mới cho cấp độ
        currentLevel = level; // Cập nhật cấp độ hiện tại
        Debug.Log($"Move Speed updated: Base = {baseMoveSpeed}, New Speed = {currentMoveSpeed}");
    }
}
