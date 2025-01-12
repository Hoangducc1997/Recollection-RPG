using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapBoss2 : SpawnManager
{
    protected override void EnemyDefeated(int enemyTypeIndex)
    {
        base.EnemyDefeated(enemyTypeIndex); // Gọi logic cơ bản từ SpawnManager

        // Kiểm tra nếu tất cả kẻ địch đã bị tiêu diệt
        if (AreAllEnemiesDefeated())
        {
            MissionOvercomeMap.Instance?.ShowMissionComplete8(); // Hiển thị missionComplete4
        }
    }
}
