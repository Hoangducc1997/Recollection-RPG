using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100; // Lượng máu tối đa của Enemy
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu ban đầu bằng máu tối đa
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Trừ máu
        if (currentHealth <= 0)
        {
            Die(); // Gọi hàm Die nếu máu bằng 0 hoặc ít hơn
        }
    }

    void Die()
    {
        // Bạn có thể thêm hiệu ứng biến mất ở đây, như hiệu ứng particle
        Destroy(gameObject); // Xóa Enemy khỏi game
    }
}
