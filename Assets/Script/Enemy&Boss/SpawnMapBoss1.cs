using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapBoss1 : SpawnManager
{
    protected override void EnemyDefeated(int enemyTypeIndex)
    {
        base.EnemyDefeated(enemyTypeIndex); // Gọi logic cơ bản từ SpawnManager

        // Kiểm tra nếu tất cả kẻ địch đã bị tiêu diệt
        if (AreAllEnemiesDefeated())
        {
            MissionOvercomeMap.Instance?.ShowMissionComplete4(); // Hiển thị missionComplete4
        }
    }
}
