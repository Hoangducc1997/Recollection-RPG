using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private CharaterStats[] characterStats;  // Array of stats for each level
    [SerializeField] private Joystick joystick;  // Joystick for movement
    [SerializeField] private PlayerLevelManager playerLevelManager;  // Reference to level manager

    private Animator animator;
    private Vector2 movement;
    private bool isFacingRight = true;  // To track the direction the character is facing
    private int currentLevel;  // Current player level

    void Start()
    {
        // Assign the Animator
        animator = GetComponent<Animator>();
        // Set the player's level based on the current scene
        currentLevel = playerLevelManager.GetLevelForCurrentScene();
    }

    void Update()
    {
        PlayerMove(new Vector2(joystick.Direction.x, joystick.Direction.y));
        UpdateAnimation();
        FlipCharacter();
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            float moveSpeed = characterStats[currentLevel].moveSpeed;
            transform.position += (Vector3)(movement * moveSpeed * Time.fixedDeltaTime);
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

    private void UpdateAnimation()
    {
        bool isRunning = movement.magnitude > 0.1f;
        animator.SetBool("isRunning", isRunning);

        if (isRunning)
        {
            AnimationClip[] availableAnimations = characterStats[currentLevel].availableAnimations;
            if (availableAnimations.Length > 0)
            {
                animator.Play(availableAnimations[0].name);  // Use the first available animation (e.g., running)
            }
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
