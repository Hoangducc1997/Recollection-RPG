﻿using System.Collections;
using UnityEngine;

public class BossLava : BossBarManager
{
    [SerializeField] private GameObject BulletPlus;
    [SerializeField] private int BossAngryHeath = 30; // Ngưỡng máu để kích hoạt BulletPlus

    public override void Start()
    {
        base.Start(); // Gọi phương thức Start từ BossBarManager
        BulletPlus.SetActive(false); // Ban đầu tắt BulletPlus
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage); // Gọi hàm TakeDamage từ BossBarManager

        // Kích hoạt BulletPlus khi máu <= 30
        if (currentHealth <= BossAngryHeath && BulletPlus != null && !BulletPlus.activeSelf)
        {
            BulletPlus.SetActive(true);
            Debug.Log("BulletPlus has been activated!");
        }
        if (currentHealth <= 0 && !isDead) // Kiểm tra nếu boss chết và chưa xử lý
        {
            isDead = true; // Đảm bảo không xử lý lại nhiều lần
            MissionOvercomeMap.Instance?.ShowMissionComplete4(); // Gọi hàm khi boss chết
        }
    }
}
