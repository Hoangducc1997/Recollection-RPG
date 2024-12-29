using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID = "Key1";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayVFX("PickupItem2");
            PlayerPrefs.SetInt(keyID, 1);
            Debug.Log("Key collected: " + keyID);

            MissionOvercomeMap.Instance?.ShowMissionComplete1(); // Hiển thị missionComplete2
            Destroy(gameObject);
        }
    }

    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All saves deleted!");
    }

    public void DeleteKeySave()
    {
        if (PlayerPrefs.HasKey(keyID))
        {
            PlayerPrefs.DeleteKey(keyID);
            Debug.Log("Key save deleted: " + keyID);
        }
        else
        {
            Debug.Log("No save found for key: " + keyID);
        }
    }
}
