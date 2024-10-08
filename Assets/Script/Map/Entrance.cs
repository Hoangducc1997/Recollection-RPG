using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Entrance : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    public float targetOrthoSize = 5f; // The desired orthographic size
    public float smoothSpeed = 2f; // Speed of the transition

    private void Awake()
    {
        animator = GetComponent<Animator>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>(); // Get the Cinemachine virtual camera
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // First, change the camera orthographic size
            StartCoroutine(ChangeCameraOrthoSize(targetOrthoSize));
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

        // Once the camera size change is done, trigger the door animation
        animator.SetBool("isOpen", true);
    }
}
