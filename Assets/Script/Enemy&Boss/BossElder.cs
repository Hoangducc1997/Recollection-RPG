using System.Collections;
using UnityEngine;

public class BossElder : BossBarManager
{
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage); // Gọi hàm TakeDamage từ BossBarManager
        if (currentHealth <= 0 && !isDead) // Kiểm tra nếu boss chết và chưa xử lý
        {
            isDead = true; // Đảm bảo không xử lý lại nhiều lần
            MissionOvercomeMap.Instance?.ShowMissionComplete4(); // Gọi hàm khi boss chết
        }
    }
}
