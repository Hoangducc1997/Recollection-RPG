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

    private void Update()
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

        // Kiểm tra trạng thái tấn công nếu không còn tấn công nữa
        if (!playerAnimator.GetBool("isAttacking") && Time.time >= lastAttackTime + currentWeaponStats.attackDuration)
        {
            playerAnimator.SetBool("isAttacking", false);
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

    private IEnumerator ResetAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerAnimator.SetBool("isAttacking", false); // Đặt lại trạng thái sau khi tấn công
    }


    private void HandleMeleeAttack(WeaponSwordStats meleeStats)
    {
        if (Time.time >= lastAttackTime + meleeStats.cooldownTime)
        {
            if (!playerAnimator.GetBool("isAttacking"))
            {
                Collider2D[] enemiesAndBossesInRange = FindEnemiesAndBossesInRange(meleeStats.rangeAtk);
                if (enemiesAndBossesInRange.Length > 0)
                {
                    AttackMelee(enemiesAndBossesInRange, meleeStats);
                }
            }
        }
    }


    private void AttackMelee(Collider2D[] targets, WeaponSwordStats currentWeapon)
    {
        playerAnimator.SetBool("isAttacking", true);
        playerAnimator.SetInteger("isWeaponType", currentWeapon.animationIndex);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(currentWeapon.damage);
                Debug.Log($"Dealt {currentWeapon.damage} damage to enemy {target.name}");
            }
            else if (target.TryGetComponent(out BossBarManager bossBarManager))
            {
                bossBarManager.TakeDamage(currentWeapon.damage);
                Debug.Log($"Dealt {currentWeapon.damage} damage to boss {target.name}");
            }
            else
            {
                Debug.LogWarning($"{target.name} does not have valid Health component.");
            }
        }

        lastAttackTime = Time.time;
        StartCoroutine(ResetAttackCoroutine(currentWeapon.attackDuration));
    }

    private void HandleRangedAttack(WeaponBowStats rangedStats)
    {
        if (Time.time >= lastAttackTime + rangedStats.cooldownTime)
        {
            Transform nearestTarget = FindNearestEnemyOrBoss();
            if (nearestTarget != null)  // Nếu có mục tiêu trong phạm vi
            {
                ShootArrow(rangedStats);
                lastAttackTime = Time.time; // Đặt lại thời gian tấn công
                playerAnimator.SetTrigger("isAttacking");  // Kích hoạt animation tấn công
            }
            else
            {
                // Nếu không có mục tiêu trong phạm vi, đặt lại trạng thái tấn công
                playerAnimator.SetBool("isAttacking", false);
            }
        }
    }


    private void HandleMagicAttack(WeaponMagicStats magicStats)
    {
        if (Time.time >= lastAttackTime + magicStats.cooldownTime)
        {
            CastMagic(magicStats);
            lastAttackTime = Time.time; // Đặt lại thời gian tấn công
            playerAnimator.SetTrigger("isAttacking");  // Kích hoạt animation tấn công
        }
    }
    private void CastMagic(WeaponMagicStats magicStats)
    {
        Transform nearestTarget = FindNearestEnemyOrBoss();
        if (nearestTarget != null)
        {
            Vector2 direction = (nearestTarget.position - shootPoint.position).normalized;

            GameObject magic = Instantiate(magicPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
            rb.velocity = direction * magicStats.rangeAtk;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            magic.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Gán Animator và animationIndex cho ArrowAndMagicFly (hoặc script tương tự)
            if (magic.TryGetComponent(out ArrowAndMagicFly magicScript))
            {
                magicScript.SetDamage(magicStats.damage);
                magicScript.SetPlayerAnimator(playerAnimator, magicStats.animationIndex);
            }

            lastAttackTime = Time.time;
        }
        else
        {
            Debug.Log("No targets nearby for magic.");
        }
    }

    private void ShootArrow(WeaponBowStats rangedStats)
    {
        Transform nearestTarget = FindNearestEnemyOrBoss();
        if (nearestTarget != null)
        {
            Vector2 direction = (nearestTarget.position - shootPoint.position).normalized;

            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
            Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            rb.velocity = direction * rangedStats.rangeAtk;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Kích hoạt animation tại đây
            playerAnimator.SetInteger("isWeaponType", rangedStats.animationIndex);
            playerAnimator.SetTrigger("isAttacking");

            // Truyền damage và các thuộc tính khác vào mũi tên
            if (arrow.TryGetComponent(out ArrowAndMagicFly arrowScript))
            {
                arrowScript.SetDamage(rangedStats.damage);
            }

            lastAttackTime = Time.time;
        }
        else
        {
            Debug.Log("No targets nearby to shoot.");
        }
    }

    private Collider2D[] FindEnemiesAndBossesInRange(float range)
    {
        // Tìm cả Enemy và Boss bằng cách gộp LayerMask
        LayerMask combinedMask = LayerMask.GetMask("Enemy", "Boss");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, range, combinedMask);
        return targets;
    }


    private Transform FindNearestEnemyOrBoss()
    {
        LayerMask combinedMask = LayerMask.GetMask("Enemy", "Boss");
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 10f, combinedMask);

        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D target in targets)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = target.transform;
            }
        }

        return nearestTarget;
    }
    public void UpdateWeaponPrefabs(WeaponStats currentWeapon)
    {
        if (currentWeapon is WeaponBowStats bowStats)
        {
            arrowPrefab = bowStats.ArrowPrefab;
        }
        else if (currentWeapon is WeaponMagicStats magicStats)
        {
            magicPrefab = magicStats.MagicPrefab;
        }
        else
        {
            arrowPrefab = null;
            magicPrefab = null;
        }
    }

}
