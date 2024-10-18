using UnityEngine;

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
        animator = GetComponent<Animator>();
        currentLevel = 1;  // Set default level to 1 at start
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
            float moveSpeed = characterStats[currentLevel - 1].moveSpeed;  // Use (level-1) as array index
            transform.position += (Vector3)(movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Detect collision with level triggers (e.g., doors or objects with a tag for the next level)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object has the tag corresponding to the level
        int newLevel = playerLevelManager.GetLevelForTag(other.tag);  // Get the level based on the tag set in the Inspector
        if (newLevel > 0 && newLevel != currentLevel)
        {
            currentLevel = newLevel;  // Set the player's new level
            Debug.Log("Player leveled up to: " + currentLevel);
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
            AnimationClip[] availableAnimations = characterStats[currentLevel - 1].availableAnimations;
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
