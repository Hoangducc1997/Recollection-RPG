using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBarManager : MonoBehaviour
{
    Animator animator;

    [SerializeField] int maxHealth;
    int currentHealth;
    public PlayerBar healthBar;

    public void Start()
    {
        animator = GetComponent<Animator>();
        PlayerStartLevel();
    }

    private void PlayerStartLevel()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        animator.SetBool("isDeath", false);
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
            animator.SetBool("isDeath", true);
            StartCoroutine(WaitRestartLevel());
        }
    }

    private IEnumerator HurtAnim()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHurt", false);
    }

    private IEnumerator WaitRestartLevel()
    {
        yield return new WaitForSeconds(3f); // Chờ cho animation "isDeath" hoàn tất
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Tải lại scene hiện tại
        PlayerStartLevel();
    }
}
