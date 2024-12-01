using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private PlayerStats playerStats; // Tham chiếu đến PlayerStats

    private Animator animator;
    private Vector2 movement;
    private bool isFacingRight = true;
    private bool isRunning;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (joystick != null)
        {
            PlayerMove(new Vector2(joystick.Direction.x, joystick.Direction.y));
            FlipCharacter();

            isRunning = movement.magnitude > 0.2f;
            animator.SetBool("isRunning", isRunning);
        }
        else
        {
            Debug.LogWarning("Joystick is not assigned or active!");
        }
    }


    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            transform.position += (Vector3)(movement * playerStats.GetMoveSpeed() * Time.fixedDeltaTime);
        }
    }


    public void PlayerMove(Vector2 newJoystickPosition)
    {
        movement.x = newJoystickPosition.x;
        movement.y = newJoystickPosition.y;

        if (movement.magnitude < 0.2f)
        {
            movement = Vector2.zero;
        }
    }

    private void FlipCharacter()
    {
        if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
