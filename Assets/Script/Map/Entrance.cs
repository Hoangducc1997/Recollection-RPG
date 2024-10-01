using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Entrance : MonoBehaviour
{
    private Animator animator;

    [SerializeField] string sceneName;
    private void Awake()
    {

        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isOpen", true);

            // Wait for a while to let the door open animation complete before switching scenes
            StartCoroutine(WaitAndLoadScene());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("isOpen", false);
        }
    }

    // Coroutine to wait for the animation to finish and load the next scene
    private IEnumerator WaitAndLoadScene()
    {
        // Wait for about 1.5 seconds (or adjust based on your animation length)
        yield return new WaitForSeconds(3.5f);

        // Load the next scene, replace "NextScene" with your scene name
        SceneManager.LoadScene(sceneName);
    }
}
