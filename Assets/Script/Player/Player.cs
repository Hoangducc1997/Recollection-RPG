using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovementConfig movementConfig; // Reference to the ScriptableObject
    [SerializeField] private Joystick joystick;  // Joystick that you're using
    private Animator animator;
    private Vector2 movement;
    private bool isFacingRight = true;  // To track the direction the character is facing

    void Start()
    {
        // Assign the Animator
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Call the function to get values from the joystick and update movement
        PlayerMove(new Vector2(joystick.Direction.x, joystick.Direction.y));

        // Control the animation based on movement speed
        UpdateAnimation();

        // Check and flip the character if needed
        FlipCharacter();
    }

    void FixedUpdate()
    {
        // Move the character by changing its position
        if (movement != Vector2.zero)
        {
            transform.position += (Vector3)(movement * movementConfig.moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void PlayerMove(Vector2 newJoystickPosition)
    {
        // Update the movement value from the joystick
        movement.x = newJoystickPosition.x;
        movement.y = newJoystickPosition.y;

        // Check if the Joystick value is too small to prevent movement when near 0
        if (movement.magnitude < 0.2f)
        {
            movement = Vector2.zero;
        }
    }

    private void UpdateAnimation()
    {
        // Check movement speed
        bool isRunning = movement.magnitude > 0.1f;
        // Set the animator's isRunning parameter
        animator.SetBool("isRunning", isRunning);

        // Play the running animation if moving
        if (isRunning)
        {
            animator.Play(movementConfig.animations[0].name); // Play the running animation
        }
    }

    private void FlipCharacter()
    {
        // Check if the Player is moving to the left
        if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
        // Check if the Player is moving to the right
        else if (movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;  // Flip the x-axis to flip the character
        transform.localScale = scale;
    }
}
