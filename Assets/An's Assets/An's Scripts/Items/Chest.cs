using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject appearNextScene;
    private Animator animator;
    private bool isOpened = false;

    public GameObject rewardPrefab;
    public string requiredKeyID = "Key1";

    void Start()
    {
        animator = GetComponent<Animator>();
        appearNextScene.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpened)
        {
            if (PlayerPrefs.GetInt(requiredKeyID, 0) == 1)
            {
                OpenChest();
                MissionOvercomeMap.Instance?.ShowMissionComplete2(); // Hiển thị missionComplete2
                AudioManager.Instance.PlayVFX("PlayerLevelUp");
            }
            else
            {
                Debug.Log("You need the key to open this chest!");
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
            Vector3 spawnOffset = new Vector3(0.5f, 2f, 0f); // Tùy chỉnh vị trí spawn
            Instantiate(rewardPrefab, transform.position + spawnOffset, Quaternion.identity);
            appearNextScene.SetActive(true);
        }

        Debug.Log("Chest opened!");
    }

}
