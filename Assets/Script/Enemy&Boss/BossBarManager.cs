﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarManager : MonoBehaviour
{
    Animator animator;
    private bool isDead = false; // Thêm cờ kiểm tra trạng thái chết của boss

    [SerializeField] int maxHealth;
    int currentHealth;
    public BossBar healthBar;
    [SerializeField] private int expForPlayer; // Điểm kinh nghiệm cho Player
    private PlayerExpManager playerExpManager; // Tham chiếu PlayerExpManager
    public void Start()
    {
        animator = GetComponent<Animator>();
        // Tìm và tham chiếu PlayerExpManager trong scene
        playerExpManager = FindObjectOfType<PlayerExpManager>();
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Không thực hiện gì nếu boss đã chết

        currentHealth -= damage;
        if (!animator.GetBool("isHurt"))
        {
            animator.SetBool("isHurt", true);
            StartCoroutine(HurtAnim());
        }
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true; // Đánh dấu boss đã chết
            Debug.Log("BossDeath");
            // Tăng điểm kinh nghiệm cho Player nếu PlayerExpManager đã được tham chiếu
            if (playerExpManager != null)
            {
                playerExpManager.AddExp(expForPlayer);
            }
            else
            {
                Debug.LogWarning("Không thể thêm EXP: PlayerExpManager không tồn tại.");
            }
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.AppearObjNextScene();
            }
            StartCoroutine(DestroyBoss());
        }
    }

    private IEnumerator HurtAnim()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHurt", false);
    }

    private IEnumerator DestroyBoss()
    {
        animator.SetBool("isDeath", true);
        yield return new WaitForSeconds(3f);
        
        Destroy(gameObject);
    }

    // Hàm để kiểm tra trạng thái chết từ script khác
    public bool IsDead()
    {
        return isDead;
    }
}
