using UnityEngine;
using UnityEngine.UI; // Nếu bạn đang sử dụng Joystick từ UI

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Joystick joystick;  // Joystick mà bạn đang sử dụng
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lấy input từ Joystick
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        // Kiểm tra nếu giá trị Joystick quá nhỏ để không di chuyển khi gần 0
        if (movement.magnitude < 0.2f)
        {
            movement = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}