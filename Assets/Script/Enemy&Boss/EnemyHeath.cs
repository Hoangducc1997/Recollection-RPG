﻿using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private float timeForAnimDeath = 1f;

    public SpawnManager enemySpawner;
    public int enemyTypeIndex;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetBool("isHurt", true);
            StartCoroutine(ResetHurtAnimation());
        }
        else if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator ResetHurtAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("isHurt", false);
    }

    void Die()
    {
        animator.SetBool("isDeath", true);
        StartCoroutine(WaitForDeathAnimation());
    }

    private IEnumerator WaitForDeathAnimation()
    {
        yield return new WaitForSeconds(timeForAnimDeath);

        enemySpawner.EnemyDefeated(enemyTypeIndex); // Thông báo số lượng enemy đã giảm

        Destroy(gameObject);
    }
}
