using UnityEngine;

public class Key : MonoBehaviour
{
    public string keyID = "Key1"; // ID của chìa khóa, sử dụng cho từng scene hoặc key riêng

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(keyID, 1); // Lưu trạng thái đã nhặt key
            Debug.Log("Key collected: " + keyID);
            Destroy(gameObject); // Xóa chìa khóa khỏi scene
        }
    }
}
