using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarManager : MonoBehaviour
{
    Animator animator;

    [SerializeField] int maxHealth;
    int currentHealth;
    public BossBar healthBar;

    public void Start()
    {
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (!animator.GetBool("isHurt"))
        {
            animator.SetBool("isHurt", true);
            StartCoroutine(HurtAnim());
        }
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("BossDeath");
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
        // Chờ cho animation "isDeath" hoàn tất (thời gian chờ có thể thay đổi tùy thuộc vào độ dài của animation)
        yield return new WaitForSeconds(3f);

        // Xóa đối tượng boss khỏi scene
        Destroy(gameObject);
    }
}
