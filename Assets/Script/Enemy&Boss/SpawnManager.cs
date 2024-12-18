using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float spawnDelay = 2f; // Thời gian delay trước khi bắt đầu spawn

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

        // Delay trước khi bắt đầu spawn
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        // Đợi khoảng thời gian delay trước khi bắt đầu spawn
        yield return new WaitForSeconds(spawnDelay);

        // Bắt đầu spawn enemy sau delay
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Tạo danh sách để quản lý số lượng còn lại của từng loại enemy
        List<int> remainingIndexes = new List<int>();
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            for (int j = 0; j < enemySpawners[i].enemyCount; j++)
            {
                remainingIndexes.Add(i);
            }
        }

        while (remainingIndexes.Count > 0)
        {
            // Chọn ngẫu nhiên một loại enemy từ danh sách còn lại
            int randomEnemyIndex = Random.Range(0, remainingIndexes.Count);
            int selectedEnemyType = remainingIndexes[randomEnemyIndex];
            remainingIndexes.RemoveAt(randomEnemyIndex);

            // Chọn ngẫu nhiên một điểm spawn
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // Spawn enemy
            GameObject spawnedEnemy = Instantiate(
                enemySpawners[selectedEnemyType].enemyPrefab,
                spawnPoint.position,
                spawnPoint.rotation
            );

            EnemyHealth enemyHealth = spawnedEnemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.enemySpawner = this;
                enemyHealth.enemyTypeIndex = selectedEnemyType;
            }

            // Cập nhật số lượng kẻ địch đã spawn
            currentSpawnedCounts[selectedEnemyType]++;
            remainingEnemyCounts[selectedEnemyType]--;
            UpdateEnemyCountText(selectedEnemyType);

            // Thời gian chờ giữa các lần spawn
            yield return new WaitForSeconds(enemySpawners[selectedEnemyType].enemyTimeSpawn);
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

            LevelMapBossAfterManager levelManager = FindObjectOfType<LevelMapBossAfterManager>();
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
                $"{enemySpawners[enemyIndex].enemyName}'S EXIST: {Mathf.Max(0, currentSpawnedCounts[enemyIndex])}";
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
