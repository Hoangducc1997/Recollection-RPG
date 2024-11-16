using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour
{
    Animator animator;

    [SerializeField] int maxHealth;
    int currentHealth;
    public PlayerBar healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        InitializeHealth();
    }

    private void InitializeHealth()
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
            StartCoroutine(RestartLevel());
        }
    }

    private IEnumerator HurtAnim()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHurt", false);
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        InitializeHealth();
    }
}
