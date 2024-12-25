using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Material flashMaterial; // Material chuyển màu
    [SerializeField] private Material defaultMaterial; // Material mặc định
    [SerializeField] private float timePlayerHurt = 0.2f; // Thời gian giữ màu đỏ
    [SerializeField] int maxHealth;
    public int MaxHealth => maxHealth;
    int currentHealth;
    public int CurrentHealth => currentHealth;

    public PlayerBar healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        StartCoroutine(FlashRed()); // Gọi hàm chuyển màu đỏ
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

    private IEnumerator FlashRed()
    {
        spriteRenderer.material = flashMaterial; // Chuyển sang Material đỏ
        yield return new WaitForSeconds(timePlayerHurt); // Thời gian giữ màu đỏ
        spriteRenderer.material = defaultMaterial; // Trả về Material mặc định
    }

    private IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        InitializeHealth();
    }
}
