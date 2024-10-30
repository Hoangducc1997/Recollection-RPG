using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIEnemy : MonoBehaviour
{
    private AIPath path;
    private Animator animator;
    [SerializeField] private float moveSpeed;
    private Transform target;
    private bool isFacingRight = true;

    private void Start()
    {
        path = GetComponent<AIPath>();
        animator = GetComponent<Animator>();

        // Tìm đối tượng Player bằng tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found. Ensure Player has the 'Player' tag.");
        }
    }

    private void Update()
    {
        if (target == null) return;

        path.maxSpeed = moveSpeed;
        path.destination = target.position;

        // Di chuyển animation khi enemy đang di chuyển
        animator.SetBool("isRun", path.velocity.magnitude > 0);

        // Quay mặt về phía player
        if (target.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }
        else if (target.position.x < transform.position.x && isFacingRight)
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
