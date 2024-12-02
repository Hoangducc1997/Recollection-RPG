using System.Collections;
using UnityEngine;

public class EnemyRanged : EnemyBase
{
    [SerializeField] private float distanceAttack = 10f;  // Phạm vi tấn công
    [SerializeField] private float minDistanceToPlayer = 5f; // Khoảng cách tối thiểu với Player
    [SerializeField] private float moveSpeed = 2f;         // Tốc độ di chuyển của Enemy
    [SerializeField] private GameObject bulletPrefab;      // Prefab viên đạn
    [SerializeField] private Transform shootingPoint;      // Vị trí bắn

    private Animator animator;
    private float lastAttackTime;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        lastAttackTime = -attackCooldown; // Đặt thời gian tấn công đầu tiên
    }

    private void Update()
    {
        if (player == null || bulletPrefab == null || shootingPoint == null) return;

        float distance = Vector2.Distance(transform.position, player.transform.position);

        // Kiểm tra nếu player trong phạm vi và cooldown đã hết
        if (distance <= distanceAttack && Time.time >= lastAttackTime + attackCooldown)
        {
            // Tấn công
            StartCoroutine(PerformAttack());
            lastAttackTime = Time.time;
        }
        else if (distance < minDistanceToPlayer)
        {
            // Di chuyển lùi lại khi quá gần Player
            MoveAwayFromPlayer();
        }
    }

    private void MoveAwayFromPlayer()
    {
        Vector2 direction = (transform.position - player.transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)direction, moveSpeed * Time.deltaTime);
    }

    private IEnumerator PerformAttack()
    {
        // Kích hoạt animation tấn công
        if (animator != null)
        {
            animator.SetBool("isAttack", true);
        }

        yield return new WaitForSeconds(0.5f); // Delay trước khi bắn

        ShootAtPlayer(); // Thực hiện bắn

        yield return new WaitForSeconds(0.5f); // Kết thúc animation tấn công
        if (animator != null)
        {
            animator.SetBool("isAttack", false);
        }
    }

    private void ShootAtPlayer()
    {
        if (bulletPrefab == null || shootingPoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

        // Tính hướng bắn về phía Player
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(player.transform.position); // Gán hướng di chuyển
        }
    }
}
