using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EnemySpawnInfo
{
    public string enemyName; // Tên của enemy
    public GameObject enemyPrefab;
    public int enemyCount;
    public float enemyTimeSpawn;
    public Text enemyCountInputs;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnInfo[] enemySpawners;
    [SerializeField] private Transform[] spawnPoints;

    private int[] remainingEnemyCounts;

    private void Start()
    {
        remainingEnemyCounts = new int[enemySpawners.Length];

        for (int i = 0; i < enemySpawners.Length; i++)
        {
            remainingEnemyCounts[i] = enemySpawners[i].enemyCount;
            UpdateEnemyCountText(i); // Cập nhật Text với tên và số lượng
        }

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        foreach (var enemyInfo in enemySpawners)
        {
            for (int i = 0; i < enemyInfo.enemyCount; i++)
            {
                yield return new WaitForSeconds(enemyInfo.enemyTimeSpawn);

                int spawnIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[spawnIndex];

                GameObject spawnedEnemy = Instantiate(enemyInfo.enemyPrefab, spawnPoint.position, spawnPoint.rotation);

                EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.enemySpawner = this;
                    enemyHealth.enemyTypeIndex = System.Array.IndexOf(enemySpawners, enemyInfo);
                }
            }
        }
    }

    public void EnemyDefeated(int enemyTypeIndex)
    {
        remainingEnemyCounts[enemyTypeIndex]--;
        UpdateEnemyCountText(enemyTypeIndex);

        // Kiểm tra nếu tất cả enemy đã bị tiêu diệt
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
                levelManager.AppearObjBossAndNextScene();
            }
        }
    }


    public bool AreAllEnemiesDefeated()
    {
        foreach (int count in remainingEnemyCounts)
        {
            if (count > 0)
            {
                return false; // Vẫn còn enemy
            }
        }
        return true; // Không còn enemy nào
    }

    private void UpdateEnemyCountText(int enemyIndex)
    {
        enemySpawners[enemyIndex].enemyCountInputs.text = $"{enemySpawners[enemyIndex].enemyName}: {Mathf.Max(0, remainingEnemyCounts[enemyIndex])}";
    }
}
