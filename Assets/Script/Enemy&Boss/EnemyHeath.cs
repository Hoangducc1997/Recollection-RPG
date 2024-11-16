using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private float timeForAnimDeath = 1f;
    [SerializeField] private int expForPlayer; // Điểm kinh nghiệm cho Player
    public SpawnManager enemySpawner;
    public int enemyTypeIndex;

    private PlayerExpManager playerExpManager; // Tham chiếu PlayerExpManager

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        // Tìm và tham chiếu PlayerExpManager trong scene
        playerExpManager = FindObjectOfType<PlayerExpManager>();

        // Debug để đảm bảo đã tìm thấy PlayerExpManager
        if (playerExpManager == null)
        {
            Debug.LogError("PlayerExpManager chưa được gắn trong scene!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetBool("isHurt", true);
            StartCoroutine(ResetHurtAnimation());
        }
        else if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("isHurt", false);
    }

    void Die()
    {
        animator.SetBool("isDeath", true);
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(timeForAnimDeath);

        // Tăng điểm kinh nghiệm cho Player nếu PlayerExpManager đã được tham chiếu
        if (playerExpManager != null)
        {
            playerExpManager.AddExp(expForPlayer);
        }
        else
        {
            Debug.LogWarning("Không thể thêm EXP: PlayerExpManager không tồn tại.");
        }

        // Thông báo Enemy đã bị tiêu diệt
        enemySpawner.EnemyDefeated(enemyTypeIndex);

        Destroy(gameObject);
    }
}
    