using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator animator;   // Animator của hộp
    private bool isOpened = false;  // Đảm bảo hộp chỉ mở một lần

    public GameObject rewardPrefab; // Phần thưởng xuất hiện khi mở hộp (nếu có)

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra nếu nhân vật chạm vào hộp
        if (other.CompareTag("Player") && !isOpened)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;  // Đánh dấu hộp đã được mở
        animator.SetTrigger("OpenChest"); // Kích hoạt trigger trong Animator

        // Sinh phần thưởng nếu có
        if (rewardPrefab != null)
        {
            Instantiate(rewardPrefab, transform.position + Vector3.up, Quaternion.identity);
        }

        Debug.Log("Chest opened!");
    }
}
