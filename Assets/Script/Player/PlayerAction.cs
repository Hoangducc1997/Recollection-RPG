using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private WeaponLevelManager weaponLevelManager; // Thay thế WeaponManager
    [SerializeField] private LayerMask enemyLayer;

    private float lastAttackTime;
    private bool isAttacking;

    void Update()
    {
        if (!isAttacking)
        {
            GameObject currentWeaponGO = weaponLevelManager.GetCurrentWeapon();
            if (currentWeaponGO != null)
            {
                Weapon currentWeapon = currentWeaponGO.GetComponent<Weapon>();
                if (currentWeapon != null && Time.time >= lastAttackTime + currentWeapon.GetCooldownTime())
                {
                    Collider2D[] enemiesInRange = FindEnemiesInRange(currentWeapon.GetRange());
                    if (enemiesInRange.Length > 0)
                    {
                        Attack(enemiesInRange, currentWeapon);
                    }
                }
            }
        }
    }

    private void Attack(Collider2D[] enemies, Weapon currentWeapon)
    {
        if (currentWeapon == null) return;

        isAttacking = true;

        // Tạo các hành động tấn công đối với kẻ địch trong phạm vi
        foreach (Collider2D enemy in enemies)
        {
            BossBarManager boss = enemy.GetComponent<BossBarManager>();
            if (boss != null)
            {
                boss.TakeDamage(currentWeapon.GetDamage());
            }
            else
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(currentWeapon.GetDamage());
                }
            }
        }

        lastAttackTime = Time.time;
        StartCoroutine(ResetAttackCoroutine(currentWeapon.GetAttackDuration()));
    }

    private Collider2D[] FindEnemiesInRange(float range)
    {
        return Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }

    private IEnumerator ResetAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResetAttack();
    }
}
