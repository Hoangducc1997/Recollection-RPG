using System.Collections;
using UnityEngine;

public class Bullet : BossBase
{
    private Animator animator;
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
        transform.position += (Vector3)target * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerBarManager != null)
        {
            animator.SetTrigger("Explosion");
            playerBarManager.TakeDamage(10); // Gây sát thương cho player
            StartCoroutine(DestroyAfterAnimation());
        }
    }

    private IEnumerator DestroyAfterAnimation()
    {
        // Đợi cho đến khi animation kết thúc trước khi hủy viên đạn
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

}
