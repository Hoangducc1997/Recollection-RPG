using System.Collections;
using UnityEngine;

public class BulletBoss : BossBase
{
    protected Animator animator; // Đổi từ private thành protected
    public float speed = 5f;
    private Vector2 target;

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        base.Start();
        // Hủy viên đạn sau 10 giây
        Destroy(gameObject, 8f);
    }

    public void SetTarget(Vector2 targetPosition)
    {
        target = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Update()
    {
        if (target != Vector2.zero)
        {
            transform.position += (Vector3)target * speed * Time.deltaTime;
        }
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("BulletBoss hit the Player.");
            if (playerBarManager != null)
            {
                playerBarManager.TakeDamage(10);
            }
            animator.SetTrigger("Explosion");
            StartCoroutine(DestroyAfterAnimation());
        }
    }


    protected IEnumerator DestroyAfterAnimation() // Đổi từ private thành protected
    {
        // Đợi cho đến khi animation kết thúc trước khi hủy viên đạn
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
