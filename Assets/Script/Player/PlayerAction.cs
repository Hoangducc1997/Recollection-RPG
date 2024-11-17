using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager; // Quản lý vũ khí của người chơi
    [SerializeField] private string enemyTag = "Enemy";   // Tag của kẻ địch
    [SerializeField] private LayerMask enemyLayer;        // Layer của kẻ địch

    private Animator animator;
    private float lastAttackTime;  // Thời gian tấn công lần cuối
    private bool isAttacking;      // Trạng thái đang tấn công

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            // Đổi vũ khí bằng phím số
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);

            // Tấn công tự động nếu có kẻ địch trong phạm vi
            WeaponStats currentWeapon = weaponManager.GetCurrentWeapon();
            if (currentWeapon != null)
            {
                Collider2D[] enemiesInRange = FindEnemiesInRange(currentWeapon.rangeAtk);
                if (enemiesInRange.Length > 0)
                {
                    Attack(enemiesInRange); // Tấn công nếu tìm thấy kẻ địch
                }
            }
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        weaponManager.SwitchWeapon(weaponIndex); // Chuyển đổi vũ khí
    }

    private void Attack(Collider2D[] enemies)
    {
        WeaponStats currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon != null && Time.time >= lastAttackTime + currentWeapon.cooldownTime)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);

            foreach (Collider2D enemy in enemies)
            {
                // Gây sát thương cho boss hoặc kẻ địch thường
                BossBarManager boss = enemy.GetComponent<BossBarManager>();
                if (boss != null)
                {
                    boss.TakeDamage(currentWeapon.damage);
                    Debug.Log($"Attacked boss: {enemy.name}, Damage: {currentWeapon.damage}");
                }
                else
                {
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage(currentWeapon.damage);
                        Debug.Log($"Attacked enemy: {enemy.name}, Damage: {currentWeapon.damage}");
                    }
                }
            }

            lastAttackTime = Time.time;
            Invoke(nameof(ResetAttack), currentWeapon.attackDuration);
        }
    }

    private Collider2D[] FindEnemiesInRange(float range)
    {
        // Tìm tất cả các đối tượng trong phạm vi sử dụng Physics2D
        return Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        Debug.Log("Attack reset.");
    }

    private void OnDrawGizmosSelected()
    {
        // Hiển thị phạm vi tấn công của vũ khí hiện tại trong Editor
        Gizmos.color = Color.red;
        WeaponStats currentWeapon = weaponManager.GetCurrentWeapon();
        float rangeToDraw = currentWeapon != null ? currentWeapon.rangeAtk : 0;

        if (rangeToDraw > 0)
        {
            Gizmos.DrawWireSphere(transform.position, rangeToDraw);
        }
    }
}
