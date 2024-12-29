using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapBoss1 : SpawnManager
{
    // Override hàm EnemyDefeated để tùy chỉnh logic nếu cần
    protected override void EnemyDefeated(int enemyTypeIndex)
    {
        base.EnemyDefeated(enemyTypeIndex); // Gọi logic cơ bản từ SpawnManager
        MissionOvercomeMap.Instance?.ShowMissionComplete3(); // Hiển thị missionComplete3
    }
}
