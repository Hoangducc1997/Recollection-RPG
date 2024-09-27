using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;  // Tạo static instance

    void Awake()
    {
        // Đảm bảo rằng chỉ có một instance của PlayerManager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Nếu đã có một instance, phá hủy bản sao mới
            return;
        }

        DontDestroyOnLoad(gameObject); // Giữ lại instance khi đổi scene
    }

    void Start()
    {
    
    }

    // Cập nhật vị trí của player
    public void SetNewTransform(Vector2 Position)
    {
        this.transform.position = Position;
    }

    public Vector3 GetTransformPlayer()
    {
        return this.transform.position;
    }
}
