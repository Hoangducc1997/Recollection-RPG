using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Joystick Reference")]
    [SerializeField] private Joystick joystick;

    [Header("Player Stats & Speed")]
    [SerializeField] private PlayerStats playerStats;

    private Animator animator;
    private Vector2 currentMovementInput;
    private bool isFacingRight = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    #region Input Handling
    /// <summary>
    /// Handle input from the joystick
    /// </summary>
    private void HandleInput()
    {
        if (joystick != null)
        {
            currentMovementInput = joystick.Direction;

            if (currentMovementInput.magnitude < 0.2f)
            {
                currentMovementInput = Vector2.zero; // Stop movement when stick input is too small
            }
        }
        else
        {
            Debug.LogWarning("Joystick reference is missing.");
            currentMovementInput = Vector2.zero;
        }

        HandleCharacterFlip();
    }
    #endregion

    #region Movement & Animation
    /// <summary>
    /// Move the player using physics and calculate movement speed
    /// </summary>
    private void MovePlayer()
    {
        if (currentMovementInput != Vector2.zero)
        {
            Vector2 desiredMovement = Vector2.ClampMagnitude(currentMovementInput, 1f) * playerStats.GetMoveSpeed();
            transform.Translate(desiredMovement * Time.fixedDeltaTime, Space.Self);
        }
    }

    /// <summary>
    /// Update the player's animation based on movement state
    /// </summary>
    private void UpdateAnimation()
    {
        bool isRunning = currentMovementInput.magnitude > 0.2f;
        animator.SetBool("isRunning", isRunning);
    }
    #endregion

    #region Flip Logic
    /// <summary>
    /// Flip the character's facing direction based on movement input
    /// </summary>
    private void HandleCharacterFlip()
    {
        if (currentMovementInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (currentMovementInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// Handle flipping the character's scale on the x-axis
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    #endregion
}
