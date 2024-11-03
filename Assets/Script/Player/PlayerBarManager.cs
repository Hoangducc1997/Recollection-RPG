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
            animator.SetBool("isDeath", true);
           
            StartCoroutine(WaitBackHome());
        }
    }


    private IEnumerator HurtAnim()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isHurt", false);
    }

    private IEnumerator WaitBackHome()
    {
        yield return new WaitForSeconds(3f);
        //Restart lại level
    }

}
