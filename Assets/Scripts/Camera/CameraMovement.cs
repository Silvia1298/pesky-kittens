using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Transform objective;      
    public Vector3 displacement = new Vector3(0f, 1f, 0f); 
    public float manualOrthographicSize = 10f;

    public float leftEdge = -50f;
    public float rightEdge = 50f;
    public float bottomEdge = -20f;
    public float topEdge = 140f;  
    private Camera cam;
    private Vector3 velocity = Vector3.zero;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign the objective every time a new scene loads
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if(player != null)
        {
            objective = player.transform;
        } 
    }
    private void Start()
    {

        if (objective == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                objective = player.transform;
            }   
        }

        cam = Camera.main;

        if (cam != null)
        {
            cam.orthographicSize = manualOrthographicSize;

            float mapHeight = topEdge - bottomEdge;
            if (mapHeight < 2f * cam.orthographicSize)
            {
                cam.orthographicSize = Mathf.Min(manualOrthographicSize, (topEdge - bottomEdge) / 2f - 0.1f);

            }
        }
    }

    private void LateUpdate()
    {
        if (objective == null || cam == null) return;

        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.orthographicSize * cam.aspect;
        float targetX = Mathf.Clamp(objective.position.x + displacement.x,
                                    leftEdge + halfWidth,
                                    rightEdge - halfWidth);
        float targetY;
        float mapHeight = topEdge - bottomEdge;
        if (mapHeight > 2f * halfHeight)
        {
            targetY = Mathf.Clamp(objective.position.y + displacement.y,
                                bottomEdge + halfHeight,
                                topEdge - halfHeight);
        }
        else
        {
            targetY = (topEdge + bottomEdge) / 2f;
        }

        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);              

        // Smooth follow
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.2f);
    }
}
