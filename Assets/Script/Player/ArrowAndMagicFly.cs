using UnityEngine;

public class ArrowAndMagicFly : MonoBehaviour
{
    private int damage;
    private Animator playerAnimator;
    private int animationIndex;

    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    public void SetPlayerAnimator(Animator animator, int index)
    {
        playerAnimator = animator;
        animationIndex = index;
    }

    // Ví dụ: sử dụng playerAnimator và animationIndex trong logic của đạn/phép thuật
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            // Kiểm tra có nhận sát thương không
            Debug.Log($"Hit {collision.name} for {damage} damage.");

            // Sử dụng animation index nếu cần
            if (playerAnimator != null)
            {
                playerAnimator.SetInteger("isWeaponType", animationIndex);
            }

            // Xử lý sát thương
            if (collision.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(damage);  // Gọi hàm nhận sát thương
            }
            else if (collision.TryGetComponent(out BossBarManager bossHealth))
            {
                bossHealth.TakeDamage(damage);  // Gọi hàm nhận sát thương cho Boss
            }

            Destroy(gameObject); // Phá hủy mũi tên sau khi va chạm
        }
    }

}
