using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;

    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("Death Settings")]
    [SerializeField] private float timeForAnimDeath = 1f;

    [Header("Player Experience")]
    [SerializeField] private int expForPlayer; // Điểm kinh nghiệm cho Player
    private PlayerExpManager playerExpManager;

    [Header("Spawner Settings")]
    public SpawnManager enemySpawner;
    public int enemyTypeIndex;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        // Tìm PlayerExpManager trong scene
        playerExpManager = FindObjectOfType<PlayerExpManager>();
        if (playerExpManager == null)
        {
            Debug.LogError("PlayerExpManager chưa được gắn trong scene!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return; // Đã chết, không nhận thêm sát thương

        currentHealth -= damage;
        Debug.Log($"Enemy nhận {damage} sát thương. Máu còn lại: {currentHealth}");

        if (currentHealth > 0)
        {
            TriggerHurtAnimation();
        }
        else
        {
            Die();
        }
    }

    private void TriggerHurtAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("isHurt", true);
            StartCoroutine(ResetHurtAnimation());
        }
    }

    private IEnumerator ResetHurtAnimation()
    {
        // Đợi đến khi animation "isHurt" chạy xong
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if (animator != null)
        {
            animator.SetBool("isHurt", false);
        }
    }

    private void Die()
    {
        if (animator != null)
        {
            animator.SetBool("isDeath", true);
        }
        Debug.Log("Enemy đã chết");
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(timeForAnimDeath);

        if (playerExpManager != null)
        {
            playerExpManager.AddExp(expForPlayer);
            Debug.Log($"Player nhận {expForPlayer} EXP.");
        }
        else
        {
            Debug.LogWarning("Không thể thêm EXP: PlayerExpManager không tồn tại.");
        }

        if (enemySpawner != null)
        {
            enemySpawner.EnemyDefeated(enemyTypeIndex);
        }
        else
        {
            Debug.LogWarning("EnemySpawner chưa được gắn.");
        }

        Destroy(gameObject);
    }
}
