using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    // Singleton instance of CameraManager
    public static CameraManager Instance { get; private set; }

    public CinemachineConfiner cinemachineConfiner; // Confiner for the Cinemachine camera
    public CinemachineVirtualCamera virtualCamera;  // Reference to the virtual camera to modify OrthographicSize

    private void Awake()
    {
        // Ensure only one instance of CameraManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy new instance if one already exists
        }
    }

    // Method to set the boundary for the CinemachineConfiner
    public void SetBoundary(PolygonCollider2D boundaryCollider)
    {
        if (cinemachineConfiner != null && boundaryCollider != null)
        {
            // Assign the PolygonCollider2D to the CinemachineConfiner's bounding shape
            cinemachineConfiner.m_BoundingShape2D = boundaryCollider;
            cinemachineConfiner.InvalidatePathCache(); // Invalidate cache to apply the new boundary
        }
        else
        {
            Debug.LogWarning("CinemachineConfiner or boundaryCollider is missing.");
        }
    }

    // Method to change the OrthographicSize of the camera
    public void SetOrthographicSize(float size)
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = size;  // Adjust Orthographic Size
        }
        else
        {
            Debug.LogWarning("Virtual Camera is missing.");
        }
    }
}
