using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int maxHealth = 100; // Lượng máu tối đa của Enemy
    private int currentHealth;
    [SerializeField] private float timeForAnimDeath = 1f; // Thời gian chờ cho animation isDeath

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Khởi tạo máu ban đầu bằng máu tối đa
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Trừ máu

        if (currentHealth > 0)
        {
            animator.SetBool("isHurt", true); // Gọi anim "isHurt" khi bị mất máu
            StartCoroutine(ResetHurtAnimation()); // Reset isHurt sau khi anim kết thúc
        }
        else if (currentHealth <= 0)
        {
            Die(); // Gọi hàm Die nếu máu bằng 0 hoặc ít hơn
        }
    }

    private IEnumerator ResetHurtAnimation()
    {
        // Đợi cho animation "isHurt" hoàn thành
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("isHurt", false); // Reset lại trạng thái isHurt
    }

    void Die()
    {
        animator.SetBool("isDeath", true); // Gọi hiệu ứng "isDeath"
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        // Chờ một khoảng thời gian cố định trước khi xóa đối tượng
        yield return new WaitForSeconds(timeForAnimDeath);
        Destroy(gameObject); // Xóa Enemy khỏi game
    }
}
