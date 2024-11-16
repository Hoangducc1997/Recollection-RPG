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

    private PlayerHealthManager playerHeathManager;
    private float lastAttackTime;


    private void Start()
    {
        animator = GetComponent<Animator>();

        // Tìm đối tượng GamePlay bằng tag
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("GamePlay not found in the scene. Please ensure the GamePlay object has the tag 'GamePlay'.");
            return;
        }

        playerHeathManager = player.GetComponent<PlayerHealthManager>();
        if (playerHeathManager == null)
        {
            Debug.LogWarning("GamePlay does not have a PlayerBarManager component. Health-related functions will be disabled.");
        }

        lastAttackTime = -attackCooldown; // Để kẻ thù có thể tấn công ngay lập tức nếu ở trong phạm vi
    }

    private void Update()
    {
        if (player != null && playerHeathManager != null)
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

        if (playerHeathManager != null)
        {
            EnemyAttack();
        }

        // Đợi cho animation attack hoàn thành (giả sử thời gian animation là 1 giây)
        yield return new WaitForSeconds(1f);
        animator.SetBool("isAttack", false);
    }

    public void EnemyAttack()
    {
        if (playerHeathManager != null)
        {
            playerHeathManager.TakeDamage(damageEnemyAttack);
        }
    }
}
