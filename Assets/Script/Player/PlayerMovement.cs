﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharaterStats[] characterStats; // Character stats
    [SerializeField] private Joystick joystick; // Joystick for movement

    private Animator animator;
    private Vector2 movement;
    private bool isFacingRight = true;
    private bool isRunning;
    private int currentLevel;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentLevel = 1;
    }

    void Update()
    {
        PlayerMove(new Vector2(joystick.Direction.x, joystick.Direction.y));
        FlipCharacter();

        // Update running state
        isRunning = movement.magnitude > 0.2f;
        animator.SetBool("isRunning", isRunning);
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            float moveSpeed = characterStats[currentLevel - 1].moveSpeed;
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
