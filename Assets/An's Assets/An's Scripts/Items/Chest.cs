using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject appearNextScene;
    private Animator animator; // Animator của hộp
    private bool isOpened = false; // Đảm bảo hộp chỉ mở một lần

    public GameObject rewardPrefab; // Phần thưởng xuất hiện khi mở hộp (nếu có)
    public string requiredKeyID = "Key1"; // ID của chìa khóa cần thiết

    void Start()
    {
        animator = GetComponent<Animator>();
        appearNextScene.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            // Kiểm tra nếu Player đã sở hữu chìa khóa
            if (PlayerPrefs.GetInt(requiredKeyID, 0) == 1)
            {
                OpenChest();
            }
            else
            {
                Debug.Log("You need the key to open this chest!");
            }
        }
    }

    private void OpenChest()
    {
        isOpened = true; // Đánh dấu hộp đã được mở
        animator.SetTrigger("OpenChest"); // Kích hoạt trigger trong Animator

        // Sinh phần thưởng nếu có
        if (rewardPrefab != null)
        {
            Instantiate(rewardPrefab, transform.position + Vector3.up, Quaternion.identity);
            appearNextScene.SetActive(true);
        }

        Debug.Log("Chest opened!");
    }
}
