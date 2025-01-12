using System.Collections;
using UnityEngine;

public class BossLava : BossBarManager
{
    [SerializeField] private GameObject BulletNormal;
    [SerializeField] private GameObject BulletPlus;
    [SerializeField] private GameObject BulletStorm;
    [SerializeField] private int BossAngryHeath = 90; // Ngưỡng máu để kích hoạt BulletPlus
    [SerializeField] private int BossCrazyHeath = 50; // Ngưỡng máu để kích hoạt BulletPlus
    public override void Start()
    {
        base.Start(); // Gọi phương thức Start từ BossBarManager
        BulletPlus.SetActive(false); // Ban đầu tắt BulletPlus
        BulletStorm.SetActive(false); // Ban đầu tắt BulletPlus
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
        if (currentHealth <= BossCrazyHeath && BulletStorm != null && !BulletStorm.activeSelf)
        {
            BulletStorm.SetActive(true);
            BulletNormal.SetActive(false);
            Debug.Log("BulletPlus has been activated!");
        }
    }
}
