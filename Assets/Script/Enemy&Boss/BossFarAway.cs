using System.Collections;
using UnityEngine;

public class BossFarAway : BossManager
{
    public GameObject bulletPrefab;       // Prefab của viên đạn
    public Transform shootingPoint;       // Vị trí bắn đạn ra

    private float lastShotTime;

    protected override void Update()
    {
        base.Update();

        if (isPlayerInRange && Time.time >= lastShotTime + attackCooldown)
        {
            ShootAtPlayer();
            lastShotTime = Time.time;
        }
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(player.transform.position);  // Gửi vị trí của player đến viên đạn
    }
}
