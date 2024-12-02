using System.Collections;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    [SerializeField] private float distanceAttack = 2f; // Khoảng cách tấn công
    private Animator animator;
    private float lastAttackTime;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
    }

    private void Update()
    {
        if (player == null || playerBarManager == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Kiểm tra nếu player trong phạm vi và cooldown đã hết
        if (distance <= distanceAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(PerformAttack());
            lastAttackTime = Time.time;
        }
    }

    private IEnumerator PerformAttack()
    {
        if (animator != null)
        {
            animator.SetBool("isAttack", true);
        }

        yield return new WaitForSeconds(0.5f); // Delay trước khi gây sát thương

        if (playerBarManager != null)
        {
            playerBarManager.TakeDamage(damageEnemyAttack); // Gây sát thương
        }

        yield return new WaitForSeconds(0.5f); // Thời gian kết thúc animation
        if (animator != null)
        {
            animator.SetBool("isAttack", false);
        }
    }
}
