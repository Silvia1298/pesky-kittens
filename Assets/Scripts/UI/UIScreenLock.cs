using UnityEngine;
public class UIScreenLock : MonoBehaviour
{
    private Vector3 screenPosition;
    private Camera mainCamera;
    [SerializeField]private float xOffset = 10f;

    private void Start()
    {
        mainCamera = Camera.main;
        screenPosition = mainCamera.WorldToScreenPoint(transform.position);
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;
        transform.position = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x + xOffset, screenPosition.y, 10f));
    }
}
