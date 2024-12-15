using UnityEngine;

public class ItemPlayer : MonoBehaviour
{
    public ItemType itemType;
    public int healthIncreaseAmount; // Số máu sẽ tăng

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Kiểm tra va chạm với Player
        {
            PlayerHealthManager playerHealth = collision.GetComponent<PlayerHealthManager>();
            if (playerHealth != null && itemType == ItemType.HeathIncrease)
            {
                playerHealth.IncreaseHealth(healthIncreaseAmount);
                Destroy(gameObject); // Xóa item sau khi sử dụng
            }
        }
    }
}
