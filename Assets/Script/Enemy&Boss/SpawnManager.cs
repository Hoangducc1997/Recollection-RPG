﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemySpawnInfo
{
    public string enemyName;
    public GameObject enemyPrefab;
    public int enemyCount; // Tổng số lượng kẻ địch sẽ spawn
    public float enemyTimeSpawn; // Thời gian giữa mỗi lần spawn
    public Text enemyCountInputs; // UI để hiển thị số lượng đã spawn
    public Text enemyCountTotalInputs; // UI để hiển thị tổng số lượng sẽ spawn
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnInfo[] enemySpawners;
    [SerializeField] private Transform[] spawnPoints;

    private int[] currentSpawnedCounts; // Số lượng kẻ địch đã spawn hiện tại
    private int[] remainingEnemyCounts; // Số lượng kẻ địch còn lại để spawn

    private void Start()
    {
        currentSpawnedCounts = new int[enemySpawners.Length];
        remainingEnemyCounts = new int[enemySpawners.Length];

        for (int i = 0; i < enemySpawners.Length; i++)
        {
            currentSpawnedCounts[i] = 0; // Bắt đầu số lượng đã spawn là 0
            remainingEnemyCounts[i] = enemySpawners[i].enemyCount; // Khởi tạo số lượng kẻ địch cần spawn
            UpdateEnemyCountText(i); // Cập nhật UI số lượng đã spawn
            UpdateTotalEnemyCountText(i); // Hiển thị tổng số lượng sẽ spawn
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            for (int j = 0; j < enemySpawners[i].enemyCount; j++)
            {
                yield return new WaitForSeconds(enemySpawners[i].enemyTimeSpawn);

                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[spawnIndex];

                GameObject spawnedEnemy = Instantiate(enemySpawners[i].enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.enemySpawner = this;
                    enemyHealth.enemyTypeIndex = i;
                }

                // Cập nhật số lượng kẻ địch đã spawn
                currentSpawnedCounts[i]++;
                remainingEnemyCounts[i]--;
                UpdateEnemyCountText(i);
            }
        }
    }

    public void EnemyDefeated(int enemyTypeIndex)
    {
        // Khi kẻ địch bị tiêu diệt, giảm số lượng đã spawn
        currentSpawnedCounts[enemyTypeIndex]--;
        UpdateEnemyCountText(enemyTypeIndex);

        // Kiểm tra nếu tất cả kẻ địch đã bị tiêu diệt
        if (AreAllEnemiesDefeated())
        {
            GamePlayPopup gamePlayPopup = FindObjectOfType<GamePlayPopup>();
            if (gamePlayPopup != null)
            {
                gamePlayPopup.ShowFindBossText();
            }

            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.AppearObjBoss();
            }
        }
    }

    public bool AreAllEnemiesDefeated()
    {
        foreach (int count in currentSpawnedCounts)
        {
            if (count > 0)
            {
                return false; // Vẫn còn ít nhất một kẻ địch chưa bị tiêu diệt
            }
        }
        return true; // Tất cả kẻ địch đã bị tiêu diệt
    }

    private void UpdateEnemyCountText(int enemyIndex)
    {
        if (enemySpawners[enemyIndex].enemyCountInputs != null)
        {
            enemySpawners[enemyIndex].enemyCountInputs.text =
                $"{enemySpawners[enemyIndex].enemyName}: {Mathf.Max(0, currentSpawnedCounts[enemyIndex])}";
        }
    }

    private void UpdateTotalEnemyCountText(int enemyIndex)
    {
        if (enemySpawners[enemyIndex].enemyCountTotalInputs != null)
        {
            enemySpawners[enemyIndex].enemyCountTotalInputs.text =
                $"{enemySpawners[enemyIndex].enemyName} Total: {enemySpawners[enemyIndex].enemyCount}";
        }
    }
}
