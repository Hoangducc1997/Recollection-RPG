using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Entrance : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Animator lightAnimator;
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    public float targetOrthoSize = 5f; // The desired orthographic size
    public float smoothSpeed = 2f; // Speed of the transition
    [SerializeField] private GameObject rainEffect; // Reference to the GameObject to activate

    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {       
            // Activate the Rain Effect
            ActivateObject();

            // First, change the camera orthographic size
            StartCoroutine(ChangeCameraOrthoSize(targetOrthoSize));

            lightAnimator.SetTrigger("ActivetDarkLight"); // Trigger the light animation
        }
    }

    private IEnumerator ChangeCameraOrthoSize(float targetSize)
    {
        float startSize = virtualCamera.m_Lens.OrthographicSize;
        float elapsed = 0f;

        // Smoothly change the camera size
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * smoothSpeed;
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsed);
            yield return null;
        }

        virtualCamera.m_Lens.OrthographicSize = targetSize; // Ensure the exact target size is reached

        animator.SetBool("isOpen", true);
    }
 
    private void ActivateObject()
    {
        if (rainEffect != null)
        {
            rainEffect.SetActive(true); // Activate the GameObject
        }
    }
}
