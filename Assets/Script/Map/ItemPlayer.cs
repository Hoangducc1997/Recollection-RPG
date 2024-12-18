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
                // Kiểm tra xem máu hiện tại có ít hơn máu tối đa không
                if (playerHealth.CurrentHealth < playerHealth.MaxHealth)
                {
                    playerHealth.IncreaseHealth(healthIncreaseAmount);
                    Destroy(gameObject); // Xóa item sau khi sử dụng
                }
            }
        }
    }
}
