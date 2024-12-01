using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private WeaponLevel weaponLevel; // Gắn WeaponLevel trong Inspector
    [SerializeField] private Animator playerAnimator;

    private float lastAttackTime;

    [SerializeField] private WeaponLevelManager weaponLevelManager; // Thay thế WeaponLevel bằng WeaponLevelManager

    void Update()
    {
        WeaponMeleeStats currentWeaponStats = weaponLevelManager?.GetCurrentWeaponStats();

        if (currentWeaponStats != null && !playerAnimator.GetBool("isAttacking"))
        {
            if (Time.time >= lastAttackTime + currentWeaponStats.cooldownTime)
            {
                Collider2D[] enemiesInRange = FindEnemiesInRange(currentWeaponStats.rangeAtk);

                if (enemiesInRange.Length > 0)
                {
                    Attack(enemiesInRange, currentWeaponStats);
                }
            }
        }
    }

    private void Attack(Collider2D[] enemies, WeaponMeleeStats currentWeapon)
    {
        playerAnimator.SetBool("isAttacking", true);
        playerAnimator.SetInteger("isWeaponType", currentWeapon.animationIndex);

        foreach (Collider2D enemyCollider in enemies)
        {
            GameObject enemy = enemyCollider.gameObject;

            BossBarManager bossBarManager = enemy.GetComponent<BossBarManager>();
            if (bossBarManager != null)
            {
                bossBarManager.TakeDamage(currentWeapon.damage);
            }
            else
            {
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(currentWeapon.damage);
                }
            }
        }

        lastAttackTime = Time.time;
        StartCoroutine(ResetAttackCoroutine(currentWeapon.attackDuration));
    }

    private Collider2D[] FindEnemiesInRange(float range)
    {
        List<Collider2D> enemiesInRange = new List<Collider2D>();
        EnemyHealth[] allEnemies = FindObjectsOfType<EnemyHealth>();
        foreach (EnemyHealth enemy in allEnemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= range)
            {
                Collider2D collider = enemy.GetComponent<Collider2D>();
                if (collider != null)
                {
                    enemiesInRange.Add(collider);
                }
            }
        }

        return enemiesInRange.ToArray();
    }

    private IEnumerator ResetAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerAnimator.SetBool("isAttacking", false);
    }
}
