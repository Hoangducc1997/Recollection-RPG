using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject appearNextScene; // Đối tượng kích hoạt khi mở rương
    [SerializeField] private Vector3 spawnOffset = new Vector3(0.5f, 2f, 0f); // Tùy chỉnh vị trí spawn

    private Animator animator;
    private bool isOpened = false;

    public GameObject rewardPrefab; // Phần thưởng khi mở rương
    public string requiredKeyID = "Key1"; // ID của khóa cần thiết để mở rương này

    void Start()
    {
        animator = GetComponent<Animator>();
        if (appearNextScene != null)
        {
            appearNextScene.SetActive(false); // Đảm bảo đối tượng ẩn lúc đầu
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            // Kiểm tra xem người chơi có khóa hay không
            if (PlayerPrefs.GetInt(requiredKeyID, 0) == 1)
            {
                OpenChest();
                MissionOvercomeMap.Instance?.ShowMissionComplete2(); // Hiển thị missionComplete2
                AudioManager.Instance.PlayVFX("PlayerLevelUp");
            }
            else
            {
                Debug.Log($"You need the {requiredKeyID} to open this chest!");
                AudioManager.Instance.PlayVFX("Touch");
            }
        }
    }

    private void OpenChest()
    {
        isOpened = true;
        animator.SetTrigger("OpenChest");

        if (rewardPrefab != null)
        {
            Instantiate(rewardPrefab, transform.position + spawnOffset, Quaternion.identity);
        }

        if (appearNextScene != null)
        {
            appearNextScene.SetActive(true); // Kích hoạt đối tượng tiếp theo
        }

        Debug.Log("Chest opened!");
    }
}
