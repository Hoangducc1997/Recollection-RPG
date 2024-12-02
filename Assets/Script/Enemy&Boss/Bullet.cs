using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Animator animator;
    public float speed = 5f; // Tốc độ viên đạn
    private Vector2 direction; // Hướng di chuyển của đạn
    protected PlayerHealthManager playerHealthManager;

    [SerializeField] protected int bulletDamage = 1;  // Sát thương của viên đạn (có thể điều chỉnh trong Inspector)

    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, 8f); // Tự hủy sau 8 giây nếu không chạm mục tiêu
    }

    // Thiết lập hướng di chuyển cho viên đạn
    public void SetDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)transform.position).normalized; // Hướng từ viên đạn đến mục tiêu
    }

    void Update()
    {
        if (direction != Vector2.zero)
        {
            // Di chuyển viên đạn
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }

    // Va chạm với Player
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Gây sát thương
            playerHealthManager = collision.GetComponent<PlayerHealthManager>();
            if (playerHealthManager != null)
            {
                playerHealthManager.TakeDamage(bulletDamage);  // Sử dụng giá trị bulletDamage
            }

            // Dừng di chuyển của viên đạn
            speed = 0f;  // Dừng di chuyển ngay lập tức

            // Kích hoạt explosion ngay lập tức
            animator.SetTrigger("Explosion");
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    // Hủy viên đạn sau khi hoàn tất animation
    protected IEnumerator DestroyAfterAnimation()
    {
        if (animator != null)
        {
            // Chờ đợi cho đến khi animation kết thúc
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        // Hủy viên đạn sau khi animation hoàn tất
        Destroy(gameObject);
    }
}
