using System.Collections;
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
    public Text enemyCountTotalInputs;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EnemySpawnInfo[] enemySpawners;
    [SerializeField] private Transform[] spawnPoints;

    private int[] remainingEnemyCounts;
    private int totalEnemies; // Tổng số lượng enemy

    private void Start()
    {
        remainingEnemyCounts = new int[enemySpawners.Length];
        totalEnemies = 0;

        for (int i = 0; i < enemySpawners.Length; i++)
        {
            remainingEnemyCounts[i] = enemySpawners[i].enemyCount;
            totalEnemies += enemySpawners[i].enemyCount; // Cộng dồn tổng số lượng enemy
            UpdateEnemyCountText(i); // Cập nhật Text với tên và số lượng
        }

        UpdateTotalEnemyCountText(); // Hiển thị tổng số lượng enemy
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
        totalEnemies--; // Giảm tổng số lượng enemy còn lại
        UpdateEnemyCountText(enemyTypeIndex);
        UpdateTotalEnemyCountText(); // Cập nhật lại tổng số lượng enemy

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
                levelManager.AppearObjBoss();
            }
        }
    }

    public bool AreAllEnemiesDefeated()
    {
        return totalEnemies <= 0; // Kiểm tra nếu tổng số lượng enemy là 0
    }

    private void UpdateEnemyCountText(int enemyIndex)
    {
        enemySpawners[enemyIndex].enemyCountInputs.text = $"{enemySpawners[enemyIndex].enemyName}:{Mathf.Max(0, remainingEnemyCounts[enemyIndex])}";
    }

    private void UpdateTotalEnemyCountText()
    {
        foreach (var enemyInfo in enemySpawners)
        {
            if (enemyInfo.enemyCountTotalInputs != null)
            {
                // Hiển thị tên enemy và tổng số lượng toàn bộ enemy đã spawn
                enemyInfo.enemyCountTotalInputs.text = $"Total {enemyInfo.enemyName}:{Mathf.Max(0, totalEnemies)}";
            }
        }
    }


}
