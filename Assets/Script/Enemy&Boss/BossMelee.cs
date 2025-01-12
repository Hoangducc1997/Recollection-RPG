using System.Collections;
using UnityEngine;

public class BossMelee : BossBase
{
    private Animator animator; // Gán Animator từ đối tượng cha
    [SerializeField] private Animator animatorEffect;
    [SerializeField] private int meleeDamage = 10; // Sát thương cận chiến
    protected float lastAttackTime;
    protected bool isPlayerInRange = false;

    protected override void Start()
    {
        base.Start();

        // Kiểm tra và lấy Animator từ đối tượng cha nếu chưa gán sẵn
        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator not found on parent object.");
            }
        }
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
        // Bắt đầu animation tấn công
        animator.SetBool("isAttack", true);
        animatorEffect.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);

        if (playerBarManager != null)
        {
            MeleeAttack();  // Gọi hàm tấn công cận chiến
        }

        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttack", false); // Tắt animation tấn công sau khi hoàn thành
        animatorEffect.SetBool("isAttack", false);
    }

    void MeleeAttack()
    {
        // Gây sát thương cho player khi tấn công cận chiến
        playerBarManager.TakeDamage(meleeDamage);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerInRange = true;

        }
    }

    protected void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }


}
