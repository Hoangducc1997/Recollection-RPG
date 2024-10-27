﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    [SerializeField] private float distanceAttack;
    [SerializeField] private int damageEnemyAttack;
    [SerializeField] private float attackCooldown = 1f; // Thời gian giữa các lần tấn công
    private PlayerBarManager playerBarManager;
    private float lastAttackTime;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Tìm đối tượng Player bằng tag
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Please ensure the Player object has the tag 'Player'.");
            return;
        }

        playerBarManager = player.GetComponent<PlayerBarManager>();
        if (playerBarManager == null)
        {
            Debug.LogWarning("Player does not have a PlayerBarManager component. Health-related functions will be disabled.");
        }

        lastAttackTime = -attackCooldown; // Để kẻ thù có thể tấn công ngay lập tức nếu ở trong phạm vi
    }

    private void Update()
    {
        if (player != null && playerBarManager != null)
        {
            // Tính khoảng cách và hướng từ enemy đến player
            Vector2 direction = player.transform.position - transform.position;
            float distance = direction.magnitude;

            if (distance < distanceAttack && Time.time >= lastAttackTime + attackCooldown)
            {
                StartCoroutine(PerformAttack());
                lastAttackTime = Time.time;
            }
        }
    }

    private IEnumerator PerformAttack()
    {
        animator.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.5f); // Đợi một khoảng thời gian để animation attack bắt đầu

        if (playerBarManager != null)
        {
            EnemyAttack();
        }

        // Đợi cho animation attack hoàn thành (giả sử thời gian animation là 1 giây)
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttack", false);
    }

    public void EnemyAttack()
    {
        if (playerBarManager != null)
        {
            playerBarManager.TakeDamage(damageEnemyAttack);
        }
    }
}
