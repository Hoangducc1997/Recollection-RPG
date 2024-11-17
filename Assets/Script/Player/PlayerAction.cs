using System.Collections;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private LayerMask enemyLayer;

    private Animator animator;
    private int currentLevel = 1;
    private float lastAttackTime;
    private bool isAttacking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Lấy Weapon từ WeaponManager
            Weapon currentWeapon = weaponManager.GetCurrentWeapon();
            if (currentWeapon != null && Time.time >= lastAttackTime + currentWeapon.weaponStats.cooldownTime)
            {
                Collider2D[] enemiesInRange = FindEnemiesInRange(currentWeapon.weaponStats.rangeAtk);
                if (enemiesInRange.Length > 0)
                {
                    Attack(enemiesInRange);
                }
            }
        }
    }

    private void Attack(Collider2D[] enemies)
    {
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon != null && Time.time >= lastAttackTime + currentWeapon.weaponStats.cooldownTime)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);

            // Tạo các hành động tấn công đối với kẻ địch trong phạm vi
            foreach (Collider2D enemy in enemies)
            {
                BossBarManager boss = enemy.GetComponent<BossBarManager>();
                if (boss != null)
                {
                    boss.TakeDamage(currentWeapon.weaponStats.damage);
                }
                else
                {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(currentWeapon.weaponStats.damage);
                    }
                }
            }

            lastAttackTime = Time.time;
            StartCoroutine(ResetAttackCoroutine(currentWeapon.weaponStats.attackDuration));
        }
    }

    private Collider2D[] FindEnemiesInRange(float range)
    {
        return Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    private IEnumerator ResetAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        ResetAttack();
    }
}
