using UnityEngine;

/// <summary>
/// Locks a UI element to a fixed screen position, preventing it from moving with the camera.
/// Works by storing the initial screen position and updating the world position each frame to maintain it.
/// </summary>
public class UIScreenLock : MonoBehaviour
{
    private Vector3 screenPosition;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        
        // Store the current screen position
        screenPosition = mainCamera.WorldToScreenPoint(transform.position);
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;

        // Convert the fixed screen position back to world position each frame
        // This keeps the element at the same screen location regardless of camera movement
        transform.position = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, 10f));
    }
}
