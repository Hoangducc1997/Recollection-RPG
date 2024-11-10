using System.Collections;
using UnityEngine;

public class BossManager : BossBase
{
    protected Animator animator;
    [SerializeField] protected int damageBossAttack;
    [SerializeField] protected float attackCooldown = 1f;
    protected float lastAttackTime;
    protected bool isPlayerInRange = false;  // Kiểm tra player trong vùng phát hiện

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown;
    }

    protected virtual void Update()
    {
        if (isPlayerInRange && Time.time >= lastAttackTime + attackCooldown)
        {
            StartCoroutine(PerformAttack());
            lastAttackTime = Time.time;
        }
    }

    protected virtual IEnumerator PerformAttack()
    {
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);

        if (playerBarManager != null)
        {
            BossAttack();
        }

        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttack", false);
    }

    public virtual void BossAttack()
    {
        if (playerBarManager != null)
        {
            playerBarManager.TakeDamage(damageBossAttack);
        }
    }

    // Phương thức OnTriggerEnter2D để kiểm tra player có vào vùng phát hiện
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // Phương thức OnTriggerExit2D để kiểm tra player ra khỏi vùng phát hiện
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
