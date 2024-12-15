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
    [SerializeField] private int expForPlayer;
    private PlayerExpManager playerExpManager;

    [Header("Spawner Settings")]
    public SpawnManager enemySpawner;
    public int enemyTypeIndex;

    [Header("Item Drop Settings")]
    [SerializeField] private ItemDrop[] itemDrops; // Danh sách các item và tỷ lệ rơi


    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        playerExpManager = FindObjectOfType<PlayerExpManager>();
        if (playerExpManager == null)
        {
            Debug.LogError("PlayerExpManager not found in the scene!");
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"Enemy took {damage} damage. Remaining health: {currentHealth}");

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
            Debug.Log("Enemy is hurt.");
            StartCoroutine(ResetHurtAnimation());
        }
    }

    private IEnumerator ResetHurtAnimation()
    {
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
        Debug.Log("Enemy died.");
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(timeForAnimDeath);

        if (playerExpManager != null)
        {
            playerExpManager.AddExp(expForPlayer);
            Debug.Log($"Player gained {expForPlayer} EXP.");
        }

        if (enemySpawner != null)
        {
            enemySpawner.EnemyDefeated(enemyTypeIndex);
        }
        else
        {
            Debug.LogWarning("EnemySpawner not assigned.");
        }

        DropItems(); // Gọi hàm để rơi item

        Destroy(gameObject);
    }

    private void DropItems()
    {
        foreach (var itemDrop in itemDrops)
        {
            float chance = Random.Range(0f, 100f);
            if (chance <= itemDrop.dropRate)
            {
                Instantiate(itemDrop.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log($"Dropped item: {itemDrop.itemPrefab.name} with chance {itemDrop.dropRate}%.");
            }
        }
    }
}
