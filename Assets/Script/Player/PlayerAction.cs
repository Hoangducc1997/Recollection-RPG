// PlayerAction.cs
using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private WeaponLevelManager weaponLevelsManager;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private Animator playerAnimator;

    private float lastAttackTime;

    void Update()
    {
        WeaponStats currentWeaponStats = weaponLevelsManager?.GetCurrentWeaponStats();

        if (currentWeaponStats == null)
        {
            Debug.LogWarning("No weapon equipped.");
            return;
        }

        if (currentWeaponStats is WeaponSwordStats swordStats && !playerAnimator.GetBool("isAttacking"))
        {
            HandleMeleeAttack(swordStats);
        }
        else if (currentWeaponStats is WeaponBowStats bowStats)
        {
            HandleRangedAttack(bowStats);
        }
        else if (currentWeaponStats is WeaponMagicStats magicStats)
        {
            HandleMagicAttack(magicStats);
        }
    }

    private void HandleMeleeAttack(WeaponSwordStats meleeStats)
    {
        if (Time.time >= lastAttackTime + meleeStats.cooldownTime)
        {
            Collider2D[] enemiesInRange = FindEnemiesInRange(meleeStats.rangeAtk);
            if (enemiesInRange.Length > 0)
            {
                AttackMelee(enemiesInRange, meleeStats);
            }
            else
            {
                Debug.Log("No enemies in range for melee attack.");
            }
        }
    }

    private void HandleRangedAttack(WeaponBowStats rangedStats)
    {
        if (Time.time >= lastAttackTime + rangedStats.cooldownTime)
        {
            ShootArrow(rangedStats);
        }
    }

    private void HandleMagicAttack(WeaponMagicStats magicStats)
    {
        if (Time.time >= lastAttackTime + magicStats.cooldownTime)
        {
            CastMagic(magicStats);
        }
    }

    private void AttackMelee(Collider2D[] enemies, WeaponSwordStats currentWeapon)
    {
        playerAnimator.SetBool("isAttacking", true);
        playerAnimator.SetInteger("isWeaponType", currentWeapon.animationIndex);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(currentWeapon.damage);
                Debug.Log($"Dealt {currentWeapon.damage} damage to {enemy.name}");
            }
            else
            {
                Debug.LogWarning($"{enemy.name} does not have EnemyHealth component.");
            }
        }

        lastAttackTime = Time.time;
        StartCoroutine(ResetAttackCoroutine(currentWeapon.attackDuration));
    }

    private void ShootArrow(WeaponBowStats rangedStats)
    {
        playerAnimator.SetTrigger("Shoot");
        playerAnimator.SetInteger("isWeaponType", rangedStats.animationIndex);

        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = shootPoint.right * rangedStats.rangeAtk;

        if (arrow.TryGetComponent(out ArrowAndMagicFly arrowScript))
        {
            arrowScript.SetDamage(rangedStats.damage);
        }

        lastAttackTime = Time.time;
    }

    private void CastMagic(WeaponMagicStats magicStats)
    {
        playerAnimator.SetTrigger("CastMagic");
        playerAnimator.SetInteger("isWeaponType", magicStats.animationIndex);

        GameObject magic = Instantiate(magicPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
        rb.velocity = shootPoint.right * magicStats.rangeAtk;

        if (magic.TryGetComponent(out ArrowAndMagicFly magicScript))
        {
            magicScript.SetDamage(magicStats.damage);
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
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        Debug.Log($"Found {enemies.Length} enemies within range {range}");
        return enemies;
    }
}