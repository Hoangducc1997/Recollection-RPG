using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIEnemy : MonoBehaviour
{
    private AIPath path;
    private Animator animator;
    private BossBarManager bossBarManager; // Thêm tham chiếu đến BossBarManager
    [SerializeField] private float moveSpeed;
    private Transform target;
    private bool isFacingRight = true;

    private void Start()
    {
        path = GetComponent<AIPath>();
        animator = GetComponent<Animator>();
        bossBarManager = GetComponent<BossBarManager>(); // Gán script BossBarManager

        // Tìm đối tượng Player bằng tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found. Ensure Player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        // Kiểm tra nếu boss đã chết thì không di chuyển hoặc thực hiện các hành động khác
        if (bossBarManager != null && bossBarManager.IsDead())
        {
            path.enabled = false; // Vô hiệu hóa AIPath để boss không di chuyển nữa
            animator.SetBool("isRun", false); // Dừng animation chạy
            return;
        }

        if (target == null) return;

        path.maxSpeed = moveSpeed;
        path.destination = target.position;

        // Di chuyển animation khi enemy đang di chuyển
        animator.SetBool("isRun", path.velocity.magnitude > 0);

        // Quay mặt về phía player
        if (target.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (target.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
