using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour
{
    Animator animator;

    [SerializeField] int maxHealth;    
    public int MaxHealth => maxHealth; // Trả về giá trị maxHealth
    int currentHealth;
    public int CurrentHealth => currentHealth; // Trả về giá trị currentHealth

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
            AudioManager.Instance.PlayVFX("PlayerDeath");
            animator.SetBool("isDeath", true);
            StartCoroutine(RestartLevel());
        }
    }
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Giới hạn máu tối đa
        }
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
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
