using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID = "Key1"; // ID của chìa khóa, sử dụng cho từng scene hoặc key riêng

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayVFX("PickupItem2");
            PlayerPrefs.SetInt(keyID, 1); // Lưu trạng thái đã nhặt key
            Debug.Log("Key collected: " + keyID);
            Destroy(gameObject); // Xóa chìa khóa khỏi scene
        }
    }
    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteAll(); // Xóa toàn bộ dữ liệu PlayerPrefs
        Debug.Log("All saves deleted!");
    }

    public void DeleteKeySave()
    {
        if (PlayerPrefs.HasKey(keyID))
        {
            PlayerPrefs.DeleteKey(keyID); // Xóa trạng thái đã lưu của chìa khóa
            Debug.Log("Key save deleted: " + keyID);
        }
        else
        {
            Debug.Log("No save found for key: " + keyID);
        }
    }

}
