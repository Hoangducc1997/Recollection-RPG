using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionReset : MonoBehaviour
{
    private void OnEnable()
    {
        // Đăng ký sự kiện chuyển cảnh
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi đối tượng bị vô hiệu hóa
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Đặt vị trí của PlayerMovement về (0, 0, 0) mỗi khi cảnh mới được tải
        transform.position = Vector3.zero;
    }
}
