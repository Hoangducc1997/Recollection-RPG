using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet : BossBase
{
    public float speed = 5f;
    private Vector2 target;

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
            playerBarManager.TakeDamage(10); // Gây sát thương cho player
            Destroy(gameObject); // Hủy viên đạn
        }
    }
}
