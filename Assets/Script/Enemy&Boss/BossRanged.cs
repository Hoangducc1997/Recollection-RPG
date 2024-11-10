using System.Collections;
using UnityEngine;

public class BossRanged : BossBase
{
    [SerializeField] private Animator animator; // Gán Animator từ đối tượng cha
    public GameObject bulletPrefab;
    public Transform shootingPoint;

    protected float lastShotTime;
    protected bool isPlayerInRange = false;

    protected override void Start()
    {
        base.Start();

        // Kiểm tra và lấy Animator từ đối tượng cha nếu chưa gán sẵn
        if (animator == null)
        {
            animator = GetComponentInParent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("Animator not found on parent object.");
            }
        }
    }

    protected virtual void Update()
    {
        if (isPlayerInRange && bulletPrefab != null && shootingPoint != null && Time.time >= lastShotTime + attackCooldown)
        {
            StartCoroutine(PerformAttack());
            lastShotTime = Time.time;
        }
    }

    protected virtual IEnumerator PerformAttack()
    {
        // Bắt đầu animation tấn công
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(0.5f);

        if (playerBarManager != null)
        {
            ShootAtPlayer();  // Thực hiện tấn công từ xa
        }

        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttack", false); // Tắt animation tấn công sau khi hoàn thành
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(player.transform.position); // Gửi vị trí của player đến viên đạn
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
