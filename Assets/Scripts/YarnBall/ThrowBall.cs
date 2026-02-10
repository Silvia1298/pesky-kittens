using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    private int scoreAmount;
    public int requiredHits = 10;
    public float interactRange = 2f;
    private Transform player;
    private Animator animator;

    void Start()
    {
        scoreAmount = 0;
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (player != null && Vector3.Distance(player.position, transform.position) <= interactRange)
            {
                scoreAmount++;
                Debug.Log("Ball hit: " + scoreAmount + " / " + requiredHits);
                if (scoreAmount >= requiredHits)
                {
                    animator = GetComponent<Animator>();
                    animator.SetBool("isThrown", true);
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
}
