using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private WeaponLevelManager weaponLevelManager;
    [SerializeField] private Transform shootPoint; // Điểm bắn tên
    [SerializeField] private GameObject arrowPrefab; // Prefab mũi tên
    [SerializeField] private Animator playerAnimator;

    private float lastAttackTime;

    void Update()
    {
        object currentWeaponStats = weaponLevelManager?.GetCurrentWeaponStats();

        if (currentWeaponStats is WeaponMeleeStats meleeStats && !playerAnimator.GetBool("isAttacking"))
        {
            HandleMeleeAttack(meleeStats);
        }
        else if (currentWeaponStats is WeaponRangedStats rangedStats)
        {
            HandleRangedAttack(rangedStats);
        }
    }

    private void HandleMeleeAttack(WeaponMeleeStats meleeStats)
    {
        if (Time.time >= lastAttackTime + meleeStats.cooldownTime)
        {
            Collider2D[] enemiesInRange = FindEnemiesInRange(meleeStats.rangeAtk);
            if (enemiesInRange.Length > 0)
            {
                AttackMelee(enemiesInRange, meleeStats);
            }
        }
    }

    private void HandleRangedAttack(WeaponRangedStats rangedStats)
    {
        if (Time.time >= lastAttackTime + rangedStats.cooldownTime)
        {
            ShootArrow(rangedStats);
        }
    }

    private void AttackMelee(Collider2D[] enemies, WeaponMeleeStats currentWeapon)
    {
        playerAnimator.SetBool("isAttacking", true);
        playerAnimator.SetInteger("isWeaponType", currentWeapon.animationIndex);

        foreach (Collider2D enemy in enemies)
        {
            var enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth?.TakeDamage(currentWeapon.damage);
        }

        lastAttackTime = Time.time;
        StartCoroutine(ResetAttackCoroutine(currentWeapon.attackDuration));
    }

    private void ShootArrow(WeaponRangedStats rangedStats)
    {
        playerAnimator.SetTrigger("Shoot");
        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = shootPoint.right * rangedStats.rangeAtk;

        ArrowAndMagicFly arrowScript = arrow.GetComponent<ArrowAndMagicFly>();
        if (arrowScript != null)
        {
            arrowScript.SetDamage(rangedStats.damage);
        }

        lastAttackTime = Time.time;
    }

    private IEnumerator ResetAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerAnimator.SetBool("isAttacking", false);
    }

    private Collider2D[] FindEnemiesInRange(float range)
    {
        return Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
    }
}
