using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAndMagicFly : MonoBehaviour
{
    private int damage;  // Sát thương của mũi tên

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra xem mũi tên có va chạm với đối tượng có script EnemyHealth không
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);  // Gọi hàm TakeDamage trên kẻ thù
            }

            // Hủy mũi tên sau khi va chạm
            Destroy(gameObject);
        }
    }
}