using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarManager : MonoBehaviour
{
    Animator animator;
    protected bool isDead = false; // Thêm cờ kiểm tra trạng thái chết của boss

    [SerializeField] int maxHealth;
    protected int currentHealth; // Cho phép truy cập từ lớp con

    public BossBar healthBar;
    [SerializeField] private int expForPlayer; // Điểm kinh nghiệm cho Player
    private PlayerExpManager playerExpManager; // Tham chiếu PlayerExpManager
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        playerExpManager = FindObjectOfType<PlayerExpManager>();
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }


    public virtual void TakeDamage(int damage)
    {
        if (isDead) return; // Không thực hiện gì nếu boss đã chết

        currentHealth -= damage;
        if (!animator.GetBool("isHurt"))
        {
            animator.SetBool("isHurt", true);
            StartCoroutine(HurtAnim());
        }
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(DestroyBoss());
            isDead = true; // Đánh dấu boss đã chết
            AudioManager.Instance.PlayVFX("Boss1Death");
            Debug.Log("BossDeath");
            // Tăng điểm kinh nghiệm cho Player nếu PlayerExpManager đã được tham chiếu
            if (playerExpManager != null)
            {
                playerExpManager.AddExp(expForPlayer);
            }
            else
            {
                Debug.LogWarning("Không thể thêm EXP: PlayerExpManager không tồn tại.");
            }
            LevelMapBossAfterManager levelMapBossAfterManager = FindObjectOfType<LevelMapBossAfterManager>();
            if (levelMapBossAfterManager != null)
            {
                levelMapBossAfterManager.AppearObjNextScene();
            }

            LevelMapBossBeforeManager levelMapBossBeforeManager = FindObjectOfType<LevelMapBossBeforeManager>();
            if (levelMapBossBeforeManager != null)
            {
                levelMapBossBeforeManager.AppearObjNextScene();
            }
        }
    }

    private IEnumerator HurtAnim()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHurt", false);
    }

    public virtual IEnumerator DestroyBoss()
    {
        Debug.Log("DestroyBoss started");
        animator.SetBool("isDeath", true);
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false; // Tắt collider
        yield return new WaitForSeconds(3f);
        Debug.Log("Destroying Boss");
        Destroy(gameObject);
    }


    // Hàm để kiểm tra trạng thái chết từ script khác
    public bool IsDead()
    {
        return isDead;
    }
}
