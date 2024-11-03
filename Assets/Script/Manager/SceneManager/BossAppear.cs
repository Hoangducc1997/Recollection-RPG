using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject bossAppear;
    public GameObject bossPrefab;
    public Transform spawnPoint; // Vị trí spawn cho bossPrefab
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm là Player
        if (collision.CompareTag("Player"))
        {
            // Spawn bossPrefab tại vị trí spawnPoint
            Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
    void Start()
    {
        bossAppear.SetActive(false);
    }

   
}
