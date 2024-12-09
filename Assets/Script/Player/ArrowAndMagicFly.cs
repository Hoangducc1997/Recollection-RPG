using System.Collections;
using UnityEngine;

public class ArrowAndMagicFly : MonoBehaviour
{
    private int damage;  // Sát thương của mũi tên

    [Header("Thời gian hủy mũi tên sau khi bắn")]
    [SerializeField] private float lifetime = 3f; // Thời gian tồn tại mũi tên

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    private void Start()
    {
        // Hủy mũi tên sau `lifetime` giây
        Destroy(gameObject, lifetime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }

            BossBarManager bossBarManager = collision.gameObject.GetComponent<BossBarManager>();
            if (bossBarManager != null)
            {
                bossBarManager.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
