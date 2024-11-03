﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    public GameObject bossAppearAndNextScene;
    public GameObject bossPrefab;
    public Transform spawnPoint; // Vị trí spawn cho bossPrefab

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
        bossAppearAndNextScene.SetActive(false);
    }

    public void AppearObjBossAndNextScene()
    {
        bossAppearAndNextScene.SetActive(true);
    }
}
