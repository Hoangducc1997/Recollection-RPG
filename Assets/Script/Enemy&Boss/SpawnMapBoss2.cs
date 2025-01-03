using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapBoss2 : SpawnManager
{
    protected override void EnemyDefeated(int enemyTypeIndex)
    {
        base.EnemyDefeated(enemyTypeIndex); // Gọi logic cơ bản từ SpawnManager
        MissionOvercomeMap.Instance?.ShowMissionComplete4(); // Hiển thị missionComplete4
    }
}
