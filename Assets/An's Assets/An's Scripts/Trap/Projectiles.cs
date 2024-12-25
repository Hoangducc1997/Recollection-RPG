using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float moveSpeed = 1f;

    public float timeToLive = 5f;

    private float timeSinceSpawned = 0f;

    public float knockbackForce = 200f;

    public int damage = 1; // Chuyển đổi từ float sang int để phù hợp với TakeDamage

    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * transform.right * Time.deltaTime;

        timeSinceSpawned += Time.deltaTime;

        if (timeSinceSpawned > timeToLive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        string tag = collider.gameObject.tag;

        if (tag == "Player")
        {
            PlayerHealthManager playerHealth = collider.gameObject.GetComponent<PlayerHealthManager>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Gọi trực tiếp phương thức TakeDamage
                Destroy(gameObject);
            }
        }
        else if (tag == "Wall") // Kiểm tra nếu chạm vào tường
        {
            Destroy(gameObject); // Xóa mũi tên khi chạm tường
        }
    }
}
