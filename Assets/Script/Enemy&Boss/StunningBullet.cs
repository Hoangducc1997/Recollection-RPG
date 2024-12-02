using System.Collections;
using UnityEngine;

public class StunningBullet : Bullet
{
    [SerializeField] private float stunDuration = 2f; // Thời gian làm choáng

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Gây sát thương sử dụng giá trị bulletDamage từ Bullet (class cha)
            playerHealthManager = collision.GetComponent<PlayerHealthManager>();
            if (playerHealthManager != null)
            {
                playerHealthManager.TakeDamage(bulletDamage);  // Sử dụng bulletDamage từ lớp cha
            }

            // Làm choáng
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                StartCoroutine(StunPlayer(playerMovement));
            }

            // Dừng di chuyển của viên đạn và kích hoạt explosion ngay lập tức
            speed = 0f;  // Hoặc set direction = Vector2.zero nếu bạn muốn dừng di chuyển ngay lập tức
            animator.SetTrigger("Explosion");

            // Hủy viên đạn sau khi animation hoàn tất
            StartCoroutine(DestroyAfterAnimation());
        }
        else
        {
            // Gọi logic mặc định từ Bullet.cs
            base.OnTriggerEnter2D(collision);
        }
    }

    // Logic làm choáng
    private IEnumerator StunPlayer(PlayerMovement playerMovement)
    {
        if (playerMovement == null) yield break;

        // Vô hiệu hóa di chuyển
        playerMovement.enabled = false;

        // Chờ hết thời gian stun
        yield return new WaitForSeconds(stunDuration);

        // Kích hoạt lại di chuyển
        playerMovement.enabled = true;
    }
}
