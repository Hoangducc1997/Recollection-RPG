using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Weapon References")]
    [SerializeField] private WeaponLevelManager weaponLevelsManager;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject magicPrefab;
    [SerializeField] private Animator playerAnimator;

    [Header("Movement Input")]
    [SerializeField] private Joystick joystick; // Thêm joystick để kiểm tra di chuyển.

    private float lastAttackTime;

    void Update()
    {
        WeaponStats currentWeaponStats = weaponLevelsManager?.GetCurrentWeaponStats();

        if (currentWeaponStats == null)
        {
            Debug.LogWarning("No weapon equipped.");
            return;
        }

        bool isMoving = CheckIsMoving();

        if (currentWeaponStats is WeaponSwordStats swordStats && !playerAnimator.GetBool("isAttacking"))
        {
            HandleMeleeAttack(swordStats);
        }
        else if (currentWeaponStats is WeaponBowStats bowStats)
        {
            if (!isMoving) HandleRangedAttack(bowStats);
        }
        else if (currentWeaponStats is WeaponMagicStats magicStats)
        {
            if (!isMoving) HandleMagicAttack(magicStats);
        }
    }

    private bool CheckIsMoving()
    {
        // Kiểm tra nếu joystick đang được sử dụng hoặc player đang di chuyển
        if (joystick != null && joystick.Direction.magnitude > 0.2f)
        {
            return true;
        }

        return false;
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
    private IEnumerator ResetAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerAnimator.SetBool("isAttacking", false);
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

    private void ShootArrow(WeaponBowStats rangedStats)
    {
        Transform nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.position - shootPoint.position).normalized;

            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.velocity = direction * rangedStats.rangeAtk;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (arrow.TryGetComponent(out ArrowAndMagicFly arrowScript))
            {
                arrowScript.SetDamage(rangedStats.damage);
            }

            lastAttackTime = Time.time;
        }
        else
        {
            Debug.Log("No enemies nearby to shoot.");
        }
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

    private Collider2D[] FindEnemiesInRange(float range)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy"));
        Debug.Log($"Found {enemies.Length} enemies within range {range}");
        return enemies;
    }

    private Transform FindNearestEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 10f, LayerMask.GetMask("Enemy"));
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        return nearestEnemy;
    }
}
