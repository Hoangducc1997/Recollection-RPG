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
                MissionOvercomeMap.Instance?.ShowMissionComplete2(); // Hiển thị missionComplete1
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
            Instantiate(rewardPrefab, transform.position + Vector3.up, Quaternion.identity);
            appearNextScene.SetActive(true);
        }

        Debug.Log("Chest opened!");
    }
}
