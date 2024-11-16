using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager; // Weapon manager
    [SerializeField] private string enemyTag = "Enemy"; // Tag for enemies

    private Animator animator;
    private float lastAttackTime;
    private bool isAttacking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Switch weapons with number keys if not attacking
        if (!isAttacking)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        }

        // Auto-attack enemies within range if not attacking
        if (!isAttacking)
        {
            float weaponRange = weaponManager.GetCurrentWeapon()?.rangeAtk ?? 0;
            GameObject[] enemiesInRange = FindEnemiesInRange(weaponRange);

            if (enemiesInRange.Length > 0)
            {
                Attack(); // Attack when enemies are within range
            }
        }
    }

    public void SwitchWeapon(int weaponIndex)
    {
        weaponManager.SwitchWeapon(weaponIndex); // Switch current weapon
    }

    private void Attack()
    {
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();
        if (currentWeapon != null)
        {
            GameObject[] enemies = FindEnemiesInRange(currentWeapon.rangeAtk);

            if (enemies.Length > 0 && Time.time >= lastAttackTime + currentWeapon.cooldownTime)
            {
                isAttacking = true;
                animator.SetBool("isAttacking", true);

                foreach (GameObject target in enemies)
                {
                    // Check if the target is a Boss
                    BossBarManager bossBarManager = target.GetComponent<BossBarManager>();
                    if (bossBarManager != null)
                    {
                        bossBarManager.TakeDamage(currentWeapon.damage);
                        Debug.Log("Attacked boss: " + target.name + " dealing " + currentWeapon.damage + " damage.");
                    }
                    else
                    {
                        // If not a Boss, check if it's an Enemy
                        EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(currentWeapon.damage);
                            Debug.Log("Attacked enemy: " + target.name + " dealing " + currentWeapon.damage + " damage.");
                        }
                    }
                }

                lastAttackTime = Time.time;
                Invoke("ResetAttack", currentWeapon.attackDuration);
            }
        }
    }

    private GameObject[] FindEnemiesInRange(float range)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        List<GameObject> enemiesInRange = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= range)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange.ToArray();
    }

    private void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        Debug.Log("Attack reset. isAttacking: " + isAttacking);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Weapon currentWeapon = weaponManager.GetCurrentWeapon();
        float rangeToDraw = currentWeapon != null ? currentWeapon.rangeAtk : 0;

        if (rangeToDraw > 0)
        {
            Gizmos.DrawWireSphere(transform.position, rangeToDraw);
        }
    }
}
