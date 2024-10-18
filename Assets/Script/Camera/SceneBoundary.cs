using UnityEngine;

public class SceneBoundary : MonoBehaviour
{
    public PolygonCollider2D boundaryCollider; // Collider of boundary
    public float orthographicSize = 5f;  // Size Camera For Setup

    void Start()
    {
        if (CameraManager.Instance != null)
        {
            // Get boundary for CameraManager
            CameraManager.Instance.SetBoundary(boundaryCollider);

            // Change Size Camera
            CameraManager.Instance.SetOrthographicSize(orthographicSize);
        }
        else
        {
            Debug.LogWarning("CameraManager is missing.");
        }
    }
}
