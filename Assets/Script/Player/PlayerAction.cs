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
    [SerializeField] private Joystick joystick;

    private float lastAttackTime;

    private void Update()
    {
        WeaponStats currentWeaponStats = weaponLevelsManager?.GetCurrentWeaponStats();

        if (currentWeaponStats == null)
        {
            playerAnimator.SetBool("isAttacking", false); // Đặt lại trạng thái tấn công
            return;
        }

        // Kiểm tra nếu đang di chuyển thì không tấn công
        if (CheckIsMoving())
        {
            playerAnimator.SetBool("isAttacking", false); // Đặt lại trạng thái nếu di chuyển
            return;
        }

        // Xử lý từng loại vũ khí
        if (currentWeaponStats is WeaponSwordStats swordStats)
        {
            HandleMeleeAttack(swordStats);
        }
        else if (currentWeaponStats is WeaponBowStats bowStats)
        {
            HandleBowAttack(bowStats);
        }
        else if (currentWeaponStats is WeaponMagicStats magicStats)
        {
            HandleMagicAttack(magicStats);
        }

        ResetAttackState(currentWeaponStats);
    }

    private bool CheckIsMoving()
    {
        return joystick != null && joystick.Direction.magnitude > 0.2f;
    }

    private void ResetAttackState(WeaponStats currentWeaponStats)
    {
        if (!playerAnimator.GetBool("isAttacking") && Time.time >= lastAttackTime + currentWeaponStats.attackDuration)
        {
            playerAnimator.SetBool("isAttacking", false);
        }
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
               
            }
            else if (target.TryGetComponent(out BossBarManager bossBarManager))
            {
                bossBarManager.TakeDamage(currentWeapon.damage);
                
            }
            else
            {
                Debug.LogWarning($"{target.name} does not have valid Health component.");
            }
        }

        lastAttackTime = Time.time;
        StartCoroutine(ResetAttackCoroutine(currentWeapon.attackDuration)); // Đảm bảo hàm tồn tại.
    }

    private IEnumerator ResetAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerAnimator.SetBool("isAttacking", false);
    }

    private void HandleBowAttack(WeaponBowStats bowStats)
    {
        if (Time.time >= lastAttackTime + bowStats.cooldownTime)
        {
            Transform nearestTarget = FindNearestEnemyOrBoss(bowStats.rangeAtk);

            // Kiểm tra nếu không có mục tiêu hoặc mục tiêu ngoài phạm vi
            if (nearestTarget == null || Vector2.Distance(transform.position, nearestTarget.position) > bowStats.rangeAtk)
            {
                Debug.Log("No target in range for bow attack.");
                playerAnimator.SetBool("isAttacking", false); // Đặt lại trạng thái tấn công
                return;
            }

            ShootArrow(bowStats, nearestTarget);
        }
    }



    private void ShootArrow(WeaponBowStats rangedStats, Transform target)
    {
        Vector2 direction = (target.position - shootPoint.position).normalized;

        GameObject arrow = Instantiate(rangedStats.ArrowPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = direction * rangedStats.ArrowSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (arrow.TryGetComponent(out ArrowAndMagicFly arrowScript))
        {
            arrowScript.SetDamage(rangedStats.damage);
        }

        playerAnimator.SetInteger("isWeaponType", rangedStats.animationIndex);
        playerAnimator.SetTrigger("isAttacking");

        lastAttackTime = Time.time;
    }

    private void HandleMagicAttack(WeaponMagicStats magicStats)
    {
        if (Time.time >= lastAttackTime + magicStats.cooldownTime)
        {
            Transform nearestTarget = FindNearestEnemyOrBoss(magicStats.rangeAtk);

            // Kiểm tra nếu không có mục tiêu hoặc mục tiêu ngoài phạm vi
            if (nearestTarget == null || Vector2.Distance(transform.position, nearestTarget.position) > magicStats.rangeAtk)
            {
                Debug.Log("No target in range for magic attack.");
                playerAnimator.SetBool("isAttacking", false); // Đặt lại trạng thái tấn công
                return;
            }

            CastMagic(magicStats, nearestTarget);
        }
    }

    private void CastMagic(WeaponMagicStats magicStats, Transform target)
    {
        Vector2 direction = (target.position - shootPoint.position).normalized;

        GameObject magic = Instantiate(magicStats.MagicPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody2D rb = magic.GetComponent<Rigidbody2D>();
        rb.velocity = direction * magicStats.MagicSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        magic.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (magic.TryGetComponent(out ArrowAndMagicFly magicScript))
        {
            magicScript.SetDamage(magicStats.damage);
        }

        playerAnimator.SetInteger("isWeaponType", magicStats.animationIndex);
        playerAnimator.SetTrigger("isAttacking");

        lastAttackTime = Time.time;
    }


    private Collider2D[] FindEnemiesAndBossesInRange(float range)
    {
        LayerMask combinedMask = LayerMask.GetMask("Enemy", "Boss");
        return Physics2D.OverlapCircleAll(transform.position, range, combinedMask);
    }

    private Transform FindNearestEnemyOrBoss(float range)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, LayerMask.GetMask("Enemy", "Boss"));
        Transform nearestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestTarget = hit.transform;
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
