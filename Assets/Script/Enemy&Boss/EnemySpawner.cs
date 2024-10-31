using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float enemyInterval = 0.5f;

    private void Start()
    {
        StartCoroutine(spawnEnemy(enemyInterval, enemyPrefabs));
    }

    private IEnumerator spawnEnemy(float interval, GameObject[] enemies)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Select a random enemy from the array
            int randomIndex = Random.Range(0, enemies.Length);
            GameObject enemy = enemies[randomIndex];

            // Spawn the enemy at a random position
            Instantiate(enemy, new Vector3(Random.Range(5f, 0f), Random.Range(-5f, 0f), 0), Quaternion.identity);
        }
    }
}
